using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2HardwareInterfaceOperationMode
    {
        None = 0x00,
        
        // Gpio Model
        ODLGpioIOStatus = 0x01010101,
        IDLGpioIOStatus = 0x01010102,

        GpioLedRedLedStatus = 0x01020101,
        GpioLedYellowLedStatus = 0x01020102,
        GpioLedBlueLedStatus = 0x01020103,
        
        // Can Model
        SendCanMessage = 0x02010101,
        ReceiveCanMessage = 0x02010102,
        SendCanopenMessage = 0x02010103,

    };
}
