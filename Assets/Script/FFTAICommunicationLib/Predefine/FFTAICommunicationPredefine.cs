using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FunctionResult : int
    {
        None = 0xFF,
        Success = 0,
        Fail = -1,
        NoData,
        ObjectDisposedException,
        ArgumentException,
        ArgumentNullException,
        ArgumentOutOfRangeException,
        FormatException,
        SocketException,
        SecurityException,
        InvalidOperationException,
        PlatformNotSupportedException,
        NetworkAlreadyConnected,
        NetworkAlreadyDisConnected,
    }

    public enum NetworkConnectionType
    {
        None = 0,
        TCP = 1,
        UDP = 2,
    }

    public enum FFTAICommunicationProtocolVersion : uint
    {
        Version1 = 0x0001,
        Version2 = 0x0002,
    }

    public enum FFTAICommunicationRobotType : uint
    {
        M1,
        M2,
        X1,
        X2,
    }

}
