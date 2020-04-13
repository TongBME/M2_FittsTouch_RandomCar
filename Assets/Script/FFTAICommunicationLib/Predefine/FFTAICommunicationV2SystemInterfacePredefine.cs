using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2SystemInterfaceOperationMode
    {
        InitedFlag = 0x0F010101,

        HardwareVersion = 0x01010101,
        SoftwareVersion = 0x01010102,

        SerialNumber = 0x01010201,

        RunTimeCount = 0x0A010101,
        EthernetType = 0x0A010103,
        EthernetTcpFrameworkType = 0x0A010104,
        EthernetUdpFrameworkType = 0x0A010105,
        RobotType = 0x0A010106,
        MechanismVersion = 0x0A010107,
        CommunicationProtocolVersion = 0x0A010108,
    };
}
