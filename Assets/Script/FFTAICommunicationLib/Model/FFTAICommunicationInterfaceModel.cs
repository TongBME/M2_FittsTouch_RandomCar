using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationInterfaceModel
    {
        public FFTAICommunicationProtocolVersion FFTAICommunicationProtocolVersion;

        public byte[] ReceiveMessageBuf = new byte[2048];
        public uint ReceiveMessageBufLength = 0;

		public byte[] SendMessageBuf = new byte[2048];
        public uint SendMessageBufLength = 0;

        // model initilization
        public FFTAICommunicationInterfaceModel()
        {

        }
    
    }

}
