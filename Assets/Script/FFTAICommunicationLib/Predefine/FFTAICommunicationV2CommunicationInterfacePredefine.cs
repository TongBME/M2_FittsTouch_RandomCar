using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2CommunicationInterfaceOperationMode
    {
        ErrorRequest = 0x07FFFFFF,
        SystemError = 0x06FFFFFF,

        TestConnection = 0x01010101,
    };
}
