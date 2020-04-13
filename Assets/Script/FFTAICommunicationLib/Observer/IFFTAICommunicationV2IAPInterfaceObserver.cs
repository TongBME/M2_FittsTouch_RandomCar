using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2IAPInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2IAPInterfaceModel model);
    }
}
