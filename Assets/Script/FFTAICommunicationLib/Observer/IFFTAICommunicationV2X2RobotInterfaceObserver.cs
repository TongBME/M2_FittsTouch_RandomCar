using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationV2X2RobotInterfaceObserver
    {
        FunctionResult ModelUpdateHandle(FFTAICommunicationV2X2RobotInterfaceModel model);
    }
}
