using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2M1TaskInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2M1TaskInterfaceModel model);
    }
}
