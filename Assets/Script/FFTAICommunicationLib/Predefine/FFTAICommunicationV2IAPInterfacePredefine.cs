using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public enum FFTAICommunicationV2IAPInterfaceOperationMode
    {
        InitedFlag = 0x0F010101,
        SoftwareVersion = 0x0F010201,
        
        BootMode = 0x01010101,
        WorkStatus = 0x01010102,

        IapStartAddress = 0x01010201,
        IapSize = 0x01010202,

        AppStartAddress = 0x01010301,
        AppSize = 0x01010302,

		UpgradeIap = 0x02010101,
		UpgradeIapFast = 0x02010102,

        UpgradeApp = 0x03010101,
		UpgradeAppFast = 0x03010102,

		EraseIap = 0x04010101,
		EraseApp = 0x04010102,
    };
}
