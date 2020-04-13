using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2HardwareInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2HardwareInterfaceModel model);
    }
}
