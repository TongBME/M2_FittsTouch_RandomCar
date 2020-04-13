using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2X1TaskInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2X1TaskInterfaceModel model);
    }
}
