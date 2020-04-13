using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2M2TaskInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2M2TaskInterfaceModel model);
    }
}
