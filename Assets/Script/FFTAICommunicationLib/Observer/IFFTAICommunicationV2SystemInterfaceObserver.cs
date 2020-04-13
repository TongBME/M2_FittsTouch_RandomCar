using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2SystemInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2SystemInterfaceModel model);
    }
}
