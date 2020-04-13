using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FFTAICommunicationLib;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2SystemInterfaceModel
    {
        public FFTAICommunicationV2DataSectionModel DataSectionModel;

        // model initilization
        public FFTAICommunicationV2SystemInterfaceModel()
        {
            DataSectionModel = new FFTAICommunicationV2DataSectionModel();
        }
    
    }

}
