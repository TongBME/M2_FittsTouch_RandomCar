using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2X1RobotInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2X1RobotInterfaceModel model);
    }
}
