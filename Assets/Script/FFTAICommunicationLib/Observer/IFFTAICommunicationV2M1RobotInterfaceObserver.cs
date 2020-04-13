using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2M1RobotInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2M1RobotInterfaceModel model);
    }
}
