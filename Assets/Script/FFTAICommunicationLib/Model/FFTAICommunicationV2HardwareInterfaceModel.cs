using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2HardwareInterfaceModel
    {
        public FFTAICommunicationV2DataSectionModel DataSectionModel;

        // model initilization
        public FFTAICommunicationV2HardwareInterfaceModel()
        {
            DataSectionModel = new FFTAICommunicationV2DataSectionModel();
        }
    
    }

}
