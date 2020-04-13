using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2X1TaskInterfaceOperationMode : uint
    {
        // Basic
        BasicWorkMode = 0x01010101,

        BasicFlagTaskInProcess = 0x01010201,

        // Debug
        DebugSetWorkMode = 0x01010101,

        // Relay
        RelayControlSetWorkMode = 0x02010101,

        // Master Control
        MasterControlSetWorkMode = 0x03010101,

        MasterControlSetWalkPassive1Command = 0x03110101,
        MasterControlSetWalkPassive2Command = 0x03120101,
        MasterControlSetWalkPassive3Command = 0x03130101,
        MasterControlSetWalkPassive4Command = 0x03140101,

        MasterControlSetSitStandTrainingCommand = 0x03210101,

    };

    public enum FFTAICommunicationV2X1TaskInterfaceWorkMode
    {
        Debug = 0x01,
        Relay = 0x02,
        MasterControl = 0x03,
    };

    public enum FFTAICommunicationV2X1TaskInterfaceMasterControlWorkMode
    {
        None = 0x00,
        WalkPassive1 = 0x11,
    }

    public enum FFTAICommunicationV2X1TaskInterfaceMasterControlWalkPassive1Command
    {
        SetHome = 0x10,
        Stop = 0x01,
        Sit = 0x02,
        Stand = 0x03,
        Walk = 0x04,
    }
}
