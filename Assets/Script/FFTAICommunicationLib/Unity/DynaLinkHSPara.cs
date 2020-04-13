/**
 * @file DynalinkHSPara.cs
 * @brief Unity Communication Interface Parameter with MMU
 * @details 
 * @mainpage 
 * @author Jason(Chen Xin)
 * @email xin.chen@fftai.com
 * @version 1.0.0
 * @date 2019-01-24
 * @license Private
 */

using System;

namespace FFTAICommunicationLib
{
    public class DynaLinkHSPara
    {

        /// @brief IAP boot mode [启动模式]
		public enum IAPBootMode : uint
		{
            None = 0x00, ///< default [默认，进入 bootloader 模式]
            IAP = 0x01, ///< bootloader [进入 bootloader 模式]
            APP = 0x02, ///< application [进入 application 模式]
		}

        /// @brief IAP work status [运行状态]
		public enum IAPWorkStatus : uint
		{
            None = 0x00, ///< default [默认，无意义]
            IAP = 0x01, ///< bootloader [当前为 bootloader 模式]
            APP = 0x02, ///< application [当前为 application 模式]
		}

        /// @brief IAP upgrade status [底层程序升级状态]
		public enum IAPUpgradeStatus : uint
		{
            Ready = 0x00, ///< ready [准备]
            Running = 0x01, ///< running [运行中]
            Success = 0xAA, ///< success [成功]
            Fail = 0xFF, ///< fail [失败]
		}

        /// @brief robot type [机器人类型]
        public enum RobotType : uint
        {
            None = 0x00000000, ///< none [无类型，接口层使用无意义]

            H1 = 0x48810000, ///< H1 [H1 机器人]
            M1 = 0x4D310000, ///< M1 [M1 机器人]
            M2 = 0x4D320000, ///< M2 [M2 机器人]
            X1 = 0x58310000, ///< X1 [X1 机器人]
            X2 = 0x58320000, ///< X2 [X2 机器人]

            All = 0x7FFFFFFF, ///< All [所有机器人，接口层使用无意义]
        }

        /// @brief mechanism type [机器人类型]
        public enum MechanismType : uint
        {
            None = 0x00000000, ///< none [无类型，接口层使用无意义]

            V1 = 0x010101, ///< Verion1 [机械结构 第一版]
            V2 = 0x010102, ///< Verion2 [机械结构 第二版]
            V3 = 0x010103, ///< Verion3 [机械结构 第三版]
            V4 = 0x010104, ///< Verion4 [机械结构 第四版]
            V5 = 0x010105, ///< Verion5 [机械结构 第五版]
            V6 = 0x010106,
            V7 = 0x010107,
            V8 = 0x010108,
            V9 = 0x010109,

            MiniV1 = 0x020101, ///< Mini verion1 [机械结构 迷你版 第一版]
            MiniV2 = 0x020102, ///< Mini verion2 [机械结构 迷你版 第二版]
            MiniV3 = 0x020103,
            MiniV4 = 0x020104,
            MiniV5 = 0x020105,
            MiniV6 = 0x020106,
            MiniV7 = 0x020107,
            MiniV8 = 0x020108,
            MiniV9 = 0x020109,

            PlusV1 = 0x020201, ///< Plus verion1 [机械结构 加强版 第一版]
            PlusV2 = 0x020202, ///< Plus verion2 [机械结构 加强版 第二版]
            PlusV3 = 0x020203,
            PlusV4 = 0x020204,
            PlusV5 = 0x020205,
            PlusV6 = 0x020206,
            PlusV7 = 0x020207,
            PlusV8 = 0x020208,
            PlusV9 = 0x020209,

            AnkleV1 = 0x030101, ///< Ankle verion1 [机械结构 踝部版 第一版]
            AnkleV2 = 0x030102, ///< Ankle verion2 [机械结构 踝部版 第二版]

            WristV1 = 0x030201, ///< Wrist verion1 [机械结构 腕部版 第一版]
            WristV2 = 0x030202, ///< Wrist verion2 [机械结构 腕部版 第二版]
        }

        /// @brief work mode [工作模式]
        public enum WorkMode : uint
        { 
            Debug = 0x01, ///< Debug mode [调试模式]
            Relay = 0x02, ///< Relay mode [中继模式]
            MasterControl = 0x03, ///< Master control mode [主控模式]
        }

    }
}
