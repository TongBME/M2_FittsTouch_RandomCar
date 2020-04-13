using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2CommunicationFrameType
    {
        None,

        HardwareInterface,
        MotorInterface,
        RobotInterface,
        M1TaskInterface,
        M2Interface,
        M3Interface,
        H1Interface,
        X1TaskInterface,
        X2TaskInterface,
    }

    public enum FFTAICommunicationV2ProtocolVersion : uint
    {
        None = 0x00,

        Version1 = 0x0001,
        Version2 = 0x0002,
    }

    public enum FFTAICommunicationV2RobotType : uint
    {
        None = 0x00,

        H1 = 0x48310000,
        M1 = 0x4D310000,
        M2 = 0x4D320000,
        X1 = 0x58310000,
        X2 = 0x58320000,

        HSeries = 0x48000000,
        MSeries = 0x4D000000,
        XSeries = 0x58000000,

        All = 0x7FFFFFFF,
    }

    public enum FFTAICommunicationV2FunctionType : uint
    {
        None = 0x00,

        IAPInterface = 0x7AAAAAAA,

        CommunicationInterface = 0x7FFFFFFF,

        SystemInterface = 0x01010101,
        HardwareInterface = 0x01010201,
        DriverInterface = 0x01010301,
        MotorInterface = 0x01010401,
        RobotInterface = 0x01010501,

        M1RobotInterface = 0x4D310101,
        M2RobotInterface = 0x4D320101,
        X1RobotInterface = 0x58310101,
        X2RobotInterface = 0x58320101,

        M1ControlInterface = 0x4D310102,
        M2ControlInterface = 0x4D320102,
        X1ControlInterface = 0x58310102,
        X2ControlInterface = 0x58320102,

        M1TaskInterface = 0x4D310103,
        M2TaskInterface = 0x4D320103,
        X1TaskInterface = 0x58310103,
        X2TaskInterface = 0x58320103,
    }

    public enum FFTAICommunicationV2NumberOfParameter : uint
    {
        Zero = 0x00,
        One = 0x01,
        Two = 0x02,
        Three = 0x03,
        Four = 0x04,
        Five = 0x05,
        Six = 0x06,
        Seven = 0x07,
        Eight = 0x08,
        Nine = 0x09,
		Ten = 0x0A,
		Eleven = 0x0B,
		Twelve = 0x0C,
		Thirteen = 0x0D,
		Fourteen = 0x0E,
		Fifteen = 0x0F,
    }

    public enum FFTAICommunicationV2Saved : uint
    {
        Zero = 0x00,
    }

    public enum FFTAICommunicationV2ReadWriteOperation : uint
    {
        Get = 0x00,
        Set = 0x01,
        Read = 0x10,
        Write = 0x11,
    }
    
    public enum FFTAICommunicationV2OperationResult : uint
    {
        Success = 0x00,
        Fail = 0x01,
    }

}
