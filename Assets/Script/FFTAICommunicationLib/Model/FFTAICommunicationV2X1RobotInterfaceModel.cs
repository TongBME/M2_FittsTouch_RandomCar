using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2X1RobotInterfaceModel
    {
        public FFTAICommunicationV2DataSectionModel DataSectionModel;

        // model initilization
        public FFTAICommunicationV2X1RobotInterfaceModel()
        {
            DataSectionModel = new FFTAICommunicationV2DataSectionModel();
        }

    }

}
