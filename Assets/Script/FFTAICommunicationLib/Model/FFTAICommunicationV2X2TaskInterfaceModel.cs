using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2X2TaskInterfaceModel
    {
        public FFTAICommunicationV2DataSectionModel DataSectionModel;

        // model initilization
        public FFTAICommunicationV2X2TaskInterfaceModel()
        {
            DataSectionModel = new FFTAICommunicationV2DataSectionModel();
        }
    }

}
