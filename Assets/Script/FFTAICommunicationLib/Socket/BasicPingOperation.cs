using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.NetworkInformation;
using System.Net;

namespace FFTAICommunicationLib
{
    class BasicPingOperation
    {
        public FunctionResult sendPing(string ipAddress)
        {
            Ping ping = null;
            PingReply pingReply = null;

            try
            {
                // Error : C# .Net Ping cannot work in Unity environment !!!
                
                ping = new Ping();

                pingReply = ping.Send(ipAddress);

                // Error : Using Unity.Engine.Ping cannot work in timer thread !!!
            }
            catch (Exception exception)
            {
                // there will be an exception for :
                // ArgumentException: The IPEndPoint was created using InterNetworkV6 AddressFamily but SocketAddress contains InterNetwork instead, please use the same type.
                //
                // but no big deal, ignore it!!!
            }

            if (ping == null || pingReply == null)
            {
                return FunctionResult.Fail;
            }

            if (pingReply.Status != IPStatus.Success)
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }
    }
}
