using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FFTAICommunicationLib;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2IAPInterfaceModel
    {
        public FFTAICommunicationV2DataSectionModel DataSectionModel;

        // model initilization
        public FFTAICommunicationV2IAPInterfaceModel()
        {
            DataSectionModel = new FFTAICommunicationV2DataSectionModel();
        }
    
    }

}
