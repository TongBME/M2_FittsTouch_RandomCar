/**
 * @file DynalinkHS.cs
 * @brief Unity Communication Interface with MMU
 * @details 
 * @mainpage 
 * @author Jason(Chen Xin)
 * @email xin.chen@fftai.com
 * @version 1.0.0
 * @date 2019-01-24
 * @license Private
 */

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;
using System.IO;

namespace FFTAICommunicationLib
{
    public class DynaLinkHS :
        IFFTAICommunicationV2IAPInterfaceObserver,
        IFFTAICommunicationV2SystemInterfaceObserver,
        IFFTAICommunicationOperationConnectionStatusObserver,
        IFFTAICommunicationV2CommunicationInterfaceObserver,
        IFFTAICommunicationV2HardwareInterfaceObserver,
        IFFTAICommunicationV2RobotInterfaceObserver,
        IFFTAICommunicationV2M1RobotInterfaceObserver,
        IFFTAICommunicationV2M1TaskInterfaceObserver,
        IFFTAICommunicationV2M2RobotInterfaceObserver,
        IFFTAICommunicationV2M2TaskInterfaceObserver,
        IFFTAICommunicationV2X2RobotInterfaceObserver,
        IFFTAICommunicationV2X2TaskInterfaceObserver
    {
        //-------------------------------------------- Variable Definition (Singleton) -------------------------------------------------------------------------

        private static volatile DynaLinkHS instance;

        /// @brief The singleton instance of DynalinkHS class [DynalinkHS 的单例]
        public static DynaLinkHS Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DynaLinkHS();
                }

