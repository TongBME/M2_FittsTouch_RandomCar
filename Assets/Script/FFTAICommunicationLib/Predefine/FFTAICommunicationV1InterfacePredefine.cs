using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV1OperationMode : int
    {
        None = 0x00,

        SysStatusFeedback = 0xFD,
        MotorMotionFeedback = 0xFC,
        CommandFeeback = 0xFB,
        MiscMotionFeedback = 0xFA,
    }
}
