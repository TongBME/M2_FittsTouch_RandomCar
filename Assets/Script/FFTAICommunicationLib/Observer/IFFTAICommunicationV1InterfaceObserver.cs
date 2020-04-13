using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV1InterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV1InterfaceModel model);
    }
}
