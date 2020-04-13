using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FFTAICommunicationLib;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2RobotInterfaceModel
    {
        public FFTAICommunicationV2DataSectionModel DataSectionModel;

        // model initilization
        public FFTAICommunicationV2RobotInterfaceModel()
        {
            DataSectionModel = new FFTAICommunicationV2DataSectionModel();
        }
    
    }

}
