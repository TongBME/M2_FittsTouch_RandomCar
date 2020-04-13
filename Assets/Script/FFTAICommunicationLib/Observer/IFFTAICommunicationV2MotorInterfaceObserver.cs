using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2MotorInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2MotorInterfaceModel model);
    }
}
