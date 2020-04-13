using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2CommunicationInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2CommunicationInterfaceModel model);
    }
}
