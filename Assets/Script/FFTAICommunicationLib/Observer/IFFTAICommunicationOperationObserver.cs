using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public interface IFFTAICommunicationOperationObserver
    {
        FunctionResult ReceiveMessageHandle(byte[] message, uint messageLength);
    }

    public interface IFFTAICommunicationOperationConnectionStatusObserver
    {
        FunctionResult ConnectedHandle();
        FunctionResult DisconnectedHandle();
    }
}
