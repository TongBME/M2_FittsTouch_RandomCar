using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2RobotInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2RobotInterfaceModel model);
    }
}