                return instance;
            }
        }

        //-------------------------------------------- Variable Definition (Singleton) -------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (Network) ---------------------------------------------------------------------------

        private class StatusNetwork
        {
            /// @brief Server address IP [服务器IP地址]
            public static string ServerAddressIp = "192.168.102.200";

            /// @brief Server address port [服务器端口]
            public static int ServerAddressPort = 4196;
        }

        //-------------------------------------------- Variable Definition (Network) ---------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (IAP) -------------------------------------------------------------------------------

        /// @brief The status of In-Application-Programming(IAP) process ["在线编程(IAP)"状态结构体]
        public class StatusIAP
        {
            /// @brief Hardware version [IAP 硬件版本]
            public static uint HardwareVersion = 0;

            /// @brief Software version [IAP 软件版本]
            public static uint SoftwareVersion = 0;

            /// @brief The current boot mode of the MMU [当前启动模式]
            /// @details 
            ///     - DynaLinkHSPara.IAPBootMode.IAP The system will run in IAP mode next time [表示下一次启动程序将运行在 IAP 模式下]
            ///     - DynaLinkHSPara.IAPBootMode.APP The system will run in APP mode next time [表示下一次启动程序将运行在 APP 模式下]
            public static DynaLinkHSPara.IAPBootMode IAPBootMode = DynaLinkHSPara.IAPBootMode.None;

            /// @brief The current work status of the MMU [当前运行状态]
            /// @details 
            ///     - DynaLinkHSPara.IAPWorkStatus.IAP The system is now in IAP mode [表示当前程序运行在 IAP 模式下]
            ///     - DynaLinkHSPara.IAPWorkStatus.APP The system is now in APP mode [表示当前程序运行在 APP 模式下]
            public static DynaLinkHSPara.IAPWorkStatus IAPWorkStatus = DynaLinkHSPara.IAPWorkStatus.None;

            /// @brief The progress of upgrade Iap (bootloader) [Bootloader 的下载进度]
            /// @details The range in between 0-100. [值范围 0 - 100]
            public static uint IAPUpgradeIapProgress = 0;

            /// @brief The progress of upgrade Iap (bootloader) [Application 的下载进度]
            /// @details The range in between 0-100. [值范围 0 - 100]
            public static uint IAPUpgradeAppProgress = 0;

            /// @brief The status of the Iap (bootloader) upgrade [Bootloader 的下载状态]
            /// @details
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Ready [准备状态]
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Running [执行状态]
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Success [成功状态]
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Fail [失败状态]
            public static DynaLinkHSPara.IAPUpgradeStatus IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            /// @brief The status of the App (Application) upgrade [Application 的下载状态]
            /// @details
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Ready [准备状态]
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Running [执行状态]
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Success [成功状态]
            ///     - DynaLinkHSPara.IAPUpgradeStatus.Fail [失败状态]
            public static DynaLinkHSPara.IAPUpgradeStatus IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;
        }

        private static bool IAPUpgradeIapLocker = false;
        private static bool IAPUpgradeIapTransmitLocker = false;
        private static uint IAPUpgradeIapStartAddress = 0x08000000;
        private static uint IAPUpgradeIapSize = 0;
        private static uint IAPUpgradeIapMonitorCount = 0;
        private static uint IAPUpgradeIapMonitorMaximumCount = 100 * 2 /* 2s */;
        private static bool IAPUpgradeIapResendRequestFlag = false;
        private static uint IAPUpgradeIapResendCount = 0;
        private static uint IAPUpgradeIapResendMaximumCount = 10;

        private static bool IAPUpgradeAppLocker = false;
        private static bool IAPUpgradeAppTransmitLocker = false;
        private static uint IAPUpgradeAppStartAddress = 0x08010000;
        private static uint IAPUpgradeAppSize = 0;
        private static uint IAPUpgradeAppMonitorCount = 0;
        private static uint IAPUpgradeAppMonitorMaximumCount = 100 * 2 /* 2s */;
        private static bool IAPUpgradeAppResendRequestFlag = false;
        private static uint IAPUpgradeAppResendCount = 0;
        private static uint IAPUpgradeAppResendMaximumCount = 10;

        //-------------------------------------------- Variable Definition (IAP) -------------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (App) -------------------------------------------------------------------------------

        /// @brief The status of Application(APP) process ["应用程序(APP)"状态结构体]
        public class StatusApp
        {
            /// @brief Hardware version [硬件版本]
            public static uint HardwareVersion = 0;

            /// @brief Software version [软件版本]
            public static uint SoftwareVersion = 0;

            /// @brief SN number [SN 号]
            /// 
            /// @note 该 SN 号为 STM32 的 SN 号，并非公司自定值
            public static uint SerialNumber = 0;

            /// @brief Robot type [机器人类型]
            public static DynaLinkHSPara.RobotType RobotType = DynaLinkHSPara.RobotType.None;

            /// @brief Mechanism type [机械类型]
            public static DynaLinkHSPara.MechanismType MechanismType = DynaLinkHSPara.MechanismType.None;
        }

        //-------------------------------------------- Variable Definition (App) -------------------------------------------------------------------------------
        
        //-------------------------------------------- Variable Definition (Flag) ------------------------------------------------------------------------------

        /// @brief The flag of different status [不同状态的标志位]
        public class StatusFlag
        {
            /// @brief The flag of server link active [MMU 服务器连接状态标志位]
            /// @retval 1 The server is active [服务器已连接]
            /// @retval 0 The server is down [服务器未连接]
            public static uint FlagServerLinkActive = 0;

            /// @brief The flag of calibration [机器校准标志位]
            /// @retval 1 The calibration finished [机器未完成校准]
            /// @retval 0 The calibration is undone [机器已完成校准]
            public static uint FlagCalibration = 0;

            /// @brief The flag of servo on [电机使能标志位]
            /// @retval 1 The servo is on [电机使能]
            /// @retval 0 The servo is off [电机失能]
            public static uint FlagServoOn = 0;

            /// @brief The flag of emergent stop [急停标志位]
            /// @retval 1 The emergent stop button is pressed [急停开关按下]
            /// @retval 0 The emergent stop button is not pressed [急停开关未按下]
            public static uint FlagEmergentStop = 0;

            /// @brief The flag of fault [错误标志位]
            /// @retval 1 The driver is in fault status [驱动器处于错误状态]
            /// @retval 0 The driver is not in fault status [驱动器处于正常状态]
            public static uint FlagFault = 0;

            /// @brief The flag of task in process [任务执行中标志位]
            /// @retval 1 The task is in process [任务执行中]
            /// @retval 0 The task is not in process [未执行任务]
            public static uint FlagTaskInProcess = 0;

            /// @brief The flag of robot moving [机器人运动标志位]
            /// @retval 1 The robot is moving [机器人在运动]
            /// @retval 0 The robot is not moving [机器人未运动]
            public static uint FlagRobotMoving = 0;

            /// @brief The flag of robot moving [机器人关节超过动力限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfJointKineticLimit = 0;

            /// @brief The flag of robot moving [机器人关节超过加速度限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfJointAccelerationLimit = 0;

            /// @brief The flag of robot moving [机器人关节超过速度限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfJointVelocityLimit = 0;

            /// @brief The flag of robot moving [机器人关节超过位置限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfJointPositionLimit = 0;

            /// @brief The flag of robot moving [机器人末端超过动力限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfEndEffectorKineticLimit = 0;

            /// @brief The flag of robot moving [机器人末端超过加速度限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfEndEffectorAccelerationLimit = 0;

            /// @brief The flag of robot moving [机器人末端超过速度限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfEndEffectorVelocityLimit = 0;

            /// @brief The flag of robot moving [机器人末端超过位置限制标志位]
            /// @retval 1 The robot is moving [超过限制]
            /// @retval 0 The robot is not moving [未超过限制]
            public static uint FlagOutOfEndEffectorPositionLimit = 0;
        }

        //-------------------------------------------- Variable Definition (Flag) ------------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (Motor) -----------------------------------------------------------------------------

        /// @brief The error code of the motor [机器人电机的错误码]
        /// 
        /// @note Not used right now [暂未使用]
        public class StatusMotorErrorCode
        {
            public static uint Motor1 = 0;
            public static uint Motor2 = 0;
            public static uint Motor3 = 0;
            public static uint Motor4 = 0;
            public static uint Motor5 = 0;
            public static uint Motor6 = 0;
        }

        //-------------------------------------------- Variable Definition (Motor) -----------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (Robot) -----------------------------------------------------------------------------

        /// @brief The status of the robot [机器人的状态]
        public class StatusRobot
        {
            // accessible of driver
            public struct Driver1
            {
                public static uint Installed = 0; ///< The installed state of the robot driver 1 [驱动器 1 安装状态]
                public static uint Accessible = 0; ///< The access state of the robot driver 1 [驱动器 1 通信状态]
            }

            public struct Driver2
            {
                public static uint Installed = 0; ///< The installed state of the robot driver 2 [驱动器 2 安装状态]
                public static uint Accessible = 0; ///< The access state of the robot driver 2 [驱动器 2 通信状态]
            }

            public struct Driver3
            {
                public static uint Installed = 0; ///< The installed state of the robot driver 3 [驱动器 3 安装状态]
                public static uint Accessible = 0; ///< The access state of the robot driver 3 [驱动器 3 通信状态]
            }

            public struct Driver4
            {
                public static uint Installed = 0; ///< The installed state of the robot driver 4 [驱动器 4 安装状态]
                public static uint Accessible = 0; ///< The access state of the robot driver 4 [驱动器 4 通信状态]
            }

            public struct Driver5
            {
                public static uint Installed = 0; ///< The installed state of the robot driver 5 [驱动器 5 安装状态]
                public static uint Accessible = 0; ///< The access state of the robot driver 5 [驱动器 5 通信状态]
            }

            public struct Driver6
            {
                public static uint Installed = 0; ///< The installed state of the robot driver 6 [驱动器 6 安装状态]
                public static uint Accessible = 0; ///< The access state of the robot driver 6 [驱动器 6 通信状态]
            }

            // inforamtion of joint
            public static float PositionDataJoint1 = 0; ///< The position value of the robot joint 1 [关节 1 位置]
            public static float PositionDataJoint2 = 0; ///< The position value of the robot joint 2 [关节 2 位置]
            public static float PositionDataJoint3 = 0; ///< The position value of the robot joint 3 [关节 3 位置]
            public static float PositionDataJoint4 = 0; ///< The position value of the robot joint 4 [关节 4 位置]
            public static float PositionDataJoint5 = 0; ///< The position value of the robot joint 5 [关节 5 位置]
            public static float PositionDataJoint6 = 0; ///< The position value of the robot joint 6 [关节 6 位置]

            public static float VelocityDataJoint1 = 0; ///< The velocity value of the robot joint 1 [关节 1 速度]
            public static float VelocityDataJoint2 = 0; ///< The velocity value of the robot joint 2 [关节 2 速度]
            public static float VelocityDataJoint3 = 0; ///< The velocity value of the robot joint 3 [关节 3 速度]
            public static float VelocityDataJoint4 = 0; ///< The velocity value of the robot joint 4 [关节 4 速度]
            public static float VelocityDataJoint5 = 0; ///< The velocity value of the robot joint 5 [关节 5 速度]
            public static float VelocityDataJoint6 = 0; ///< The velocity value of the robot joint 6 [关节 6 速度]

            public static float AccelerationDataJoint1 = 0; ///< The acceleration value of the robot joint 1 [关节 1 加速度]
            public static float AccelerationDataJoint2 = 0; ///< The acceleration value of the robot joint 2 [关节 2 加速度]
            public static float AccelerationDataJoint3 = 0; ///< The acceleration value of the robot joint 3 [关节 3 加速度]
            public static float AccelerationDataJoint4 = 0; ///< The acceleration value of the robot joint 4 [关节 4 加速度]
            public static float AccelerationDataJoint5 = 0; ///< The acceleration value of the robot joint 5 [关节 5 加速度]
            public static float AccelerationDataJoint6 = 0; ///< The acceleration value of the robot joint 6 [关节 6 加速度]

            public static float KineticDataJoint1 = 0; ///< The kinetic value of the robot joint 1 [关节 1 力/力矩]
            public static float KineticDataJoint2 = 0; ///< The kinetic value of the robot joint 2 [关节 2 力/力矩]
            public static float KineticDataJoint3 = 0; ///< The kinetic value of the robot joint 3 [关节 3 力/力矩]
            public static float KineticDataJoint4 = 0; ///< The kinetic value of the robot joint 4 [关节 4 力/力矩]
            public static float KineticDataJoint5 = 0; ///< The kinetic value of the robot joint 5 [关节 5 力/力矩]
            public static float KineticDataJoint6 = 0; ///< The kinetic value of the robot joint 6 [关节 6 力/力矩]

            // information of end effector
            public static float PositionDataEndEffectorX1 = 0; ///< The position value of the robot end effector x 1 [末端 1 位姿 position x]
            public static float PositionDataEndEffectorY1 = 0; ///< The position value of the robot end effector y 1 [末端 1 位姿 position y]
            public static float PositionDataEndEffectorZ1 = 0; ///< The position value of the robot end effector z 1 [末端 1 位姿 position z]
            public static float PositionDataEndEffectorAlpha1 = 0; ///< The position value of the robot end effector alpha 1 [末端 1 位姿 position alpha]
            public static float PositionDataEndEffectorBeta1 = 0; ///< The position value of the robot end effector beta 1 [末端 1 位姿 position beta]
            public static float PositionDataEndEffectorGamma1 = 0; ///< The position value of the robot end effector gamma 1 [末端 1 位姿 position gamma]

            public static float VelocityDataEndEffectorX1 = 0; ///< The velocity value of the robot end effector x 1 [末端 1 位姿 velocity x]
            public static float VelocityDataEndEffectorY1 = 0; ///< The velocity value of the robot end effector y 1 [末端 1 位姿 velocity y]
            public static float VelocityDataEndEffectorZ1 = 0; ///< The velocity value of the robot end effector z 1 [末端 1 位姿 velocity z]
            public static float VelocityDataEndEffectorAlpha1 = 0; ///< The velocity value of the robot end effector alpha 1 [末端 1 位姿 velocity alpha]
            public static float VelocityDataEndEffectorBeta1 = 0; ///< The velocity value of the robot end effector beta 1 [末端 1 位姿 velocity beta]
            public static float VelocityDataEndEffectorGamma1 = 0; ///< The velocity value of the robot end effector gamma 1 [末端 1 位姿 velocity gamma]

            public static float AccelerationDataEndEffectorX1 = 0; ///< The acceleration value of the robot end effector x 1 [末端 1 位姿 acceleration x]
            public static float AccelerationDataEndEffectorY1 = 0; ///< The acceleration value of the robot end effector y 1 [末端 1 位姿 acceleration y]
            public static float AccelerationDataEndEffectorZ1 = 0; ///< The acceleration value of the robot end effector z 1 [末端 1 位姿 acceleration z]
            public static float AccelerationDataEndEffectorAlpha1 = 0; ///< The acceleration value of the robot end effector alpha 1 [末端 1 位姿 acceleration alpha]
            public static float AccelerationDataEndEffectorBeta1 = 0; ///< The acceleration value of the robot end effector beta 1 [末端 1 位姿 acceleration beta]
            public static float AccelerationDataEndEffectorGamma1 = 0; ///< The acceleration value of the robot end effector gamma 1 [末端 1 位姿 acceleration gamma]

            public static float KineticDataEndEffectorX1 = 0; ///< The kinetic value of the robot end effector x 1 [末端 1 位姿 kinetic x]
            public static float KineticDataEndEffectorY1 = 0; ///< The kinetic value of the robot end effector y 1 [末端 1 位姿 kinetic y]
            public static float KineticDataEndEffectorZ1 = 0; ///< The kinetic value of the robot end effector z 1 [末端 1 位姿 kinetic z]
            public static float KineticDataEndEffectorAlpha1 = 0; ///< The kinetic value of the robot end effector alpha 1 [末端 1 位姿 kinetic alpha]
            public static float KineticDataEndEffectorBeta1 = 0; ///< The kinetic value of the robot end effector beta 1 [末端 1 位姿 kinetic beta]
            public static float KineticDataEndEffectorGamma1 = 0; ///< The kinetic value of the robot end effector gamma 1 [末端 1 位姿 kinetic gamma]

            public static float PositionDataEndEffectorX2 = 0; ///< The position value of the robot end effector x 2 [末端 2 位姿 position x]
            public static float PositionDataEndEffectorY2 = 0; ///< The position value of the robot end effector y 2 [末端 2 位姿 position y]
            public static float PositionDataEndEffectorZ2 = 0; ///< The position value of the robot end effector z 2 [末端 2 位姿 position z]
            public static float PositionDataEndEffectorAlpha2 = 0; ///< The position value of the robot end effector alpha 2 [末端 2 位姿 position alpha]
            public static float PositionDataEndEffectorBeta2 = 0; ///< The position value of the robot end effector beta 2 [末端 2 位姿 position beta]
            public static float PositionDataEndEffectorGamma2 = 0; ///< The position value of the robot end effector gamma 2 [末端 2 位姿 position gamma]

            public static float VelocityDataEndEffectorX2 = 0; ///< The velocity value of the robot end effector x 2 [末端 2 位姿 velocity x]
            public static float VelocityDataEndEffectorY2 = 0; ///< The velocity value of the robot end effector y 2 [末端 2 位姿 velocity y]
            public static float VelocityDataEndEffectorZ2 = 0; ///< The velocity value of the robot end effector z 2 [末端 2 位姿 velocity z]
            public static float VelocityDataEndEffectorAlpha2 = 0; ///< The velocity value of the robot end effector alpha 2 [末端 2 位姿 velocity alpha]
            public static float VelocityDataEndEffectorBeta2 = 0; ///< The velocity value of the robot end effector beta 2 [末端 2 位姿 velocity beta]
            public static float VelocityDataEndEffectorGamma2 = 0; ///< The velocity value of the robot end effector gamma 2 [末端 2 位姿 velocity gamma]

            public static float AccelerationDataEndEffectorX2 = 0; ///< The acceleration value of the robot end effector x 2 [末端 2 位姿 acceleration x]
            public static float AccelerationDataEndEffectorY2 = 0; ///< The acceleration value of the robot end effector y 2 [末端 2 位姿 acceleration y]
            public static float AccelerationDataEndEffectorZ2 = 0; ///< The acceleration value of the robot end effector z 2 [末端 2 位姿 acceleration z]
            public static float AccelerationDataEndEffectorAlpha2 = 0; ///< The acceleration value of the robot end effector alpha 2 [末端 2 位姿 acceleration alpha]
            public static float AccelerationDataEndEffectorBeta2 = 0; ///< The acceleration value of the robot end effector beta 2 [末端 2 位姿 acceleration beta]
            public static float AccelerationDataEndEffectorGamma2 = 0; ///< The acceleration value of the robot end effector gamma 2 [末端 2 位姿 acceleration gamma]

            public static float KineticDataEndEffectorX2 = 0; ///< The kinetic value of the robot end effector x 2 [末端 2 位姿 kinetic x]
            public static float KineticDataEndEffectorY2 = 0; ///< The kinetic value of the robot end effector y 2 [末端 2 位姿 kinetic y]
            public static float KineticDataEndEffectorZ2 = 0; ///< The kinetic value of the robot end effector z 2 [末端 2 位姿 kinetic z]
            public static float KineticDataEndEffectorAlpha2 = 0; ///< The kinetic value of the robot end effector alpha 2 [末端 2 位姿 kinetic alpha]
            public static float KineticDataEndEffectorBeta2 = 0; ///< The kinetic value of the robot end effector beta 2 [末端 2 位姿 kinetic beta]
            public static float KineticDataEndEffectorGamma2 = 0; ///< The kinetic value of the robot end effector gamma 2 [末端 2 位姿 kinetic gamma]

            // limitation group
            public struct PositionLimitationDataJoint1
            {
                public static float Min = 0; ///< The minimum position value of the robot joint 1 [关节 1 允许的最小位置]
                public static float Max = 0; ///< The maximum position value of the robot joint 1 [关节 1 允许的最大位置]
            }
            public struct PositionLimitationDataJoint2
            {
                public static float Min = 0; ///< The minimum position value of the robot joint 2 [关节 2 允许的最小位置]
                public static float Max = 0; ///< The maximum position value of the robot joint 2 [关节 2 允许的最大位置]
            }
            public struct PositionLimitationDataJoint3
            {
                public static float Min = 0; ///< The minimum position value of the robot joint 3 [关节 3 允许的最小位置]
                public static float Max = 0; ///< The maximum position value of the robot joint 3 [关节 3 允许的最大位置]
            }
            public struct PositionLimitationDataJoint4
            {
                public static float Min = 0; ///< The minimum position value of the robot joint 4 [关节 4 允许的最小位置]
                public static float Max = 0; ///< The maximum position value of the robot joint 4 [关节 4 允许的最大位置]
            }
            public struct PositionLimitationDataJoint5
            {
                public static float Min = 0; ///< The minimum position value of the robot joint 5 [关节 5 允许的最小位置]
                public static float Max = 0; ///< The maximum position value of the robot joint 5 [关节 5 允许的最大位置]
            }
            public struct PositionLimitationDataJoint6
            {
                public static float Min = 0; ///< The minimum position value of the robot joint 6 [关节 6 允许的最小位置]
                public static float Max = 0; ///< The maximum position value of the robot joint 6 [关节 6 允许的最大位置]
            }

            public struct PositionLimitationDataEndEffectorX1
            {
                public static float Min = 0; ///< The minimum position value of the robot end effector x1 [末端 x1 允许的最小位置]
                public static float Max = 0; ///< The maximun position value of the robot end effector x1 [末端 x1 允许的最大位置]
            }
            public struct PositionLimitationDataEndEffectorY1
            {
                public static float Min = 0; ///< The minimum position value of the robot end effector y1 [末端 y1 允许的最小位置]
                public static float Max = 0; ///< The maximun position value of the robot end effector y1 [末端 y1 允许的最大位置]
            }
            public struct PositionLimitationDataEndEffectorZ1
            {
                public static float Min = 0; ///< The minimum position value of the robot end effector z1 [末端 z1 允许的最小位置]
                public static float Max = 0; ///< The maximun position value of the robot end effector z1 [末端 z1 允许的最大位置]
            }
            public struct PositionLimitationDataEndEffectorAlpha1
            {
                public static float Min = 0; ///< The minimum position value of the robot end effector alpha1 [末端 alpha1 允许的最小位置]
                public static float Max = 0; ///< The maximun position value of the robot end effector alpha1 [末端 alpha1 允许的最大位置]
            }
            public struct PositionLimitationDataEndEffectorBeta1
            {
                public static float Min = 0; ///< The minimum position value of the robot end effector beta1 [末端 beta1 允许的最小位置]
                public static float Max = 0; ///< The maximun position value of the robot end effector beta1 [末端 beta1 允许的最大位置]
            }
            public struct PositionLimitationDataEndEffectorGamma1
            {
                public static float Min = 0; ///< The minimum position value of the robot end effector gamma1 [末端 gamma1 允许的最小位置]
                public static float Max = 0; ///< The maximun position value of the robot end effector gamma1 [末端 gamma1 允许的最大位置]
            }
        }

        /// @brief The sensor value [传感器状态]
        public class StatusSensor
        {
            /// @brief The value of button sensor 1 [按钮传感器1的数值]
            public struct ButtonSensor1
            {
                public static uint Installed = 0; ///< The installed state of the button sensor 1 [按钮传感器 1 安装状态]
                public static uint Accessible = 0;  ///< The access state of the button sensor 1 [按钮传感器 1 通信状态]
                public static float Value = 0; ///< The value of the button sensor 1 [按钮传感器 1 值]
            }

            /// @brief The value of button sensor 2 [按钮传感器2的数值]
            public struct ButtonSensor2
            {
                public static uint Installed = 0; ///< The installed state of the button sensor 2 [按钮传感器 2 安装状态]
                public static uint Accessible = 0; ///< The access state of the button sensor 2 [按钮传感器 2 通信状态]
                public static float Value = 0; ///< The value of the button sensor 2 [按钮传感器 2 值]
            }

            /// @brief The value of button sensor 3 [按钮传感器3的数值]
            public struct ButtonSensor3
            {
                public static uint Installed = 0; ///< The installed state of the button sensor 3 [按钮传感器 3 安装状态]
                public static uint Accessible = 0; ///< The access state of the button sensor 3 [按钮传感器 3 通信状态]
                public static float Value = 0; ///< The value of the button sensor 3 [按钮传感器 3 值]
            }

            /// @brief The value of button sensor 4 [按钮传感器4的数值]
            public struct ButtonSensor4
            {
                public static uint Installed = 0; ///< The installed state of the button sensor 4 [按钮传感器 4 安装状态]
                public static uint Accessible = 0; ///< The access state of the button sensor 4 [按钮传感器 4 通信状态]
                public static float Value = 0; ///< The value of the button sensor 4 [按钮传感器 4 值]
            }

            /// @brief The value of button sensor 5 [按钮传感器5的数值]
            public struct ButtonSensor5
            {
                public static uint Installed = 0; ///< The installed state of the button sensor 5 [按钮传感器 5 安装状态]
                public static uint Accessible = 0; ///< The access state of the button sensor 5 [按钮传感器 5 通信状态]
                public static float Value = 0; ///< The value of the button sensor 5 [按钮传感器 5 值]
            }

            /// @brief The value of button sensor 6 [按钮传感器6的数值]
            public struct ButtonSensor6
            {
                public static uint Installed = 0; ///< The installed state of the button sensor 6 [按钮传感器 6 安装状态]
                public static uint Accessible = 0; ///< The access state of the button sensor 6 [按钮传感器 6 通信状态]
                public static float Value = 0; ///< The value of the button sensor 6 [按钮传感器 6 值]
            }
            
            /// @brief The value of adc sensor 1 [模拟-数字传感器1的数值]
            public struct ADCSensor1
            {
                public static uint Installed = 0; ///< The installed state of the adc sensor 1 [ADC传感器 1 安装状态]
                public static uint Accessible = 0; ///< The access state of the adc sensor 1 [ADC传感器 1 通信状态]
                public static float RawValue = 0; ///< The raw value of the adc sensor 1 [按钮传感器 1 原始值]
                public static float CalculateValue = 0; ///< The calculate value of the adc sensor 1 [按钮传感器 1 校准值]
            }

            /// @brief The value of adc sensor 2 [模拟-数字传感器2的数值]
            public struct ADCSensor2
            {
                public static uint Installed = 0; ///< The installed state of the adc sensor 2 [ADC传感器 2 安装状态]
                public static uint Accessible = 0; ///< The access state of the adc sensor 2 [ADC传感器 2 通信状态]
                public static float RawValue = 0; ///< The raw value of the adc sensor 2 [按钮传感器 2 原始值]
                public static float CalculateValue = 0; ///< The calculate value of the adc sensor 2 [按钮传感器 2 校准值]
            }

            /// @brief The value of adc sensor 3 [模拟-数字传感器3的数值]
            public struct ADCSensor3
            {
                public static uint Installed = 0; ///< The installed state of the adc sensor 3 [ADC传感器 3 安装状态]
                public static uint Accessible = 0; ///< The access state of the adc sensor 3 [ADC传感器 3 通信状态]
                public static float RawValue = 0; ///< The raw value of the adc sensor 3 [按钮传感器 3 原始值]
                public static float CalculateValue = 0; ///< The calculate value of the adc sensor 3 [按钮传感器 3 校准值]
            }

            /// @brief The value of adc sensor 4 [模拟-数字传感器4的数值]
            public struct ADCSensor4
            {
                public static uint Installed = 0; ///< The installed state of the adc sensor 4 [ADC传感器 4 安装状态]
                public static uint Accessible = 0; ///< The access state of the adc sensor 4 [ADC传感器 4 通信状态]
                public static float RawValue = 0; ///< The raw value of the adc sensor 4 [按钮传感器 4 原始值]
                public static float CalculateValue = 0; ///< The calculate value of the adc sensor 4 [按钮传感器 4 校准值]
            }

            /// @brief The value of adc sensor 5 [模拟-数字传感器5的数值]
            public struct ADCSensor5
            {
                public static uint Installed = 0; ///< The installed state of the adc sensor 5 [ADC传感器 5 安装状态]
                public static uint Accessible = 0; ///< The access state of the adc sensor 5 [ADC传感器 5 通信状态]
                public static float RawValue = 0; ///< The raw value of the adc sensor 1 [按钮传感器 5 原始值]
                public static float CalculateValue = 0; ///< The calculate value of the adc sensor 5 [按钮传感器 5 校准值]
            }

            /// @brief The value of adc sensor 6 [模拟-数字传感器6的数值]
            public struct ADCSensor6
            {
                public static uint Installed = 0; ///< The installed state of the adc sensor 6 [ADC传感器 6 安装状态]
                public static uint Accessible = 0; ///< The access state of the adc sensor 6 [ADC传感器 6 通信状态]
                public static float RawValue = 0; ///< The raw value of the adc sensor 6 [按钮传感器 6 原始值]
                public static float CalculateValue = 0; ///< The calculate value of the adc sensor 6 [按钮传感器 6 校准值]
            }
        }

        /// @brief The status of circuit board digit input [电路板数字输入口的状态]
        /// 
        /// @note Not used right now [暂未使用]
        public class StatusDigiInput
        {
            public static uint[] IDL = new uint[4]{0, 0, 0, 0};
        }

        /// @brief The status of circuit board digit output [电路板数字输出口的状态]
        /// 
        /// @note Not used right now [暂未使用]
        public class StatusDigiOutput
        {
            public static uint[] ODL = new uint[4]{0, 0, 0, 0};
        }

        //-------------------------------------------- Variable Definition (Robot) -----------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (Network) ---------------------------------------------------------------------------

        private static uint ServerLinkMonitorCount = 0;
        private static uint ServerLinkMonitorMaximumCount = 3;

        private static Timer ServerLinkMonitorTimer;
        private static uint ServerLinkMonitorTimerPeriodTime = 1000;
        private static TimerCallback ServerLinkMonitorTimerCallback = new TimerCallback(ServerLinkMonitorTimerCallbackFunction);
        private static bool ServerLinkMonitorTimerLocker = false;
        private static bool ServerLinkMonitorTimerActiveFlag = false;

        private static Timer IAPTimer;
        private static uint IAPTimerPeriodTime = 1000;
        private static TimerCallback IAPTimerCallback = new TimerCallback(IAPTimerCallbackFunction);
        private static bool IAPTimerLocker = false;
        private static bool IAPTimerActiveFlag = false;

        private static Timer SystemPeriodicFunctionTimer;
        private static uint SystemPeriodicFunctionTimerPeriodTime = 1000;
        private static TimerCallback SystemPeriodicFunctionTimerCallback = new TimerCallback(SystemPeriodicFunctionTimerCallbackFunction);
        private static bool SystemPeriodicFunctionTimerLocker = false;
        private static bool SystemPeriodicFunctionTimerActiveFlag = false;

        private static Timer HardwareStatusPeriodicFunctionTimer;
        private static uint HardwareStatusPeriodicFunctionTimerPeriodTime = 1000;
        private static TimerCallback HardwarePeriodicFunctionTimerCallback = new TimerCallback(HardwareStatusPeriodicFunctionTimerCallbackFunction);
        private static bool HardwareStatusPeriodicFunctionTimerLocker = false;
        private static bool HardwareStatusPeriodicFunctionTimerActiveFlag = false;

        private static Timer FlagPeriodicFunctionTimer;
        private static uint FlagPeriodicFunctionTimerPeriodTime = 500;
        private static TimerCallback FlagPeriodicFunctionTimerCallback = new TimerCallback(FlagPeriodicFunctionTimerCallbackFunction);
        private static bool FlagPeriodicFunctionTimerLocker = false;
        private static bool FlagPeriodicFunctionTimerActiveFlag = false;

        private static Timer RobotStatusPeriodicFunctionTimer;
        private static uint RobotStatusPeriodicFunctionTimerPeriodTime = 50;
        private static TimerCallback RobotPeriodicFunctionTimerCallback = new TimerCallback(RobotStatusPeriodicFunctionTimerCallbackFunction);
        private static bool RobotStatusPeriodicFunctionTimerLocker = false;
        private static bool RobotStatusPeriodicFunctionTimerActiveFlag = false;

        private static Timer RobotSensorStatusPeriodicFunctionTimer;
        private static uint RobotSensorStatusPeriodicFunctionTimerPeriodTime = 200;
        private static TimerCallback RobotSensorPeriodicFunctionTimerCallback = new TimerCallback(RobotSensorStatusPeriodicFunctionTimerCallbackFunction);
        private static bool RobotSensorStatusPeriodicFunctionTimerLocker = false;
        private static bool RobotSensorStatusPeriodicFunctionTimerActiveFlag = false;

        //-------------------------------------------- Variable Definition (Network) ---------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (V1) --------------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (V2) --------------------------------------------------------------------------------

        //-------------------------------------------- Variable Definition (V2) --------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Model) -----------------------------------------------------------------------------

        private static FunctionResult ResetModelParameters()
        {
            // System
            StatusApp.HardwareVersion = 0;
            StatusApp.SoftwareVersion = 0;
            StatusApp.SerialNumber = 0;
            StatusApp.RobotType = DynaLinkHSPara.RobotType.None;
            StatusApp.MechanismType = DynaLinkHSPara.MechanismType.None;

            // Flag
            StatusFlag.FlagCalibration = 0;
            StatusFlag.FlagEmergentStop = 0;
            StatusFlag.FlagFault = 0;
            StatusFlag.FlagRobotMoving = 0;
            StatusFlag.FlagServerLinkActive = 0;
            StatusFlag.FlagServoOn = 0;
            StatusFlag.FlagTaskInProcess = 0;

            // Motor Error Code
            StatusMotorErrorCode.Motor1 = 0;
            StatusMotorErrorCode.Motor2 = 0;
            StatusMotorErrorCode.Motor3 = 0;
            StatusMotorErrorCode.Motor4 = 0;
            StatusMotorErrorCode.Motor5 = 0;
            StatusMotorErrorCode.Motor6 = 0;

            return FunctionResult.Success;
        }

        private static FunctionResult StatusRobotUpdateNotification()
        {
            // set flag
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.None:
                    break;
                case DynaLinkHSPara.RobotType.M1:
                    if (StatusRobot.VelocityDataJoint1 == 0)
                    {
                        StatusFlag.FlagRobotMoving = 0;
                    }
                    else
                    {
                        StatusFlag.FlagRobotMoving = 1;
                    }

                    break;
                case DynaLinkHSPara.RobotType.M2:
                    if (StatusRobot.VelocityDataJoint1 == 0
                    && StatusRobot.VelocityDataJoint2 == 0)
                    {
                        StatusFlag.FlagRobotMoving = 0;
                    }
                    else
                    {
                        StatusFlag.FlagRobotMoving = 1;
                    }
                    
                    break;
                case DynaLinkHSPara.RobotType.X1:
                    break;
                case DynaLinkHSPara.RobotType.X2:
                    break;
                case DynaLinkHSPara.RobotType.All:
                    break;
                default:
                    break;
            }

            return FunctionResult.Success;
        }

        // -------------------------------------------- Function Definition (Model) -----------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Network) ---------------------------------------------------------------------------

        /// @brief Connect to the MMU [连接 MMU 服务器]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult Connect()
        {
            FunctionResult functionResult;

            // reset model parameters
            ResetModelParameters();

            // add observer
            FFTAICommunicationManager.Instance.FFTAICommunicationOperation.AddObserver(DynaLinkHS.Instance);

            // connect
            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationOperation.Connect(StatusNetwork.ServerAddressIp, StatusNetwork.ServerAddressPort, NetworkConnectionType.TCP);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                // remove observer
                FFTAICommunicationManager.Instance.FFTAICommunicationOperation.RemoveObserver(DynaLinkHS.Instance);

                return FunctionResult.Fail;
            }

            // add observer
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.AddObserver(DynaLinkHS.Instance);

            FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2CommunicationInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2RobotInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.AddObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.AddObserver(DynaLinkHS.Instance);

            // start server monitor
            CmdInitHeartBeat();

            // start periodic sending
            CmdInitPeriodicFunction();

            // set connection flag
            StatusFlag.FlagServerLinkActive = 0x01;

            return FunctionResult.Success;
        }

        /// @brief Disconnect to the MMU [断开 MMU 服务器]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult Disconnect()
        {
            // stop server monitor
            CmdDeInitHeartBeat();

            // stop periodic sending
            CmdDeInitPeriodicFunction();

            // disconnect
            FFTAICommunicationManager.Instance.FFTAICommunicationOperation.Disconnect();

            // remove observer
            FFTAICommunicationManager.Instance.FFTAICommunicationOperation.RemoveObserver(DynaLinkHS.Instance);

            // remove observer
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RemoveObserver(DynaLinkHS.Instance);

            FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2CommunicationInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2RobotInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RemoveObserver(DynaLinkHS.Instance);
            FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RemoveObserver(DynaLinkHS.Instance);

            // reset connection flag
            StatusFlag.FlagServerLinkActive = 0x00;

            return FunctionResult.Success;
        }

        private static FunctionResult CmdInitHeartBeat()
        {
            if (ServerLinkMonitorTimer == null)
            {

                // clear server monitor count
                ServerLinkMonitorCount = 0;

                // clear active flag
                ServerLinkMonitorTimerActiveFlag = false;

                // create timer
                ServerLinkMonitorTimer =
         			new Timer(ServerLinkMonitorTimerCallback, null, 0, ServerLinkMonitorTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (ServerLinkMonitorTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
                // clear server monitor count
                ServerLinkMonitorCount = 0;

                // clear active flag
                ServerLinkMonitorTimerActiveFlag = false;

                // ceate timer
                ServerLinkMonitorTimer.Change(0, ServerLinkMonitorTimerPeriodTime);
            }

            return FunctionResult.Success;
        }

        private static FunctionResult CmdDeInitHeartBeat()
        {
            // heart beat
            if (ServerLinkMonitorTimer != null)
            {
                ServerLinkMonitorTimer.Dispose();
                ServerLinkMonitorTimer = null;
            }

            ServerLinkMonitorTimerActiveFlag = false;

            return FunctionResult.Success;
        }

        private static FunctionResult CmdInitPeriodicFunction()
        {
            // iap
            if (IAPTimer == null)
            {
                // clear active flag
                IAPTimerActiveFlag = false;

                // ceate timer
                IAPTimer = new Timer(IAPTimerCallback, null, 0, IAPTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (IAPTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
                // clear active flag
                IAPTimerActiveFlag = false;

                // ceate timer
                IAPTimer.Change(0, IAPTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (IAPTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }

            // thread sleep some time, make sure the boot mode and work status have been read
            Thread.Sleep(10);

            if (StatusIAP.IAPWorkStatus == DynaLinkHSPara.IAPWorkStatus.IAP)
            {
                return FunctionResult.Success;
            }

            // system periodic function
            if (SystemPeriodicFunctionTimer == null)
            {
                // clear active flag
                SystemPeriodicFunctionTimerActiveFlag = false;

                // create timer
                SystemPeriodicFunctionTimer =
					new Timer(SystemPeriodicFunctionTimerCallback, null, 0, SystemPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (SystemPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
                // clear active flag
                SystemPeriodicFunctionTimerActiveFlag = false;

                // create timer
                SystemPeriodicFunctionTimer.Change(0, SystemPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (SystemPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }

            // thread sleep some time, make sure the robot type and mechanism type have been read
            Thread.Sleep(10);

            // hardware information
            if (HardwareStatusPeriodicFunctionTimer == null)
            {
                // clear active flag
                HardwareStatusPeriodicFunctionTimerActiveFlag = false;

                // create timer
                HardwareStatusPeriodicFunctionTimer =
                    new Timer(HardwarePeriodicFunctionTimerCallback, null, 0, HardwareStatusPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (HardwareStatusPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
                // clear active flag
                HardwareStatusPeriodicFunctionTimerActiveFlag = false;

                // create timer
                HardwareStatusPeriodicFunctionTimer.Change(0, HardwareStatusPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (HardwareStatusPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }

            // thread sleep some time, make sure the robot type and mechanism type have been read
            Thread.Sleep(10);

            // robot information
            // flag periodic function
            if (FlagPeriodicFunctionTimer == null)
            {
                // clear active flag
                FlagPeriodicFunctionTimerActiveFlag = false;

                // create timer
                FlagPeriodicFunctionTimer =
					new Timer(FlagPeriodicFunctionTimerCallback, null, 0, FlagPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (FlagPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
				
                // clear active flag
                FlagPeriodicFunctionTimerActiveFlag = false;

                // create timer
                FlagPeriodicFunctionTimer.Change(0, FlagPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (FlagPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }

            // thread sleep some time, make sure the robot type and mechanism type have been read
            Thread.Sleep(10);

            // robot status periodic function
            if (RobotStatusPeriodicFunctionTimer == null)
            {
                // clear active flag
                RobotStatusPeriodicFunctionTimerActiveFlag = false;

                // create timer
                RobotStatusPeriodicFunctionTimer =
                    new Timer(RobotPeriodicFunctionTimerCallback, null, 0, RobotStatusPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (RobotStatusPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
                // clear active flag
                RobotStatusPeriodicFunctionTimerActiveFlag = false;

                // create timer
                RobotStatusPeriodicFunctionTimer.Change(0, RobotStatusPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (RobotStatusPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }

            // thread sleep some time, make sure the robot type and mechanism type have been read
            Thread.Sleep(10);

            // robot sensor status periodic function
            if (RobotSensorStatusPeriodicFunctionTimer == null)
            {
                // clear active flag
                RobotSensorStatusPeriodicFunctionTimerActiveFlag = false;

                // create timer
                RobotSensorStatusPeriodicFunctionTimer =
                    new Timer(RobotSensorPeriodicFunctionTimerCallback, null, 0, RobotSensorStatusPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (RobotSensorStatusPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }
            else
            {
                // clear active flag
                RobotSensorStatusPeriodicFunctionTimerActiveFlag = false;

                // create timer
                RobotSensorStatusPeriodicFunctionTimer.Change(0, RobotSensorStatusPeriodicFunctionTimerPeriodTime);

                // sleep to let main thread give time to create children thread.
                while (RobotSensorStatusPeriodicFunctionTimerActiveFlag == false)
                {
                    Thread.Sleep(10);
                }

            }

            return FunctionResult.Success;
        }

        private static FunctionResult CmdDeInitPeriodicFunction()
        {
            // iap
            if (IAPTimer != null)
            {
                IAPTimer.Dispose();
                IAPTimer = null;
            }

            IAPTimerActiveFlag = false;
            IAPTimerLocker = false;

            IAPUpgradeIapLocker = false;
            IAPUpgradeAppLocker = false;

            // system
            if (SystemPeriodicFunctionTimer != null)
            {
                SystemPeriodicFunctionTimer.Dispose();
                SystemPeriodicFunctionTimer = null;
            }

            SystemPeriodicFunctionTimerActiveFlag = false;
            SystemPeriodicFunctionTimerLocker = false;

            // flag
            if (FlagPeriodicFunctionTimer != null)
            {
                FlagPeriodicFunctionTimer.Dispose();
                FlagPeriodicFunctionTimer = null;
            }

            FlagPeriodicFunctionTimerActiveFlag = false;
            FlagPeriodicFunctionTimerLocker = false;

            // robot status
            if (RobotStatusPeriodicFunctionTimer != null)
            {
                RobotStatusPeriodicFunctionTimer.Dispose();
                RobotStatusPeriodicFunctionTimer = null;
            }

            RobotStatusPeriodicFunctionTimerActiveFlag = false;
            RobotStatusPeriodicFunctionTimerLocker = false;

            // robot sensor status
            if (RobotSensorStatusPeriodicFunctionTimer != null)
            {
                RobotSensorStatusPeriodicFunctionTimer.Dispose();
                RobotSensorStatusPeriodicFunctionTimer = null;
            }

            RobotSensorStatusPeriodicFunctionTimerActiveFlag = false;
            RobotSensorStatusPeriodicFunctionTimerLocker = false;

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Network) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (IAP) -------------------------------------------------------------------------------

        /// @brief Upgrade MMU Iap (bootloader) [更新 MMU Bootloader 程序]
        /// 
        /// @param filePath The file path of the bootloader .bin file [.bin 文件的路径]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult UpgradeMMUIap(
            string filePath)
        {
            // check the upgrade process is locked or not
            if (IAPUpgradeIapLocker == true)
            {
                return FunctionResult.Fail;
            }

            Thread upgradeMMUIapThread = new Thread(new ParameterizedThreadStart(UpgradeMMUIapFastThread));
            upgradeMMUIapThread.Start(filePath);

            // sleep to let main thread give time to create children thread.
            Thread.Sleep(FFTAICommunicationConfig.SLEEP_TIME_FOR_CREATING_CHILDREN_THREAD);

            return FunctionResult.Success;
        }

        private static FunctionResult UpgradeMMUIapThread(
            string filePath)
        {
            // check the upgrade process is locked or not
            if (IAPUpgradeIapLocker == true)
            {
                return FunctionResult.Fail;
            }

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // set progress to 0
            StatusIAP.IAPUpgradeIapProgress = 0;

            // set start address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPStartAddress(IAPUpgradeIapStartAddress);

            // set size
            IAPUpgradeIapSize = (uint)fileStream.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPSize(IAPUpgradeIapSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseIAP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // clear locker
            IAPUpgradeIapTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileStream.Length; i++)
            {
                while (IAPUpgradeIapTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeIapMonitorCount++;

                    if (IAPUpgradeIapMonitorCount > IAPUpgradeIapMonitorMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        IAPUpgradeIapResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // close file
                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        return FunctionResult.Fail;
                    }
                }

                // resend request ?
                if (IAPUpgradeIapResendRequestFlag == true)
                {
                    IAPUpgradeIapResendCount++;

                    if (IAPUpgradeIapResendCount > IAPUpgradeIapResendMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        IAPUpgradeIapResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // close file
                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        return FunctionResult.Fail;
                    }

                    i = i - 1;

                    IAPUpgradeIapResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeIapResendCount = 0;
                }

                int byteData = fileStream.ReadByte();

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeIAP(i, (uint)byteData);

                IAPUpgradeIapMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeIapProgress = 100 * i / IAPUpgradeIapSize;

                // set locker
                IAPUpgradeIapTransmitLocker = true;
            }

            // close file
            fileStream.Close();

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeIapProgress = 100;

            // clear locker
            IAPUpgradeIapLocker = false;

            return FunctionResult.Success;
        }

        private static void UpgradeMMUIapFastThread(object filePath)
        {
            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread Start Running ...");

            FFTAICommunicationManager.Instance.Logger.WriteLine("FilePath = " + filePath.ToString());

            // set locker
            IAPUpgradeIapLocker = true;

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file
            FileStream fileStream = new FileStream(filePath.ToString(), FileMode.Open, FileAccess.Read);

            // set progress to 0
            StatusIAP.IAPUpgradeIapProgress = 0;

            // set start address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPStartAddress(IAPUpgradeIapStartAddress);

            // set size
            IAPUpgradeIapSize = (uint)fileStream.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPSize(IAPUpgradeIapSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseIAP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // set each message contain byte count
            uint IAPUpgradeIapEachRequestSendByte = 200;
            int fileData = 0;
            byte[] byteData = new byte[IAPUpgradeIapEachRequestSendByte];

            // clear locker
            IAPUpgradeIapTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileStream.Length; i = i + IAPUpgradeIapEachRequestSendByte)
            {
                while (IAPUpgradeIapTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeIapMonitorCount++;

                    if (IAPUpgradeIapMonitorCount > IAPUpgradeIapMonitorMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeIapLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread End Running (Not Receive Response) ...");

                        return;
                    }
                }

                // resend request ?
                if (IAPUpgradeIapResendRequestFlag == true)
                {
                    IAPUpgradeIapResendCount++;

                    if (IAPUpgradeIapResendCount > IAPUpgradeIapResendMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        IAPUpgradeIapResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // close file
                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeIapLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread End Running (Resent Exceed Maximum Count) ...");

                        return;
                    }

                    i = i - IAPUpgradeIapEachRequestSendByte;

                    IAPUpgradeIapResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeIapResendCount = 0;
                }

                // send data
                for (uint ii = 0; ii < IAPUpgradeIapEachRequestSendByte; ii++)
                {
                    fileData = fileStream.ReadByte(); // when greater the file size, it will return 0xFF
                    byteData[ii] = (byte)fileData;
                }

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeIAPFast(i, byteData, IAPUpgradeIapEachRequestSendByte);

                IAPUpgradeIapMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeIapProgress = 100 * i / IAPUpgradeIapSize;

                // set locker
                IAPUpgradeIapTransmitLocker = true;
            }

            // close file
            fileStream.Close();

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeIapProgress = 100;

            // clear locker
            IAPUpgradeIapLocker = false;

            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread End Running (Success) ...");

            return;
        }


        /// @brief Upgrade MMU Iap (bootloader) [更新 MMU Bootloader 程序]
        /// 
        /// @param fileBuffer The file buffer of the bootloader .bin file [.bin 文件的数据]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult UpgradeMMUIap(
            byte[] fileBuffer)
        {
            // check the upgrade process is locked or not
            if (IAPUpgradeIapLocker == true)
            {
                return FunctionResult.Fail;
            }

            Thread upgradeMMUIapThread = new Thread(new ParameterizedThreadStart(UpgradeMMUIapFastThread));
            upgradeMMUIapThread.Start(fileBuffer);

            // sleep to let main thread give time to create children thread.
            Thread.Sleep(FFTAICommunicationConfig.SLEEP_TIME_FOR_CREATING_CHILDREN_THREAD);

            return FunctionResult.Success;
        }

        private static FunctionResult UpgradeMMUIapThread(
            byte[] fileBuffer)
        {
            // check the upgrade process is locked or not
            if (IAPUpgradeIapLocker == true)
            {
                return FunctionResult.Fail;
            }

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file

            // set progress to 0
            StatusIAP.IAPUpgradeIapProgress = 0;

            // set start address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPStartAddress(IAPUpgradeIapStartAddress);

            // set size
            IAPUpgradeIapSize = (uint)fileBuffer.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPSize(IAPUpgradeIapSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseIAP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // clear locker
            IAPUpgradeIapTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileBuffer.Length; i++)
            {
                while (IAPUpgradeIapTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeIapMonitorCount++;

                    if (IAPUpgradeIapMonitorCount > IAPUpgradeIapMonitorMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        IAPUpgradeIapResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // close file

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        return FunctionResult.Fail;
                    }
                }

                // resend request ?
                if (IAPUpgradeIapResendRequestFlag == true)
                {
                    IAPUpgradeIapResendCount++;

                    if (IAPUpgradeIapResendCount > IAPUpgradeIapResendMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        IAPUpgradeIapResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // close file

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        return FunctionResult.Fail;
                    }

                    i = i - 1;

                    IAPUpgradeIapResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeIapResendCount = 0;
                }

                int byteData = fileBuffer[i];

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeIAP(i, (uint)byteData);

                IAPUpgradeIapMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeIapProgress = 100 * i / IAPUpgradeIapSize;

                // set locker
                IAPUpgradeIapTransmitLocker = true;
            }

            // close file

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeIapProgress = 100;

            // clear locker
            IAPUpgradeIapLocker = false;

            return FunctionResult.Success;
        }

        private static void UpgradeMMUIapFastThread(
            byte[] fileBuffer)
        {
            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread Start Running ...");

            // set locker
            IAPUpgradeIapLocker = true;

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file

            // set progress to 0
            StatusIAP.IAPUpgradeIapProgress = 0;

            // set start address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPStartAddress(IAPUpgradeIapStartAddress);

            // set size
            IAPUpgradeIapSize = (uint)fileBuffer.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetIAPSize(IAPUpgradeIapSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseIAP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // set each message contain byte count
            uint IAPUpgradeIapEachRequestSendByte = 200;
            int fileData = 0;
            byte[] byteData = new byte[IAPUpgradeIapEachRequestSendByte];

            // clear locker
            IAPUpgradeIapTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileBuffer.Length; i = i + IAPUpgradeIapEachRequestSendByte)
            {
                uint j = i;
                
                while (IAPUpgradeIapTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeIapMonitorCount++;

                    if (IAPUpgradeIapMonitorCount > IAPUpgradeIapMonitorMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeIapLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread End Running (Not Receive Response) ...");

                        return;
                    }
                }

                // resend request ?
                if (IAPUpgradeIapResendRequestFlag == true)
                {
                    IAPUpgradeIapResendCount++;

                    if (IAPUpgradeIapResendCount > IAPUpgradeIapResendMaximumCount)
                    {
                        IAPUpgradeIapMonitorCount = 0;

                        IAPUpgradeIapResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeIapProgress = 0;

                        // close file

                        // set status
                        StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeIapLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread End Running (Resent Exceed Maximum Count) ...");

                        return;
                    }

                    i = i - IAPUpgradeIapEachRequestSendByte;

                    IAPUpgradeIapResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeIapResendCount = 0;
                }

                // send data
                for (uint ii = 0; ii < IAPUpgradeIapEachRequestSendByte; ii++)
                {
                    if (j < fileBuffer.Length)
                    {
                        fileData = fileBuffer[j];
                    }
                    else
                    {
                        fileData = 0xFF; // when greater the file size, it will return 0xFF
                    }

                    j++;

                    byteData[ii] = (byte)fileData;
                }

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeIAPFast(i, byteData, IAPUpgradeIapEachRequestSendByte);

                IAPUpgradeIapMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeIapProgress = 100 * i / IAPUpgradeIapSize;

                // set locker
                IAPUpgradeIapTransmitLocker = true;
            }

            // close file

            // set status
            StatusIAP.IAPUpgradeIapStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeIapProgress = 100;

            // clear locker
            IAPUpgradeIapLocker = false;

            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUIapFastThread End Running (Success) ...");

            return;
        }

        /// @brief Upgrade MMU App (application) [更新 MMU Application 程序]
        /// 
        /// @param filePath The file path of the application .bin file [.bin 文件的路径]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult UpgradeMMUApp(
            string filePath)
        {
            // check the upgrade process is locked or not
            if (IAPUpgradeAppLocker == true)
            {
                return FunctionResult.Fail;
            }

            Thread upgradeMMUAppThread = new Thread(new ParameterizedThreadStart(UpgradeMMUAppFastThread));
            upgradeMMUAppThread.Start(filePath);

            // sleep to let main thread give time to create children thread.
            Thread.Sleep(FFTAICommunicationConfig.SLEEP_TIME_FOR_CREATING_CHILDREN_THREAD);

            return FunctionResult.Success;
        }

        private static void UpgradeMMUAppThread(object filePath)
        {
            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread Start Running ...");

            FFTAICommunicationManager.Instance.Logger.WriteLine("FilePath = " + filePath.ToString());

            // set locker
            IAPUpgradeAppLocker = true;
			
            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file
            FileStream fileStream = new FileStream(filePath.ToString(), FileMode.Open, FileAccess.Read);

            // set progress to 0
            StatusIAP.IAPUpgradeAppProgress = 0;

            // set address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPStartAddress(IAPUpgradeAppStartAddress);

            // set size
            IAPUpgradeAppSize = (uint)fileStream.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPSize(IAPUpgradeAppSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseAPP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // clear locker
            IAPUpgradeAppTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileStream.Length; i++)
            {
                while (IAPUpgradeAppTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeAppMonitorCount++;

                    if (IAPUpgradeAppMonitorCount > IAPUpgradeAppMonitorMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread End Running (Not Receive Response) ...");

                        return;
                    }
                }

                // resend request ?
                if (IAPUpgradeAppResendRequestFlag == true)
                {
                    IAPUpgradeAppResendCount++;

                    if (IAPUpgradeAppResendCount > IAPUpgradeAppResendMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        IAPUpgradeAppResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        // close file
                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread End Running (Resent Exceed Maximum Count) ...");

                        return;
                    }

                    i = i - 1;

                    IAPUpgradeAppResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeAppResendCount = 0;
                }

                int fileData = fileStream.ReadByte();

                byte byteData = (byte)fileData;

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeAPP(i, byteData);

                IAPUpgradeAppMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeAppProgress = 100 * i / IAPUpgradeAppSize;

                // set locker
                IAPUpgradeAppTransmitLocker = true;
            }

            // close file
            fileStream.Close();

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeAppProgress = 100;

            // clear locker
            IAPUpgradeAppLocker = false;

            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread End Running (Success) ...");

            return;
        }

        private static void UpgradeMMUAppFastThread(object filePath)
        {
            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread Start Running ...");

            FFTAICommunicationManager.Instance.Logger.WriteLine("FilePath = " + filePath.ToString());

            // set locker
            IAPUpgradeAppLocker = true;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file
            FileStream fileStream = new FileStream(filePath.ToString(), FileMode.Open, FileAccess.Read);

            // set progress to 0
            StatusIAP.IAPUpgradeAppProgress = 0;

            // set address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPStartAddress(IAPUpgradeAppStartAddress);

            // set size
            IAPUpgradeAppSize = (uint)fileStream.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPSize(IAPUpgradeAppSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseAPP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // set each message contain byte count
            uint IAPUpgradeAppEachRequestSendByte = 200;
            int fileData = 0;
            byte[] byteData = new byte[IAPUpgradeAppEachRequestSendByte];

            // clear locker
            IAPUpgradeAppTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileStream.Length; i = i + IAPUpgradeAppEachRequestSendByte)
            {
                while (IAPUpgradeAppTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeAppMonitorCount++;

                    if (IAPUpgradeAppMonitorCount > IAPUpgradeAppMonitorMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread End Running (Not Receive Response) ...");

                        return;
                    }
                }

                // resend request ?
                if (IAPUpgradeAppResendRequestFlag == true)
                {
                    IAPUpgradeAppResendCount++;

                    if (IAPUpgradeAppResendCount > IAPUpgradeAppResendMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        IAPUpgradeAppResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        // close file
                        fileStream.Close();

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread End Running (Resent Exceed Maximum Count) ...");

                        return;
                    }

                    i = i - IAPUpgradeAppEachRequestSendByte;

                    // resend
                    FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread Running (Resend Data) ...");

                    FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeAPPFast(i, byteData, IAPUpgradeAppEachRequestSendByte);

                    // set locker
                    IAPUpgradeAppTransmitLocker = true;

                    // clear resend flag
                    IAPUpgradeAppResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeAppResendCount = 0;
                }

                // send data
                for (uint ii = 0; ii < IAPUpgradeAppEachRequestSendByte; ii++)
                {
                    fileData = fileStream.ReadByte(); // when greater the file size, it will return 0xFF
                    byteData[ii] = (byte)fileData;
                }

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeAPPFast(i, byteData, IAPUpgradeAppEachRequestSendByte);

                IAPUpgradeAppMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeAppProgress = 100 * i / IAPUpgradeAppSize;

                // set locker
                IAPUpgradeAppTransmitLocker = true;
            }

            // close file
            fileStream.Close();

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeAppProgress = 100;

            // clear locker
            IAPUpgradeAppLocker = false;

            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread End Running (Success) ...");

            return;
        }

        /// @brief Upgrade MMU App (application) [更新 MMU Application 程序]
        /// 
        /// @param fileBuffer The file buffer of the application .bin file [.bin 文件的数据]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult UpgradeMMUApp(
            byte[] fileBuffer)
        {
            // check the upgrade process is locked or not
            if (IAPUpgradeAppLocker == true)
            {
                return FunctionResult.Fail;
            }

            Thread upgradeMMUAppThread = new Thread(new ParameterizedThreadStart(UpgradeMMUAppFastThread));
            upgradeMMUAppThread.Start(fileBuffer);

            // sleep to let main thread give time to create children thread.
            Thread.Sleep(FFTAICommunicationConfig.SLEEP_TIME_FOR_CREATING_CHILDREN_THREAD);

            return FunctionResult.Success;
        }

        private static void UpgradeMMUAppThread(
            byte[] fileBuffer)
        {
            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread Start Running ...");

            // set locker
            IAPUpgradeAppLocker = true;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file

            // set progress to 0
            StatusIAP.IAPUpgradeAppProgress = 0;

            // set address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPStartAddress(IAPUpgradeAppStartAddress);

            // set size
            IAPUpgradeAppSize = (uint)fileBuffer.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPSize(IAPUpgradeAppSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseAPP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // clear locker
            IAPUpgradeAppTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileBuffer.Length; i++)
            {
                while (IAPUpgradeAppTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeAppMonitorCount++;

                    if (IAPUpgradeAppMonitorCount > IAPUpgradeAppMonitorMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread End Running (Not Receive Response) ...");

                        return;
                    }
                }

                // resend request ?
                if (IAPUpgradeAppResendRequestFlag == true)
                {
                    IAPUpgradeAppResendCount++;

                    if (IAPUpgradeAppResendCount > IAPUpgradeAppResendMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        IAPUpgradeAppResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        // close file

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread End Running (Resent Exceed Maximum Count) ...");

                        return;
                    }

                    i = i - 1;

                    IAPUpgradeAppResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeAppResendCount = 0;
                }

                int fileData = fileBuffer[i];

                byte byteData = (byte)fileData;

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeAPP(i, byteData);

                IAPUpgradeAppMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeAppProgress = 100 * i / IAPUpgradeAppSize;

                // set locker
                IAPUpgradeAppTransmitLocker = true;
            }

            // close file

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeAppProgress = 100;

            // clear locker
            IAPUpgradeAppLocker = false;

            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppThread End Running (Success) ...");

            return;
        }

        private static void UpgradeMMUAppFastThread(byte[] fileBuffer)
        {
            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread Start Running ...");

            // set locker
            IAPUpgradeAppLocker = true;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Ready;

            // open file

            // set progress to 0
            StatusIAP.IAPUpgradeAppProgress = 0;

            // set address
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPStartAddress(IAPUpgradeAppStartAddress);

            // set size
            IAPUpgradeAppSize = (uint)fileBuffer.Length;

            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetAPPSize(IAPUpgradeAppSize);

            // erase flash
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetEraseAPP();

            Thread.Sleep(10 * 1000); // sleep 10s for flash erase

            // set each message contain byte count
            uint IAPUpgradeAppEachRequestSendByte = 200;
            int fileData = 0;
            byte[] byteData = new byte[IAPUpgradeAppEachRequestSendByte];

            // clear locker
            IAPUpgradeAppTransmitLocker = false;

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Running;

            for (uint i = 0; i < fileBuffer.Length; i = i + IAPUpgradeAppEachRequestSendByte)
            {
                uint j = i;

                while (IAPUpgradeAppTransmitLocker == true)
                {
                    Thread.Sleep(10);

                    IAPUpgradeAppMonitorCount++;

                    if (IAPUpgradeAppMonitorCount > IAPUpgradeAppMonitorMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread End Running (Not Receive Response) ...");

                        return;
                    }
                }

                // resend request ?
                if (IAPUpgradeAppResendRequestFlag == true)
                {
                    IAPUpgradeAppResendCount++;

                    if (IAPUpgradeAppResendCount > IAPUpgradeAppResendMaximumCount)
                    {
                        IAPUpgradeAppMonitorCount = 0;

                        IAPUpgradeAppResendCount = 0;

                        // update progress
                        StatusIAP.IAPUpgradeAppProgress = 0;

                        // close file
          
                        // set status
                        StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Fail;

                        // clear locker
                        IAPUpgradeAppLocker = false;

                        FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread End Running (Resent Exceed Maximum Count) ...");

                        return;
                    }

                    i = i - IAPUpgradeAppEachRequestSendByte;

                    // resend
                    FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread Running (Resend Data) ...");

                    FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeAPPFast(i, byteData, IAPUpgradeAppEachRequestSendByte);

                    // set locker
                    IAPUpgradeAppTransmitLocker = true;

                    // clear resend flag
                    IAPUpgradeAppResendRequestFlag = false;
                }
                else
                {
                    IAPUpgradeAppResendCount = 0;
                }

                // send data
                for (uint ii = 0; ii < IAPUpgradeAppEachRequestSendByte; ii++)
                {
                    if (j < fileBuffer.Length)
                    {
                        fileData = fileBuffer[j];
                    }
                    else
                    {
                        fileData = 0xFF; // when greater the file size, it will return 0xFF
                    }

                    j++;

                    byteData[ii] = (byte)fileData;
                }

                FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread Running (Send Data) ...");

                FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetUpgradeAPPFast(i, byteData, IAPUpgradeAppEachRequestSendByte);

                IAPUpgradeAppMonitorCount = 0;

                // update progress
                StatusIAP.IAPUpgradeAppProgress = 100 * i / IAPUpgradeAppSize;

                // set locker
                IAPUpgradeAppTransmitLocker = true;
            }

            // close file

            // set status
            StatusIAP.IAPUpgradeAppStatus = DynaLinkHSPara.IAPUpgradeStatus.Success;

            // update progress
            StatusIAP.IAPUpgradeAppProgress = 100;

            // clear locker
            IAPUpgradeAppLocker = false;

            FFTAICommunicationManager.Instance.Logger.WriteLine("UpgradeMMUAppFastThread End Running (Success) ...");

            return;
        }

        /// @brief Set MMU boot mode [设置 MMU Boot 模式]
        /// 
        /// @param The boot mode to be set to the MMU [Boot 模式]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetBootMode(
            DynaLinkHSPara.IAPBootMode bootMode)
        {
            FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestSetBootMode((uint)bootMode);

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (IAP) -------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V1) --------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V1) --------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V2) --------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V2 - System) -----------------------------------------------------------------------

        /// @brief Set robot type [设置机器人类型]
        /// @details Set the robot model calculated in the MMU, after set, must reboot the MMU [设置 MMu 的机器人类型，设置完成后，必须重启机器]
        /// 
        /// @param robotType The robot type [机器人类型]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetRobotType(
            DynaLinkHSPara.RobotType robotType)
        {
            FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestSetRobotType((uint)robotType);

            return FunctionResult.Success;
        }

        /// @brief Set mechanism type [设置机械类型]
        /// @details Set the mechanism type in the MMU, after set, must reboot the MMU [设置 MMu 的机械类型，设置完成后，必须重启机器]
        /// 
        /// @param mechanism The robot mechanism version [机械类型]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetMechanismType(
            DynaLinkHSPara.MechanismType mechanismType)
        {
            FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestSetMechanismVersion((uint)mechanismType);

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (V2 - System) -----------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V2 - Basic) ------------------------------------------------------------------------

        /// @brief Set MMU work mode [设置 MMU 工作模式]
        /// @details There are several work modes set in the MMU, the Unity use Relay mode as default. [在底层存在多种工作模式，Unity 默认使用 Relay 工作模式即可]
        /// 
        /// @param workMode The work mode of MMU [MMU 工作模式]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetWorkMode(
            DynaLinkHSPara.WorkMode workMode)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            (FFTAICommunicationV2M1TaskInterfaceWorkMode)workMode);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            (FFTAICommunicationV2M2TaskInterfaceWorkMode)workMode);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X1TaskInterface.RequestSetWorkMode(
                            (FFTAICommunicationV2X1TaskInterfaceWorkMode)workMode);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            (FFTAICommunicationV2X2TaskInterfaceWorkMode)workMode);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (V2 - Basic) ------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V2 - Hardware) ---------------------------------------------------------------------

        /// @brief Set digit output of the circuit board [设置数字端口输出]
        ///
        /// @note Not used right now [暂未使用]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetDigitOutput(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                case DynaLinkHSPara.RobotType.M2:
                case DynaLinkHSPara.RobotType.X1:
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RequestSetGpioIoStatus(
                            (uint)parameters[0], (uint)parameters[1], (uint)parameters[2], (uint)parameters[3]);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (V2 - Hardware) ---------------------------------------------------------------------

        //-------------------------------------------- Function Definition (V2 - Protection) -------------------------------------------------------------------

        /// @brief Set joint kinetic limit [设置关节力/力矩限制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector minimum kinetic [关节最小力矩限制] (type : float, unit : N, range : < 0) 
        ///     - end effector maximum kinetic [关节最大力矩限制] (type : float, unit : N, range : > 0) 
        /// 
        /// for M2, the parameters are
        ///     - end effector horizontal minimum kinetic [x 轴方向关节最小力限制] (type : float, unit : N, range : < 0) 
        ///     - end effector horizontal maximum kinetic [x 轴方向关节最大力限制] (type : float, unit : N, range : > 0) 
        ///     - end effector vertical minimum kinetic [y 轴方向关节最小力限制] (type : float, unit : N, range : < 0) 
        ///     - end effector vertical maximum kinetic [y 轴方向关节最大力限制] (type : float, unit : N, range : > 0) 
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetJointLimitKinetic(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestSetJointLimitKinetic(
                            parameters[0], parameters[1]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestSetJointLimitKinetic(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set end effector kinetic limit [设置末端力/力矩限制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector minimum kinetic [最小力矩限制] (type : float, unit : N, range : < 0) 
        ///     - end effector maximum kinetic [最大力矩限制] (type : float, unit : N, range : > 0) 
        /// 
        /// for M2, the parameters are
        ///     - end effector horizontal minimum kinetic [x 轴方向最小力限制] (type : float, unit : N, range : < 0) 
        ///     - end effector horizontal maximum kinetic [x 轴方向最大力限制] (type : float, unit : N, range : > 0) 
        ///     - end effector vertical minimum kinetic [y 轴方向最小力限制] (type : float, unit : N, range : < 0) 
        ///     - end effector vertical maximum kinetic [y 轴方向最大力限制] (type : float, unit : N, range : > 0) 
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetEndEffectorLimitKinetic(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestSetEndEffectorLimitKinetic(
                            parameters[0], parameters[1]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestSetEndEffectorLimitKinetic(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set end effector position limit [机器人末端位置限制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector minimum position [最小位置限制] (type : float, unit : deg, range : )
        ///     - end effector maximum position [最大位置限制] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - end effector horizontal minimum position [x 轴最小位置限制] (type : float, unit : N, range : )
        ///     - end effector horizontal maximum position [x 轴最大位置限制] (type : float, unit : N, range : )
        ///     - end effector vertical minimum position [y 轴最小位置限制] (type : float, unit : N, range : )
        ///     - end effector vertical maximum position [y 轴最大位置限制] (type : float, unit : N, range : )
        ///
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetEndEffectorLimitPosition(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestSetEndEffectorLimitPosition(
                            parameters[0], parameters[1]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestSetEndEffectorLimitPosition(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set current position as the end effector position limit [设置当前位置为机器人位置限制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector minimum position flag [末端最小位置设置标志] (type : float, unit : , range : 0 or 1)
        ///     - end effector maximum position flag [末端最大位置设置标志] (type : float, unit : , range : 0 or 1)
        ///
        /// for M2, the parameters are
        ///     - end effector horizontal minimum position flag [末端 x 轴最小位置设置标志] (type : float, unit : , range : 0 or 1)
        ///     - end effector horizontal maximum position flag [末端 x 轴最大位置设置标志] (type : float, unit : , range : 0 or 1)
        ///     - end effector vertical minimum position flag [末端 y 轴最小位置设置标志] (type : float, unit : , range : 0 or 1)
        ///     - end effector vertical maximum position flag [末端 y 轴最大位置设置标志] (type : float, unit : , range : 0 or 1)
        ///
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdSetCurrentPositionAsEndEffectorLimitPosition(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestSetCurrentValueAsEndEffectorLimitPosition(
                            (uint)parameters[0], (uint)parameters[1]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestSetCurrentValueAsEndEffectorLimitPosition(
                            (uint)parameters[0], (uint)parameters[1], (uint)parameters[2], (uint)parameters[3]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (V2 - Protection) -------------------------------------------------------------------
        
        //-------------------------------------------- Function Definition (V2 - Robot Control) ----------------------------------------------------------------

        /// @brief Set joint kinetic control [设置机器关节力/力矩控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - joint kinetic [关节 力矩] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - horizontal joint kinetic [横向关节 力] (type : float, unit : m, range : )
        ///     - vertical joint kinetic [纵向关节 力] (type : float, unit : m, range : )
        /// 
        /// for X2, the parameters are
        ///     - left leg hip joint kinetic [左腿髋关节 力矩] (type : float, unit : m, range : )
        ///     - left leg knee joint kinetic [左腿膝关节 力矩] (type : float, unit : m, range : )
        ///     - right leg hip joint kinetic [右腿髋关节 力矩] (type : float, unit : m, range : )
        ///     - right leg knee joint kinetic [左腿膝关节 力矩] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdJointKineticControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicJointKineticControlKinetic(
                            parameters[0]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceDebugWorkMode.JointKineticControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicJointKineticControlKinetic(
                            parameters[0], parameters[1]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceDebugWorkMode.JointKineticControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicJointKineticControlKinetic(
                            parameters[0], parameters[1], parameters[2], parameters[3]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceDebugWorkMode.JointKineticControl);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set joint velocity control [设置机器关节速度控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - joint acceleration [关节 加速度] (type : float, unit : deg, range : )
        ///     - joint velocity [关节 速度] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - horizontal joint acceleration [横向关节 加速度] (type : float, unit : m, range : )
        ///     - vertical joint acceleration [纵向关节 加速度] (type : float, unit : m, range : )
        ///     - horizontal joint velocity [横向关节 速度] (type : float, unit : m, range : )
        ///     - vertical joint velocity [纵向关节 速度] (type : float, unit : m, range : )
        /// 
        /// for X2, the parameters are
        ///     - left leg hip joint acceleration [左腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - left leg knee joint acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - right leg hip joint acceleration [右腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - right leg knee joint acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - left leg hip joint velocity [左腿髋关节 速度] (type : float, unit : m, range : )
        ///     - left leg knee joint velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        ///     - right leg hip joint velocity [右腿髋关节 速度] (type : float, unit : m, range : )
        ///     - right leg knee joint velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdJointVelocityControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicJointVelocityControlAcceleration(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicJointVelocityControlVelocity(
                            parameters[1]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceDebugWorkMode.JointVelocityControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicJointVelocityControlAcceleration(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicJointVelocityControlVelocity(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceDebugWorkMode.JointVelocityControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 8)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicJointVelocityControlAcceleration(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicJointVelocityControlVelocity(
                            parameters[4], parameters[5], parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceDebugWorkMode.JointVelocityControl);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set joint position control [设置机器关节位置控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - joint acceleration [关节 加速度] (type : float, unit : deg, range : )
        ///     - joint velocity [关节 速度] (type : float, unit : deg, range : )
        ///     - joint position [关节 位置] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - horizontal joint acceleration [横向关节 加速度] (type : float, unit : m, range : )
        ///     - vertical joint acceleration [纵向关节 加速度] (type : float, unit : m, range : )
        ///     - horizontal joint velocity [横向关节 速度] (type : float, unit : m, range : )
        ///     - vertical joint velocity [纵向关节 速度] (type : float, unit : m, range : )
        ///     - horizontal joint position [横向关节 位置] (type : float, unit : m, range : )
        ///     - vertical joint position [纵向关节 位置] (type : float, unit : m, range : )
        /// 
        /// for X2, the parameters are
        ///     - left leg hip joint acceleration [左腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - left leg knee joint acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - right leg hip joint acceleration [右腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - right leg knee joint acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - left leg hip joint velocity [左腿髋关节 速度] (type : float, unit : m, range : )
        ///     - left leg knee joint velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        ///     - right leg hip joint velocity [右腿髋关节 速度] (type : float, unit : m, range : )
        ///     - right leg knee joint velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        ///     - left leg hip joint position [左腿髋关节 位置] (type : float, unit : m, range : )
        ///     - left leg knee joint position [左腿膝关节 位置] (type : float, unit : m, range : )
        ///     - right leg hip joint position [右腿髋关节 位置] (type : float, unit : m, range : )
        ///     - right leg knee joint position [左腿膝关节 位置] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdJointPositionControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 3)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicJointPositionControlAcceleration(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicJointPositionControlVelocity(
                            parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicJointPositionControlPosition(
                            parameters[2]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceDebugWorkMode.JointPositionControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 6)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicJointPositionControlAcceleration(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicJointPositionControlVelocity(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicJointPositionControlPosition(
                            parameters[4], parameters[5]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceDebugWorkMode.JointPositionControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 12)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicJointPositionControlAcceleration(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicJointPositionControlVelocity(
                            parameters[4], parameters[5], parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicJointPositionControlPosition(
                            parameters[8], parameters[9], parameters[10], parameters[11]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceDebugWorkMode.JointPositionControl);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set end effector kinetic control [设置机器末端力/力矩控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector kinetic [末端 力矩] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - end effector x kinetic [末端横向 力] (type : float, unit : m, range : )
        ///     - end effector y kinetic [末端纵向 力] (type : float, unit : m, range : )
        /// 
        /// for X2, the parameters are
        ///     - end effector x1 kinetic [末端1 x方向 力矩] (type : float, unit : m, range : )
        ///     - end effector y1 kinetic [末端1 y方向 力矩] (type : float, unit : m, range : )
        ///     - end effector x2 kinetic [末端2 x方向 力矩] (type : float, unit : m, range : )
        ///     - end effector y2 kinetic [末端1 y方向 力矩] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdEndEffectorKineticControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicEndEffectorKineticControlKinetic(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceDebugWorkMode.EndEffectorKineticControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicEndEffectorKineticControlKinetic(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceDebugWorkMode.EndEffectorKineticControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicEndEffectorKineticControlKinetic(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceDebugWorkMode.EndEffectorKineticControl);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set end effector velocity control [设置机器末端速度控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector acceleration [关节 加速度] (type : float, unit : deg, range : )
        ///     - end effector velocity [关节 速度] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - end effector x acceleration [横向关节 加速度] (type : float, unit : m, range : )
        ///     - end effector y acceleration [纵向关节 加速度] (type : float, unit : m, range : )
        ///     - end effector x velocity [横向关节 速度] (type : float, unit : m, range : )
        ///     - end effector y velocity [纵向关节 速度] (type : float, unit : m, range : )
        /// 
        /// for X2, the parameters are
        ///     - end effector x1 acceleration [左腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - end effector y1 acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - end effector x2 acceleration [右腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - end effector y2 acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - end effector x1 velocity [左腿髋关节 速度] (type : float, unit : m, range : )
        ///     - end effector y1 velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        ///     - end effector x2 velocity [右腿髋关节 速度] (type : float, unit : m, range : )
        ///     - end effector y2 velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdEndEffectorVelocityControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicEndEffectorVelocityControlAcceleration(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicEndEffectorVelocityControlVelocity(
                            parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceDebugWorkMode.EndEffectorVelocityControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicEndEffectorVelocityControlAcceleration(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicEndEffectorVelocityControlVelocity(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceDebugWorkMode.EndEffectorVelocityControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 8)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicEndEffectorVelocityControlAcceleration(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicEndEffectorVelocityControlVelocity(
                            parameters[4], parameters[5], parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceDebugWorkMode.EndEffectorVelocityControl);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set end effector position control [设置机器末端位置控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - end effector acceleration [关节 加速度] (type : float, unit : deg, range : )
        ///     - end effector velocity [关节 速度] (type : float, unit : deg, range : )
        ///     - end effector position [关节 位置] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - end effector x acceleration [横向关节 加速度] (type : float, unit : m, range : )
        ///     - end effector y acceleration [纵向关节 加速度] (type : float, unit : m, range : )
        ///     - end effector x velocity [横向关节 速度] (type : float, unit : m, range : )
        ///     - end effector y velocity [纵向关节 速度] (type : float, unit : m, range : )
        ///     - end effector x position [横向关节 位置] (type : float, unit : m, range : )
        ///     - end effector y position [纵向关节 位置] (type : float, unit : m, range : )
        /// 
        /// for X2, the parameters are
        ///     - end effector x1 acceleration [左腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - end effector y1 acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - end effector x2 acceleration [右腿髋关节 加速度] (type : float, unit : m, range : )
        ///     - end effector y2 acceleration [左腿膝关节 加速度] (type : float, unit : m, range : )
        ///     - end effector x1 velocity [左腿髋关节 速度] (type : float, unit : m, range : )
        ///     - end effector y1 velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        ///     - end effector x2 velocity [右腿髋关节 速度] (type : float, unit : m, range : )
        ///     - end effector y2 velocity [左腿膝关节 速度] (type : float, unit : m, range : )
        ///     - end effector x1 position [左腿髋关节 位置] (type : float, unit : m, range : )
        ///     - end effector y1 position [左腿膝关节 位置] (type : float, unit : m, range : )
        ///     - end effector x2 position [右腿髋关节 位置] (type : float, unit : m, range : )
        ///     - end effector y2 position [左腿膝关节 位置] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdEndEffectorPositionControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 3)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlAcceleration(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlVelocity(
                            parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlPosition(
                            parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceDebugWorkMode.EndEffectorPositionControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 6)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlAcceleration(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlVelocity(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlPosition(
                            parameters[4], parameters[5]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceDebugWorkMode.EndEffectorPositionControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 12)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlAcceleration(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlVelocity(
                            parameters[4], parameters[5], parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicEndEffectorPositionControlPosition(
                            parameters[8], parameters[9], parameters[10], parameters[11]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Debug);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetDebugWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceDebugWorkMode.EndEffectorPositionControl);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set servo on [设置机器电机使能]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdServoOn(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ServoOn);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ServoOn);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.ServoOn);
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set servo off [设置机器电机失能]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdServoOff(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ServoOff);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ServoOff);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.ServoOff);
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set pause motion [设置机器电机锁住]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdPauseMotion(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.PauseMotion);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.PauseMotion);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.PauseMotion);
                        
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set clear alarm [设置清楚警告]
        /// 
        /// @note Not used right now [暂未使用]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdClearAlarm(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ClearAlarm);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ClearAlarm);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.ClearAlarm);
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set clear fault [设置清除驱动器错误]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdClearFault(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ClearFault);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ClearFault);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.ClearFault);
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set calibrate sensors [设置校准传感器]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdCalibrateSensor(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestSetButtonSensorCalibrate();

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestSetTorqueSensorCalibrate();

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestSetButtonSensorCalibrate();

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestSetForceSensorCalibrate();

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestSetButtonSensorCalibrate();

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestSetForceSensorCalibrate();

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestSetFootPressureSensorCalibrate();

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set find home position [设置寻找零点位置]
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdFindHomePosition(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.FindHome);
					
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicFindHomeRequestKinetic(
                            50 /* unit : N */, 
                            50 /* unit : N */);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.FindHome);
					
                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set passive movement control [设置被动运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - target position [目标位置] (type : float, unit : deg, range : )
        ///     - angular velocity [移动速度] (type : float, unit : deg/s, range : )
        ///
        /// for M2, the parameters are
        ///     - target position x [目标位置 x 轴] (type : float, unit : m, range : )
        ///     - target position y [目标位置 y 轴] (type : float, unit : m, range : )
        ///     - velocity [移动速度] (type : float, unit : m/s, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdPassiveMovementControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicPassiveLinearMotionPosition(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicPassiveLinearMotionVelocity(
                            parameters[1]);
                                    
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.PassiveLinearMotion);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 3)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicPassiveLinearMotionPosition(
                            parameters[0], parameters[1]);
                                    
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicPassiveLinearMotionVelocity(
                            parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.PassiveLinearMotion);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 8)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicPassiveMovePosition(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicPassiveMoveVelocity(
                            parameters[4], parameters[5], parameters[6], parameters[7]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.PassiveMove);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set mass simulation control [设置质量仿真运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - mass [质量] (type : float, unit : deg, range : > 0)
        ///     - friction [摩擦系数] (type : float, unit : deg/s, range : > 0)
        ///
        /// for M2, the parameters are
        ///     - mass x [质量 x 轴] (type : float, unit : kg, range : > 0)
        ///     - mass y [质量 y 轴] (type : float, unit : kg, range : > 0)
        ///     - friction x [摩擦系数 x 轴] (type : float, unit : , range : > 0)
        ///     - friction y [摩擦系数 y 轴] (type : float, unit : , range : > 0)
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdMassSimulationControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicMassSimulationMass(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicMassSimulationFriction(
                            parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.MassSimulation);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicMassSimulationMass(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicMassSimulationFriction(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.MassSimulation);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set assist control without sensor [设置无传感器助力运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - assist torque [助力力矩] (type : float, unit : Nm, range : )
        ///
        /// for M2, the parameters are
        ///     - assist force x [助力力量 x 轴] (type : float, unit : N, range : > 0)
        ///     - assist force y [助力力量 y 轴] (type : float, unit : N, range : > 0)
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdAssistControlWithoutSensor(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicForceAssistWithoutSensorForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ForceAssistWithoutSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicForceAssistWithoutSensorForce(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ForceAssistWithoutSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set assist control with sensor [设置有传感器助力运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - assist torque [助力力矩] (type : float, unit : Nm, range : )
        ///
        /// for M2, the parameters are
        ///     - assist force x [助力力量 x 轴] (type : float, unit : N, range : > 0)
        ///     - assist force y [助力力量 y 轴] (type : float, unit : N, range : > 0)
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdAssistControlWithSensor(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicForceAssistWithSensorForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ForceAssistWithSensor);
                                    
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicForceAssistWithSensorForce(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ForceAssistWithSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set assist control with target position [设置有目标点的传感器助力运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - assist torque [助力力矩] (type : float, unit : Nm, range : )
        ///     - target position [目标角度] (type : float, unit : deg, range : )
        ///
        /// for M2, the parameters are
        ///     - assist force x [助力力量 合力] (type : float, unit : N, range : > 0)
        ///     - target position x [目标位置 x 轴] (type : float, unit : m, range : )
        ///     - target position y [目标位置 y 轴] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdAssistControlWithTargetPosition(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ForceAssistWithTargetPosition);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicForceAssistWithTargetPositionForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicForceAssistWithTargetPositionPosition(
                            parameters[1]);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 3)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicForceAssistWithTargetPositionForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicForceAssistWithTargetPositionPosition(
                            parameters[1], parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ForceAssistWithTargetPosition);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set resist control without sensor [设置无传感器阻力运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - resist torque [阻力力矩] (type : float, unit : Nm, range : )
        ///
        /// for M2, the parameters are
        ///     - resist force x [阻力力量 x 轴] (type : float, unit : N, range : > 0)
        ///     - resist force y [阻力力量 y 轴] (type : float, unit : N, range : > 0)
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdResistControlWithoutSensor(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicForceResistWithoutSensorForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ForceResistWithoutSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicForceResistWithoutSensorForce(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ForceResistWithoutSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set resist control with sensor [设置有传感器阻力运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - resist torque [阻力力矩] (type : float, unit : Nm, range : )
        ///
        /// for M2, the parameters are
        ///     - resist force x [阻力力量 x 轴] (type : float, unit : N, range : > 0)
        ///     - resist force y [阻力力量 y 轴] (type : float, unit : N, range : > 0)
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdResistControlWithSensor(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }
                                    
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicForceResistWithSensorForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.ForceResistWithSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicForceResistWithSensorForce(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.ForceResistWithSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set transparent control [设置透明运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - origin/target position [原始/目标位置] (type : float, unit : m, range : )
        ///     - M (mass) [模拟质量] (type : float, unit : kg, range : )
        ///     - B (damping) [模拟阻尼] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) [模拟弹簧] (type : float, unit : N/m, range : )
        ///
        /// for M2, the parameters are
        ///     - origin/target position x [原始/目标位置 x 轴] (type : float, unit : m, range : )
        ///     - origin/target position y [原始/目标位置 y 轴] (type : float, unit : m, range : )
        ///     - M (mass) x [模拟质量 x 轴] (type : float, unit : kg, range : )
        ///     - M (mass) y [模拟质量 y 轴] (type : float, unit : kg, range : )
        ///     - B (damping) x [模拟阻尼 x 轴] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) y [模拟阻尼 y 轴] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) x [模拟弹簧 x 轴] (type : float, unit : N/m, range : )
        ///     - K (spring) y [模拟阻尼 y 轴] (type : float, unit : N/m, range : )
        /// 
        /// for X2, the parameters are
        ///     - origin/target position left leg hip joint [原始/目标位置 左腿髋关节] (type : float, unit : m, range : )
        ///     - origin/target position left leg knee joint [原始/目标位置 左腿膝关节] (type : float, unit : m, range : )
        ///     - origin/target position right leg hip joint [原始/目标位置 右腿髋关节] (type : float, unit : m, range : )
        ///     - origin/target position right leg hip joint [原始/目标位置 右腿膝关节] (type : float, unit : m, range : )
        ///     - M (mass) left leg hip joint [模拟质量 左腿髋关节] (type : float, unit : kg, range : )
        ///     - M (mass) left leg knee joint [模拟质量 左腿膝关节] (type : float, unit : kg, range : )
        ///     - M (mass) right leg hip joint [模拟质量 右腿髋关节] (type : float, unit : kg, range : )
        ///     - M (mass) right leg knee joint [模拟质量 右腿膝关节] (type : float, unit : kg, range : )
        ///     - B (damping) left leg hip joint [模拟阻尼 左腿髋关节] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) left leg knee joint [模拟阻尼 左腿膝关节] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) right leg hip joint [模拟阻尼 右腿髋关节] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) right leg knee joint [模拟阻尼 右腿膝关节] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) left leg hip joint [模拟弹簧 左腿髋关节] (type : float, unit : N/m, range : )
        ///     - K (spring) left leg knee joint [模拟阻尼 左腿膝关节] (type : float, unit : N/m, range : )
        ///     - K (spring) right leg hip joint [模拟弹簧 右腿髋关节] (type : float, unit : N/m, range : )
        ///     - K (spring) right leg knee joint [模拟阻尼 右腿膝关节] (type : float, unit : N/m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdTransparentControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlOriginPosition(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlM(
                            parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlB(
                            parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlK(
                            parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.TransparentControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 8)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlOriginPosition(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlM(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlB(
                            parameters[4], parameters[5]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlK(
                            parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.TransparentControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 16)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicTransparentControlMoveOriginPosition(
                            parameters[0], parameters[1], parameters[2], parameters[3]);
                       
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicTransparentControlMoveM(
                            parameters[4], parameters[5], parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicTransparentControlMoveB(
                            parameters[8], parameters[9], parameters[10], parameters[11]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicTransparentControlMoveK(
                            parameters[12], parameters[13], parameters[14], parameters[15]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.TransparentControlMove);
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set tunnel guidance control [通道引导控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - Inapplicable [不适用]
        ///
        /// for M2, the parameters are
        ///     - Point A position x [A点位置 x 坐标] (type : float, unit : m, range : )
        ///     - Point A position y [A点位置 y 坐标] (type : float, unit : m, range : )
        ///     - Point B position x [B点位置 x 坐标] (type : float, unit : m, range : )
        ///     - Point B position y [B点位置 y 坐标] (type : float, unit : m, range : )
        ///     - M (mass) AB direction [模拟质量 AB 横轴] (type : float, unit : kg, range : )
        ///     - M (mass) AB vertical direction [模拟质量 AB 纵轴] (type : float, unit : kg, range : )
        ///     - B (damping) AB direction [模拟阻尼 AB 横轴] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) AB vertical direction [模拟阻尼 AB 纵轴] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) AB direction [模拟弹簧 AB 横轴] (type : float, unit : N/m, range : )
        ///     - K (spring) AB vertical direction [模拟阻尼 AB 纵轴] (type : float, unit : N/m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdTunnelGuidanceControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 10)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTunnelGuidanceControlPointA(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTunnelGuidanceControlPointB(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTunnelGuidanceControlM(
                            parameters[4], parameters[5]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTunnelGuidanceControlB(
                            parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTunnelGuidanceControlK(
                            parameters[8], parameters[9]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.TunnelGuidanceControl);
                        
                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set draw infinity curve control [画"无穷"符号]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - Inapplicable [不适用]
        ///
        /// for M2, the parameters are
        ///     - origin position x [原点 x 轴] (type : float, unit : m, range : )
        ///     - origin position y [原点 y 轴] (type : float, unit : m, range : )
        ///     - time period [周期] (type : float, unit : s, range : )
        ///     - scale x [运动范围 x 轴] (type : float, unit : m, range : )
        ///     - scale y [运动范围 y 轴] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdDrawInfinityCurveControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        // check parameter length match
                        if (parameters.Length != 5)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicDrawInfinityCurveOriginPoint(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicDrawInfinityCurveTimePeriod(
                            parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicDrawInfinityCurveScale(
                            parameters[3], parameters[4]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.DrawInfinityCurve);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set draw infinity curve control [画椭圆/圆]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - Inapplicable [不适用]
        ///
        /// for M2, the parameters are
        ///     - origin position x [原点 x 轴] (type : float, unit : m, range : )
        ///     - origin position y [原点 y 轴] (type : float, unit : m, range : )
        ///     - time period [周期] (type : float, unit : s, range : )
        ///     - scale x [运动范围 x 轴] (type : float, unit : m, range : )
        ///     - scale y [运动范围 y 轴] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdDrawCircleCurveControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        // check parameter length match
                        if (parameters.Length != 5)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicDrawCircleCurveOriginPoint(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicDrawCircleCurveTimePeriod(
                            parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicDrawCircleCurveScale(
                            parameters[3], parameters[4]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.DrawCircleCurve);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set kinetic control with sensor [设置有传感器动力运动控制(合并AssistControl和ResistControl)]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - assist torque [动力力矩] (type : float, unit : Nm, range : )
        ///
        /// for M2, the parameters are
        ///     - assist force x [动力力量 x 轴] (type : float, unit : N, range : )
        ///     - assist force y [动力力量 y 轴] (type : float, unit : N, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdKineticControlWithSensor(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 1)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicKineticControlWithSensorForce(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.KineticControlWithSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 2)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicKineticControlWithSensorForce(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.KineticControlWithSensor);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        if (parameters.Length != 4)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetSubtaskBasicActiveMoveKinetic(
                            parameters[0], parameters[1], parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2X2TaskInterfaceRelayWorkMode.ActiveMove);

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set transparent control With Limit Spring Force [设置带弹簧弹力限制的透明运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///     - origin/target position [原始/目标位置] (type : float, unit : m, range : )
        ///     - M (mass) [模拟质量] (type : float, unit : kg, range : )
        ///     - B (damping) [模拟阻尼] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) [模拟弹簧] (type : float, unit : N/m, range : )
        ///     - Limit Spring Force [弹簧弹力限制] (type : float, unit : N, range : )
        ///
        /// for M2, the parameters are
        ///     - origin/target position x [原始/目标位置 x 轴] (type : float, unit : m, range : )
        ///     - origin/target position y [原始/目标位置 y 轴] (type : float, unit : m, range : )
        ///     - M (mass) x [模拟质量 x 轴] (type : float, unit : kg, range : )
        ///     - M (mass) y [模拟质量 y 轴] (type : float, unit : kg, range : )
        ///     - B (damping) x [模拟阻尼 x 轴] (type : float, unit : N/(m/s), range : )
        ///     - B (damping) y [模拟阻尼 y 轴] (type : float, unit : N/(m/s), range : )
        ///     - K (spring) x [模拟弹簧 x 轴] (type : float, unit : N/m, range : )
        ///     - K (spring) y [模拟阻尼 y 轴] (type : float, unit : N/m, range : )
        ///     - Limit Spring Force x [弹簧弹力限制 x 轴] (type : float, unit : N, range : )
        ///     - Limit Spring Force y [弹簧弹力限制 x 轴] (type : float, unit : N, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdTransparentControlWithLimitSpringForce(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        if (parameters.Length != 5)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceOriginPosition(
                            parameters[0]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceM(
                            parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceB(
                            parameters[2]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceK(
                            parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceLimitSpringForce(
                            parameters[4]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M1TaskInterfaceRelayWorkMode.TransparentControlWithLimitSpringForce);
                        
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 10)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceOriginPosition(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceM(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceB(
                            parameters[4], parameters[5]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceK(
                            parameters[6], parameters[7]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicTransparentControlWithLimitSpringForceLimitSpringForce(
                            parameters[8], parameters[9]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.TransparentControlWithLimitSpringForce);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        /// @brief Set minimum jerk trajectory control [设置最小 jerk 运动控制]
        /// 
        /// @details 
        /// for M1, the parameters are
        ///
        /// for M2, the parameters are
        ///     - point A x [点A位置 x 轴] (type : float, unit : m, range : )
        ///     - point A y [点A位置 y 轴] (type : float, unit : m, range : )
        ///     - point B x [点A位置 x 轴] (type : float, unit : m, range : )
        ///     - point B y [点A位置 y 轴] (type : float, unit : m, range : )
        ///     - total time [规划总时间] (type : float, unit : m, range : )
        /// 
        /// @return Function result [方法执行结果]
        /// @retval FunctionResult.Success Success [成功]
        /// @retval FunctionResult.Fail Fail [失败]
        public static FunctionResult CmdMinimumJerkTrajectoryControl(
            params float[] parameters)
        {
            switch (DynaLinkHS.StatusApp.RobotType)
            {
                case DynaLinkHSPara.RobotType.M1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.M2:
                    {
                        if (parameters.Length != 5)
                        {
                            return FunctionResult.Fail;
                        }

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicMinimumJerkTrajectoryControlPointA(
                            parameters[0], parameters[1]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicMinimumJerkTrajectoryControlPointB(
                            parameters[2], parameters[3]);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicMinimumJerkTrajectoryControlInitialTime();

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetSubtaskBasicMinimumJerkTrajectoryControlTotalTime(
                            parameters[4]);
                        
                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceWorkMode.Relay);

                        FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestSetRelayWorkMode(
                            FFTAICommunicationV2M2TaskInterfaceRelayWorkMode.MinimumJerkTrajectoryControl);

                        break;
                    }
                case DynaLinkHSPara.RobotType.X1:
                    {
                        break;
                    }
                case DynaLinkHSPara.RobotType.X2:
                    {
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (V2 - Robot Control) ----------------------------------------------------------------

        //-------------------------------------------- Function Definition (V2) --------------------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (Network) --------------------------------------------------------------------

        private static void ServerLinkMonitorTimerCallbackFunction(object state)
        {
            ServerLinkMonitorTimerActiveFlag = true;
			
            if (ServerLinkMonitorTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(ServerLinkMonitorTimerFunction));
                thread.Start();
            }
        }

        private static void ServerLinkMonitorTimerFunction()
        {
            // lock server monitor timer
            ServerLinkMonitorTimerLocker = true;

            //-------------------------------------------------------------------

            FunctionResult functionResult = FunctionResult.None;

            try
            {
                /**
                 *  Note :  The link act judegement must before the RequestTestConnection(), 
                 *          because once the connection is broken, the exception will be throw out, 
                 *          and the rest code will not be execute.
                 */
                // monitor count add 1
                if (StatusFlag.FlagServerLinkActive == 0x01)
                {
                    /**
                     *  Note : In case of iap or app update, do not add server link monitor count !!!
                     */
                    if(IAPUpgradeIapLocker == true || IAPUpgradeAppLocker == true)
                    {
                        
                    }
                    else
                    {
                        ServerLinkMonitorCount++;
                    }
                }

                if (ServerLinkMonitorCount > ServerLinkMonitorMaximumCount)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("ServerLinkMonitorCount Greater Than ServerLinkMonitorMaximumCount, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket

                    // Thread.CurrentThread.Abort(); // kill current thread
                }

                // send connection test request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2CommunicationInterface.RequestTestConnection();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("ServerLinkMonitorTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------
            
            // unlock server monitor timer
            ServerLinkMonitorTimerLocker = false;
        }

        //-------------------------------------------- Period Function Definition (Network) --------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (IAP) ------------------------------------------------------------------------

        private static void IAPTimerCallbackFunction(object state)
        {
            IAPTimerActiveFlag = true;
			
            if (IAPTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(IAPTimerFunction));
                thread.Start();
            }
        }

        private static void IAPTimerFunction()
        {
            // lock server monitor timer
            IAPTimerLocker = true;

            //-------------------------------------------------------------------

            FunctionResult functionResult = FunctionResult.None;

            try
            {
                // send iap request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestGetSoftwareVersion();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }

                // send iap request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestGetBootMode();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }

                // send iap request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2IAPInterface.RequestGetWorkStatus();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("ServerLinkMonitorTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------

            // unlock server monitor timer
            IAPTimerLocker = false;
        }

        //-------------------------------------------- Period Function Definition (IAP) ------------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (System) ---------------------------------------------------------------------

        private static void SystemPeriodicFunctionTimerCallbackFunction(object state)
        {
            SystemPeriodicFunctionTimerActiveFlag = true;
			
            if (SystemPeriodicFunctionTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(SystemTimerFunction));
                thread.Start();
            }
        }

        private static void SystemTimerFunction()
        {
            // lock
            SystemPeriodicFunctionTimerLocker = true;

            //-------------------------------------------------------------------

            FunctionResult functionResult = FunctionResult.None;

            try
            {
                // send system request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestGetHardwareVersion();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }

                // send system request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestGetSoftwareVersion();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }

                // send system request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestGetSerialNumber();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }

                // send system request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestGetRobotType();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }

                // send system request
                functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2SystemInterface.RequestGetMechanismVersion();

                // detect socket exception
                if (functionResult == FunctionResult.SocketException)
                {
                    FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                    ServerLinkMonitorCount = 0;
                    Disconnect(); // the server is down, close socket
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("ServerLinkMonitorTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------

            // unlock
            SystemPeriodicFunctionTimerLocker = false;
        }

        //-------------------------------------------- Period Function Definition (System) ---------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2) -------------------------------------------------------------------------

        private static void FlagPeriodicFunctionTimerCallbackFunction(object state)
        {
            FlagPeriodicFunctionTimerActiveFlag = true;
			
            if (FlagPeriodicFunctionTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(FlagPeriodicTimerFunction));
                thread.Start();
            }
        }

        private static void FlagPeriodicTimerFunction()
        {
            // lock periodic timer
            FlagPeriodicFunctionTimerLocker = true;

            //-------------------------------------------------------------------

            try
            {
                switch (DynaLinkHS.StatusApp.RobotType)
                {
                    case DynaLinkHSPara.RobotType.M1:
                        {
                            CommunicationV2M1FlagPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.M2:
                        {
                            CommunicationV2M2FlagPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.X1:
                        {
                            CommunicationV2X1FlagPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.X2:
                        {
                            CommunicationV2X2FlagPeriodicFunction();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("PeriodicTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------

            // unlock periodic timer
            FlagPeriodicFunctionTimerLocker = false;
        }

        //----------------------------------------------------------------------------------------------------

        private static void HardwareStatusPeriodicFunctionTimerCallbackFunction(object state)
        {
            HardwareStatusPeriodicFunctionTimerActiveFlag = true;

            if (HardwareStatusPeriodicFunctionTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(HardwareStatusPeriodicTimerFunction));
                thread.Start();
            }
        }

        private static void HardwareStatusPeriodicTimerFunction()
        {
            // lock periodic timer
            HardwareStatusPeriodicFunctionTimerLocker = true;

            //-------------------------------------------------------------------

            try
            {
                switch (DynaLinkHS.StatusApp.HardwareVersion)
                {
                    case 0x01:
                        {
                            break;
                        }
                    case 0x02:
                        {
                            CommunicationV2M2HardwareStatusPeriodicFunction();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("PeriodicTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------

            // unlock periodic timer
            HardwareStatusPeriodicFunctionTimerLocker = false;
        }

        //----------------------------------------------------------------------------------------------------

        private static void RobotStatusPeriodicFunctionTimerCallbackFunction(object state)
        {
            RobotStatusPeriodicFunctionTimerActiveFlag = true;
			
            if (RobotStatusPeriodicFunctionTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(RobotStatusPeriodicTimerFunction));
                thread.Start();
            }
        }

        private static void RobotStatusPeriodicTimerFunction()
        {
            // lock periodic timer
            RobotStatusPeriodicFunctionTimerLocker = true;

            //-------------------------------------------------------------------

            try
            {
                switch (DynaLinkHS.StatusApp.RobotType)
                {
                    case DynaLinkHSPara.RobotType.M1:
                        {
                            CommunicationV2M1RobotStatusPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.M2:
                        {
                            CommunicationV2M2RobotStatusPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.X1:
                        {
                            CommunicationV2X1RobotStatusPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.X2:
                        {
                            CommunicationV2X2RobotStatusPeriodicFunction();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("PeriodicTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------

            // unlock periodic timer
            RobotStatusPeriodicFunctionTimerLocker = false;
        }

        //----------------------------------------------------------------------------------------------------

        private static void RobotSensorStatusPeriodicFunctionTimerCallbackFunction(object state)
        {
            RobotSensorStatusPeriodicFunctionTimerActiveFlag = true;

            if (RobotSensorStatusPeriodicFunctionTimerLocker == false)
            {
                Thread thread = new Thread(new ThreadStart(RobotSensorStatusPeriodicTimerFunction));
                thread.Start();
            }
        }

        private static void RobotSensorStatusPeriodicTimerFunction()
        {
            // lock periodic timer
            RobotSensorStatusPeriodicFunctionTimerLocker = true;

            //-------------------------------------------------------------------

            try
            {
                switch (DynaLinkHS.StatusApp.RobotType)
                {
                    case DynaLinkHSPara.RobotType.M1:
                        {
                            CommunicationV2M1RobotSensorStatusPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.M2:
                        {
                            CommunicationV2M2RobotSensorStatusPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.X1:
                        {
                            CommunicationV2X1RobotSensorStatusPeriodicFunction();
                            break;
                        }
                    case DynaLinkHSPara.RobotType.X2:
                        {
                            CommunicationV2X2RobotSensorStatusPeriodicFunction();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception exception)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("PeriodicTimerFunction() catch exception : " + exception);
            }

            //-------------------------------------------------------------------

            // unlock periodic timer
            RobotSensorStatusPeriodicFunctionTimerLocker = false;
        }

        //-------------------------------------------- Period Function Definition (V2) -------------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2 Hardware) ----------------------------------------------------------------

        private static FunctionResult CommunicationV2M2HardwareStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for hardware information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RequestGetGpioIoStatus();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for hardware information ---------------------------------------------

            return FunctionResult.Success;
        }

        //-------------------------------------------- Period Function Definition (V2 Hardware) ----------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2 M1) ----------------------------------------------------------------------
        
        private static FunctionResult CommunicationV2M1FlagPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadFlagInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadFlagOutOfJointLimitInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadFlagOutOfEndEffectorLimitInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }
            
            // request for robot information ------------------------------------------------

            // request for task information -------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1TaskInterface.RequestReadFlagInformation();
            
            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for task information -------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2M1RobotStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for hardware information ---------------------------------------------

            //functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RequestReadGpioIoStatus();

            //if (functionResult == FunctionResult.SocketException)
            //{
            //            FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
            //            ServerLinkMonitorCount = 0;
            //            Disconnect(); // the server is down, close socket
            //            return FunctionResult.Fail;
            //}

            // request for hardware information ---------------------------------------------

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadJointInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadEndEffectorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }
            
            // request for robot information ------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2M1RobotSensorStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for robot sensor information -----------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadButtonSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadTorqueSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadButtonSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M1RobotInterface.RequestReadTorqueSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for robot sensor information -----------------------------------------

            return FunctionResult.Success;
        }

        //-------------------------------------------- Period Function Definition (V2 M1) ----------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2 M2) ----------------------------------------------------------------------
        
        private static FunctionResult CommunicationV2M2FlagPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadFlagInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadFlagOutOfJointLimitInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadFlagOutOfEndEffectorLimitInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }
            
            // request for robot information ------------------------------------------------

            // request for task information -------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2TaskInterface.RequestReadFlagInformation();
            
            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for task information -------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2M2RobotStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for hardware information ---------------------------------------------

            //functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RequestReadGpioIoStatus();

            //if (functionResult == FunctionResult.SocketException)
            //{
            //            FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
            //            ServerLinkMonitorCount = 0;
            //            Disconnect(); // the server is down, close socket
            //            return FunctionResult.Fail;
            //}

            // request for hardware information ---------------------------------------------

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadJointInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadEndEffectorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }
            
            // request for robot information ------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2M2RobotSensorStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for robot sensor information -----------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadButtonSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadForceSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadButtonSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2M2RobotInterface.RequestReadForceSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for robot sensor information -----------------------------------------

            return FunctionResult.Success;
        }

        //-------------------------------------------- Period Function Definition (V2 M2) ----------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2 X1) ----------------------------------------------------------------------

        private static FunctionResult CommunicationV2X1FlagPeriodicFunction()
        {
            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2X1RobotStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for hardware information ---------------------------------------------

            //functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RequestReadGpioIoStatus();

            //if (functionResult == FunctionResult.SocketException)
            //{
            //            FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
            //            ServerLinkMonitorCount = 0;
            //            Disconnect(); // the server is down, close socket
            //            return FunctionResult.Fail;
            //}

            // request for hardware information ---------------------------------------------

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X1RobotInterface.RequestReadForceSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X1RobotInterface.RequestReadJointInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X1RobotInterface.RequestReadEndEffectorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for robot information ------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2X1RobotSensorStatusPeriodicFunction()
        {
            return FunctionResult.Success;
        }

        //-------------------------------------------- Period Function Definition (V2 X1) ----------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2 X2) ----------------------------------------------------------------------

        private static FunctionResult CommunicationV2X2FlagPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadFlagInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadFlagOutOfJointLimitInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadFlagOutOfEndEffectorLimitInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for robot information ------------------------------------------------

            // request for task information -------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2TaskInterface.RequestReadFlagInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for task information -------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2X2RobotStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for hardware information ---------------------------------------------

            //functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2HardwareInterface.RequestReadGpioIoStatus();

            //if (functionResult == FunctionResult.SocketException)
            //{
            //            FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
            //            ServerLinkMonitorCount = 0;
            //            Disconnect(); // the server is down, close socket
            //            return FunctionResult.Fail;
            //}

            // request for hardware information ---------------------------------------------

            // request for robot information ------------------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadDriverAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadJointInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadEndEffectorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for robot information ------------------------------------------------

            return FunctionResult.Success;
        }

        private static FunctionResult CommunicationV2X2RobotSensorStatusPeriodicFunction()
        {
            FunctionResult functionResult;

            // request for robot sensor information -----------------------------------------

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadButtonSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadForceSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadFootPressureSensorAccessible();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadButtonSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadForceSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationManager.Instance.FFTAICommunicationV2X2RobotInterface.RequestReadFootPressureSensorInformation();

            if (functionResult == FunctionResult.SocketException)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Socket Exception, Server is Down.");
                ServerLinkMonitorCount = 0;
                Disconnect(); // the server is down, close socket
                return FunctionResult.Fail;
            }

            // request for robot sensor information -----------------------------------------

            return FunctionResult.Success;
        }

        //-------------------------------------------- Period Function Definition (V2 X2) ----------------------------------------------------------------------

        //-------------------------------------------- Period Function Definition (V2) -------------------------------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationOperation) ---------------------------------------------------------
        
        public FunctionResult ConnectedHandle()
        {
            DynaLinkHS.StatusFlag.FlagServerLinkActive = 0x01;

            return FunctionResult.Success;
        }

        public FunctionResult DisconnectedHandle()
        {
            DynaLinkHS.StatusFlag.FlagServerLinkActive = 0x00;

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationOperation) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (V2) --------------------------------------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2Interface) -------------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2IAPInterface) ----------------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2IAPInterfaceModel model)
        {
            FFTAICommunicationV2IAPInterfaceOperationMode operationMode =
                (FFTAICommunicationV2IAPInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2OperationResult operationResult =
                (FFTAICommunicationV2OperationResult)model.DataSectionModel.ResponseModel.OperationResult;

            switch (operationMode)
            {
                case FFTAICommunicationV2IAPInterfaceOperationMode.SoftwareVersion:
                    {
                        StatusIAP.SoftwareVersion = 
                            BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                        
                        break;
                    }
                case FFTAICommunicationV2IAPInterfaceOperationMode.BootMode:
                    {
                        StatusIAP.IAPBootMode = 
						    (DynaLinkHSPara.IAPBootMode)BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
					
                        break;
                    }
                case FFTAICommunicationV2IAPInterfaceOperationMode.WorkStatus:
                    {
                        StatusIAP.IAPWorkStatus = 
						    (DynaLinkHSPara.IAPWorkStatus)BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);

                        break;
                    }
                case FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeIap:
                case FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeIapFast:
                    {
                        switch (operationResult)
                        {
                            case FFTAICommunicationV2OperationResult.Success:

                                FFTAICommunicationManager.Instance.Logger.WriteLine("IAP Interface Receive Upgrade IAP Response : Success.");

                                IAPUpgradeIapTransmitLocker = false;

                                break;
                            case FFTAICommunicationV2OperationResult.Fail:

                                FFTAICommunicationManager.Instance.Logger.WriteLine("IAP Interface Receive Upgrade IAP Response : Fail.");

                                IAPUpgradeIapResendRequestFlag = true;
                                IAPUpgradeIapTransmitLocker = false;

                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeApp:
                case FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeAppFast:
                    {
                        switch (operationResult)
                        {
                            case FFTAICommunicationV2OperationResult.Success:

                                FFTAICommunicationManager.Instance.Logger.WriteLine("IAP Interface Receive Upgrade APP Response : Success.");

                                IAPUpgradeAppTransmitLocker = false;

                                break;
                            case FFTAICommunicationV2OperationResult.Fail:

                                FFTAICommunicationManager.Instance.Logger.WriteLine("IAP Interface Receive Upgrade APP Response : Fail.");

                                IAPUpgradeAppResendRequestFlag = true;
                                IAPUpgradeAppTransmitLocker = false;

                                break;
                            default:
                                break;
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2IAPInterface) ----------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2SystemInterface) -------------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2SystemInterfaceModel model)
        {
            FFTAICommunicationV2SystemInterfaceOperationMode operationMode =
                (FFTAICommunicationV2SystemInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            switch (operationMode)
            {
                case FFTAICommunicationV2SystemInterfaceOperationMode.HardwareVersion:

                    StatusApp.HardwareVersion = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);

                    break;
                case FFTAICommunicationV2SystemInterfaceOperationMode.SoftwareVersion:
                    
                    StatusApp.SoftwareVersion = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                    
                    break;
                case FFTAICommunicationV2SystemInterfaceOperationMode.SerialNumber:
                    
                    StatusApp.SerialNumber = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                    
                    break;
                case FFTAICommunicationV2SystemInterfaceOperationMode.RobotType:

                    StatusApp.RobotType = (DynaLinkHSPara.RobotType)BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);

                    break;
                case FFTAICommunicationV2SystemInterfaceOperationMode.MechanismVersion:

                    StatusApp.MechanismType = (DynaLinkHSPara.MechanismType)BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);

                    break;
                default:
                    break;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2SystemInterface) -------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2CommunicationInterface) ------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2CommunicationInterfaceModel model)
        {
            FFTAICommunicationV2CommunicationInterfaceOperationMode operationMode =
                (FFTAICommunicationV2CommunicationInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            switch (operationMode)
            {
                case FFTAICommunicationV2CommunicationInterfaceOperationMode.ErrorRequest:
                    {
                        break;
                    }
                case FFTAICommunicationV2CommunicationInterfaceOperationMode.SystemError:
                    {
                        break;
                    }
                case FFTAICommunicationV2CommunicationInterfaceOperationMode.TestConnection:
                    {
                        // FFTAICommunicationManager.Instance.Logger.WriteLine("Receive Connection Heart Beat Response !");
                        ServerLinkMonitorCount = 0; // clear server socket count
                        StatusFlag.FlagServerLinkActive = 0x01;

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2CommunicationInterface) ------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2HardwareInterface) -----------------------------------------------
        
        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2HardwareInterfaceModel model)
        {
            FFTAICommunicationV2HardwareInterfaceOperationMode operationMode =
                (FFTAICommunicationV2HardwareInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            switch (operationMode)
            {
                case FFTAICommunicationV2HardwareInterfaceOperationMode.None:
                    {
                        break;
                    }
                case FFTAICommunicationV2HardwareInterfaceOperationMode.ODLGpioIOStatus:
                    {
                        StatusDigiOutput.ODL[0] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                        StatusDigiOutput.ODL[1] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                        StatusDigiOutput.ODL[2] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                        StatusDigiOutput.ODL[3] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);

                        break;
                    }
                case FFTAICommunicationV2HardwareInterfaceOperationMode.IDLGpioIOStatus:
                    {
                        StatusDigiInput.IDL[0] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                        StatusDigiInput.IDL[1] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                        StatusDigiInput.IDL[2] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                        StatusDigiInput.IDL[3] = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2HardwareInterface) -----------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2RobotInterface) --------------------------------------------------
        
        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2RobotInterfaceModel model)
        {
            FFTAICommunicationV2RobotInterfaceOperationMode operationMode =
                (FFTAICommunicationV2RobotInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            switch (operationMode)
            {
                default:
                    {
                        break;
                    }
            }
            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2RobotInterface) --------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2M1Interface) -----------------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2M1RobotInterfaceModel model)
        {
            FFTAICommunicationV2M1RobotInterfaceOperationMode operationMode =
                (FFTAICommunicationV2M1RobotInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2ReadWriteOperation readWriteOperation =
                (FFTAICommunicationV2ReadWriteOperation)model.DataSectionModel.ResponseModel.ReadWriteOperation;

            switch (operationMode)
            {
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.FlagInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagCalibration = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagEmergentStop = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagFault = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagServoOn = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.FlagOutOfJointLimitInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagOutOfJointKineticLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagOutOfJointAccelerationLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagOutOfJointVelocityLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagOutOfJointPositionLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.FlagOutOfEndEffectorLimitInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagOutOfEndEffectorKineticLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagOutOfEndEffectorAccelerationLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagOutOfEndEffectorVelocityLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagOutOfEndEffectorPositionLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.ButtonSensorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusDigiInput.IDL[0] = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.ButtonSensorAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusSensor.ButtonSensor1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.TorqueSensorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusSensor.ADCSensor1.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        
                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.TorqueSensorAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusSensor.ADCSensor1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.DriverAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusRobot.Driver1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.JointInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusRobot.KineticDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusRobot.VelocityDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusRobot.PositionDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        StatusRobotUpdateNotification();

                        break;
                    }
                case FFTAICommunicationV2M1RobotInterfaceOperationMode.EndEffectorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusRobot.KineticDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusRobot.VelocityDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusRobot.PositionDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    break;
                                }
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                {
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        StatusRobotUpdateNotification();

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2M1TaskInterfaceModel model)
        {
            FFTAICommunicationV2M1TaskInterfaceOperationMode operationMode =
                (FFTAICommunicationV2M1TaskInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2ReadWriteOperation readWriteOperation =
                (FFTAICommunicationV2ReadWriteOperation)model.DataSectionModel.ResponseModel.ReadWriteOperation;

            switch (operationMode)
            {
                case FFTAICommunicationV2M1TaskInterfaceOperationMode.BasicFlagInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusFlag.FlagTaskInProcess = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2M1Interface) -----------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2M2Interface) -----------------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2M2RobotInterfaceModel model)
        {
            FFTAICommunicationV2M2RobotInterfaceOperationMode operationMode =
                (FFTAICommunicationV2M2RobotInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2ReadWriteOperation readWriteOperation =
                (FFTAICommunicationV2ReadWriteOperation)model.DataSectionModel.ResponseModel.ReadWriteOperation;

            switch (operationMode)
            {
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusFlag.FlagCalibration = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusFlag.FlagEmergentStop = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusFlag.FlagFault = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusFlag.FlagServoOn = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagOutOfJointLimitInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagOutOfJointKineticLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagOutOfJointAccelerationLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagOutOfJointVelocityLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagOutOfJointPositionLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagOutOfEndEffectorLimitInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagOutOfEndEffectorKineticLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagOutOfEndEffectorAccelerationLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagOutOfEndEffectorVelocityLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagOutOfEndEffectorPositionLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.ButtonSensorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusDigiInput.IDL[0] = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.ButtonSensorAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ButtonSensor1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.ForceSensorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ADCSensor1.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ADCSensor2.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.ForceSensorAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ButtonSensor1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ButtonSensor2.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.DriverAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusRobot.Driver1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusRobot.Driver2.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.JointInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusRobot.KineticDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusRobot.KineticDataJoint2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusRobot.VelocityDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusRobot.VelocityDataJoint2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                StatusRobot.PositionDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[4]), 0);
                                StatusRobot.PositionDataJoint2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[5]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        StatusRobotUpdateNotification();

                        break;
                    }
                case FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusRobot.KineticDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusRobot.KineticDataEndEffectorY1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusRobot.VelocityDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusRobot.VelocityDataEndEffectorY1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                StatusRobot.PositionDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[4]), 0);
                                StatusRobot.PositionDataEndEffectorY1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[5]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        StatusRobotUpdateNotification();

                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2M2TaskInterfaceModel model)
        {
            FFTAICommunicationV2M2TaskInterfaceOperationMode operationMode =
                (FFTAICommunicationV2M2TaskInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2ReadWriteOperation readWriteOperation =
                (FFTAICommunicationV2ReadWriteOperation)model.DataSectionModel.ResponseModel.ReadWriteOperation;

            switch (operationMode)
            {
                case FFTAICommunicationV2M2TaskInterfaceOperationMode.BasicFlagInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusFlag.FlagTaskInProcess = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2M2Interface) -----------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2X1Interface) -----------------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2X1RobotInterfaceModel model)
        {
            return FunctionResult.Success;
        }

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2X1TaskInterfaceModel model)
        {
            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2X1Interface) -----------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2X2Interface) -----------------------------------------------------

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2X2RobotInterfaceModel model)
        {
            FFTAICommunicationV2X2RobotInterfaceOperationMode operationMode =
                (FFTAICommunicationV2X2RobotInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2ReadWriteOperation readWriteOperation =
                (FFTAICommunicationV2ReadWriteOperation)model.DataSectionModel.ResponseModel.ReadWriteOperation;

            switch (operationMode)
            {
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.FlagInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusFlag.FlagCalibration = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusFlag.FlagEmergentStop = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusFlag.FlagFault = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusFlag.FlagServoOn = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.FlagOutOfJointLimitInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagOutOfJointKineticLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagOutOfJointAccelerationLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagOutOfJointVelocityLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagOutOfJointPositionLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.FlagOutOfEndEffectorLimitInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                {
                                    StatusFlag.FlagOutOfEndEffectorKineticLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                    StatusFlag.FlagOutOfEndEffectorAccelerationLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                    StatusFlag.FlagOutOfEndEffectorVelocityLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                    StatusFlag.FlagOutOfEndEffectorPositionLimit = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.ButtonSensorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusDigiInput.IDL[0] = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.ButtonSensorAccessible:
                    {
                        switch (readWriteOperation)
                        {

                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ButtonSensor1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ButtonSensor2.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusSensor.ButtonSensor3.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusSensor.ButtonSensor4.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.ForceSensorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ADCSensor1.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ADCSensor2.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusSensor.ADCSensor3.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusSensor.ADCSensor4.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.ForceSensorAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ADCSensor1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ADCSensor2.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusSensor.ADCSensor3.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusSensor.ADCSensor4.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.FootPressureInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ADCSensor5.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ADCSensor6.CalculateValue = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.FootPressureAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusSensor.ADCSensor5.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusSensor.ADCSensor6.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.DriverAccessible:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusRobot.Driver1.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusRobot.Driver2.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusRobot.Driver3.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusRobot.Driver4.Accessible = (uint)BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                break;
                            default:
                                break;
                        }

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.JointInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:

                                StatusRobot.KineticDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusRobot.KineticDataJoint2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusRobot.KineticDataJoint3 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusRobot.KineticDataJoint4 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                StatusRobot.VelocityDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[4]), 0);
                                StatusRobot.VelocityDataJoint2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[5]), 0);
                                StatusRobot.VelocityDataJoint3 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[6]), 0);
                                StatusRobot.VelocityDataJoint4 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[7]), 0);
                                StatusRobot.PositionDataJoint1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[8]), 0);
                                StatusRobot.PositionDataJoint2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[9]), 0);
                                StatusRobot.PositionDataJoint3 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[10]), 0);
                                StatusRobot.PositionDataJoint4 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[11]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        StatusRobotUpdateNotification();

                        break;
                    }
                case FFTAICommunicationV2X2RobotInterfaceOperationMode.EndEffectorInformation:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:

                                StatusRobot.KineticDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                StatusRobot.KineticDataEndEffectorY1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[1]), 0);
                                StatusRobot.VelocityDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[2]), 0);
                                StatusRobot.VelocityDataEndEffectorY1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[3]), 0);
                                StatusRobot.PositionDataEndEffectorX1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[4]), 0);
                                StatusRobot.PositionDataEndEffectorY1 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[5]), 0);

                                StatusRobot.KineticDataEndEffectorX2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[6]), 0);
                                StatusRobot.KineticDataEndEffectorY2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[7]), 0);
                                StatusRobot.VelocityDataEndEffectorX2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[8]), 0);
                                StatusRobot.VelocityDataEndEffectorY2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[9]), 0);
                                StatusRobot.PositionDataEndEffectorX2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[10]), 0);
                                StatusRobot.PositionDataEndEffectorY2 = BitConverter.ToSingle(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[11]), 0);

                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }

                        StatusRobotUpdateNotification();

                        break;
                    }
                default:
                    break;
            }

            return FunctionResult.Success;
        }

        public FunctionResult ModelUpdateHandle(FFTAICommunicationV2X2TaskInterfaceModel model)
        {
            FFTAICommunicationV2X2TaskInterfaceOperationMode operationMode =
                (FFTAICommunicationV2X2TaskInterfaceOperationMode)model.DataSectionModel.ResponseModel.OperationMode;

            FFTAICommunicationV2ReadWriteOperation readWriteOperation =
                (FFTAICommunicationV2ReadWriteOperation)model.DataSectionModel.ResponseModel.ReadWriteOperation;

            switch (operationMode)
            {
                case FFTAICommunicationV2X2TaskInterfaceOperationMode.BasicFlagTaskInProcess:
                    {
                        switch (readWriteOperation)
                        {
                            case FFTAICommunicationV2ReadWriteOperation.Get:
                            case FFTAICommunicationV2ReadWriteOperation.Read:
                                StatusFlag.FlagTaskInProcess = BitConverter.ToUInt32(BitConverter.GetBytes(model.DataSectionModel.ResponseModel.Parameter[0]), 0);
                                break;
                            case FFTAICommunicationV2ReadWriteOperation.Set:
                            case FFTAICommunicationV2ReadWriteOperation.Write:
                                break;
                            default:
                                break;
                        }
                        
                        break;
                    }
                default:
                    {
                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Function (FFTAICommunicationV2X2Interface) -----------------------------------------------------

        //-------------------------------------------- Observer Function (FFTAICommunicationV2Interface) -------------------------------------------------------

        //-------------------------------------------- Observer Function (V2) ----------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------------------------------------------
    }
}
