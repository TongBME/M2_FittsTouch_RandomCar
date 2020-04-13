using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <note>
    /// 
    /// 可以通过配置变量的权限，限制外界调用这个库里面内容的权限（部分限制，不是绝对的）
    /// When define a variable, you can use `public` or `private` to limit the control authority
    /// 
    /// </note>
    public sealed class FFTAICommunicationManager
    {

        //-------------------------------------------- Variables Definition ------------------------------------------------

        // config
        public FFTAICommunicationConfig Config;

        // log
        public FFTAICommunicationLog Logger;

        // basic configuration
        public FFTAICommunicationRobotType RobotType;
        public FFTAICommunicationProtocolVersion ProtocolVersion;

        // socket
        public FFTAICommunicationOperation FFTAICommunicationOperation;

        // interface
        private FFTAICommunicationInterface FFTAICommunicationInterface;

        // v1 interface
        public FFTAICommunicationV1Interface FFTAICommunicationV1Interface;

        // v2 interface
        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        public FFTAICommunicationV2IAPInterface FFTAICommunicationV2IAPInterface;

        public FFTAICommunicationV2SystemInterface FFTAICommunicationV2SystemInterface;
        public FFTAICommunicationV2CommunicationInterface FFTAICommunicationV2CommunicationInterface;
        public FFTAICommunicationV2HardwareInterface FFTAICommunicationV2HardwareInterface;
        public FFTAICommunicationV2DriverInterface FFTAICommunicationV2DriverInterface;
        public FFTAICommunicationV2RobotInterface FFTAICommunicationV2RobotInterface;

        public FFTAICommunicationV2M1RobotInterface FFTAICommunicationV2M1RobotInterface;
        public FFTAICommunicationV2M1TaskInterface FFTAICommunicationV2M1TaskInterface;

        public FFTAICommunicationV2M2RobotInterface FFTAICommunicationV2M2RobotInterface;
        public FFTAICommunicationV2M2TaskInterface FFTAICommunicationV2M2TaskInterface;

        public FFTAICommunicationV2X1RobotInterface FFTAICommunicationV2X1RobotInterface;
        public FFTAICommunicationV2X1TaskInterface FFTAICommunicationV2X1TaskInterface;

        public FFTAICommunicationV2X2RobotInterface FFTAICommunicationV2X2RobotInterface;
        public FFTAICommunicationV2X2TaskInterface FFTAICommunicationV2X2TaskInterface;

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Singleton -----------------------------------------------------------

        private static volatile FFTAICommunicationManager instance;

        private FFTAICommunicationManager()
        {
            // create object
            Config = new FFTAICommunicationConfig();
            Logger = new FFTAICommunicationLog();

            FFTAICommunicationOperation = new FFTAICommunicationOperation();

            FFTAICommunicationInterface = new FFTAICommunicationInterface();

            FFTAICommunicationV1Interface = new FFTAICommunicationV1Interface();

            FFTAICommunicationV2Interface = new FFTAICommunicationV2Interface();

            FFTAICommunicationV2IAPInterface = new FFTAICommunicationV2IAPInterface();

            FFTAICommunicationV2SystemInterface = new FFTAICommunicationV2SystemInterface();
            FFTAICommunicationV2CommunicationInterface = new FFTAICommunicationV2CommunicationInterface();
            FFTAICommunicationV2HardwareInterface = new FFTAICommunicationV2HardwareInterface();
            FFTAICommunicationV2DriverInterface = new FFTAICommunicationV2DriverInterface();
            FFTAICommunicationV2RobotInterface = new FFTAICommunicationV2RobotInterface();

            FFTAICommunicationV2M1RobotInterface = new FFTAICommunicationV2M1RobotInterface();
            FFTAICommunicationV2M1TaskInterface = new FFTAICommunicationV2M1TaskInterface();

            FFTAICommunicationV2M2RobotInterface = new FFTAICommunicationV2M2RobotInterface();
            FFTAICommunicationV2M2TaskInterface = new FFTAICommunicationV2M2TaskInterface();

            FFTAICommunicationV2X1RobotInterface = new FFTAICommunicationV2X1RobotInterface();
            FFTAICommunicationV2X1TaskInterface = new FFTAICommunicationV2X1TaskInterface();

            FFTAICommunicationV2X2RobotInterface = new FFTAICommunicationV2X2RobotInterface();
            FFTAICommunicationV2X2TaskInterface = new FFTAICommunicationV2X2TaskInterface();

            // build relationship
            FFTAICommunicationOperation.AddObserver(FFTAICommunicationInterface);

            FFTAICommunicationInterface.FFTAICommunicationOperation = FFTAICommunicationOperation;
            FFTAICommunicationInterface.FFTAICommunicationV1Interface = FFTAICommunicationV1Interface;
            FFTAICommunicationInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;

            // v1 interface
            FFTAICommunicationV1Interface.FFTAICommunicationInterface = FFTAICommunicationInterface;
            
            // v2 interface
            FFTAICommunicationV2Interface.FFTAICommunicationInterface = FFTAICommunicationInterface;

            FFTAICommunicationV2Interface.IAPInterface = FFTAICommunicationV2IAPInterface;

            FFTAICommunicationV2Interface.SystemInterface = FFTAICommunicationV2SystemInterface;
            FFTAICommunicationV2Interface.CommunicationInterface = FFTAICommunicationV2CommunicationInterface;
            FFTAICommunicationV2Interface.HardwareInterface = FFTAICommunicationV2HardwareInterface;
            FFTAICommunicationV2Interface.DriverInterface = FFTAICommunicationV2DriverInterface;
            FFTAICommunicationV2Interface.RobotInterface = FFTAICommunicationV2RobotInterface;

            FFTAICommunicationV2Interface.M1RobotInterface = FFTAICommunicationV2M1RobotInterface;
            FFTAICommunicationV2Interface.M1TaskInterface = FFTAICommunicationV2M1TaskInterface;

            FFTAICommunicationV2Interface.M2RobotInterface = FFTAICommunicationV2M2RobotInterface;
            FFTAICommunicationV2Interface.M2TaskInterface = FFTAICommunicationV2M2TaskInterface;

            FFTAICommunicationV2Interface.X1RobotInterface = FFTAICommunicationV2X1RobotInterface;
            FFTAICommunicationV2Interface.X1TaskInterface = FFTAICommunicationV2X1TaskInterface;

            FFTAICommunicationV2Interface.X2RobotInterface = FFTAICommunicationV2X2RobotInterface;
            FFTAICommunicationV2Interface.X2TaskInterface = FFTAICommunicationV2X2TaskInterface;

            // children interface
            FFTAICommunicationV2IAPInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;

            FFTAICommunicationV2SystemInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2CommunicationInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2HardwareInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2DriverInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2RobotInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;

            FFTAICommunicationV2M1RobotInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2M1TaskInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;

            FFTAICommunicationV2M2RobotInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2M2TaskInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;

            FFTAICommunicationV2X1RobotInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2X1TaskInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;

            FFTAICommunicationV2X2RobotInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
            FFTAICommunicationV2X2TaskInterface.FFTAICommunicationV2Interface = FFTAICommunicationV2Interface;
        }

        public static FFTAICommunicationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FFTAICommunicationManager();
                }

                return instance;
            }
        }

        //-------------------------------------------- Singleton -----------------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------
        
        //-------------------------------------------- Function Definition -------------------------------------------------
    }
}


