/*
 * 文件说明：
 *      Basic Socket Operation Class
 * 
 * Reference:
 *      https://www.cnblogs.com/sdyinfang/p/5519708.html
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

using System.Net;
using System.Net.Sockets;

namespace FFTAICommunicationLib
{
    public class BasicUdpClientOperation
    {

        //-------------------------------------------- UDP Property ---------------------------------------------

        private string UdpConnectRemoteIpAddressString;
        private IPAddress UdpConnectRemoteIpAddress;
        private int UdpConnectRemotePort;
        private IPEndPoint UdpConnectRemoteEndPoint;
        
        private int UdpConnectLocalPort = 4196;

        public bool UdpConnectStatus;

        private UdpClient UdpClient;

        //-------------------------------------------- UDP Property ---------------------------------------------

        public BasicUdpClientOperation()
        {
        }
        
        //-------------------------------------------- UDP Functions --------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public FunctionResult UdpConnectLocalToRemote(string ipAddress, int port)
        {
            // build remote ip end point
            UdpConnectRemoteIpAddressString = ipAddress;
            UdpConnectRemotePort = port;

            UdpConnectRemoteIpAddress = IPAddress.Parse(UdpConnectRemoteIpAddressString);

            UdpConnectRemoteEndPoint = new IPEndPoint(UdpConnectRemoteIpAddress, UdpConnectRemotePort);

            // build udp client
            if (UdpClient == null)
            {
                UdpClient = new UdpClient(UdpConnectLocalPort);
            }

            // connect local to remote
            UdpClient.Connect(UdpConnectRemoteEndPoint);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult UdpDisconnectLocalToRemote()
        {
            if (UdpClient != null)
            {
                UdpClient.Close();
            }

            UdpClient = null;

            return FunctionResult.Success;
        }

        /// <summary>  
        /// 向远端主机的端口发送数据报  
        /// </summary>  
        public FunctionResult UdpConnectLocalSendMessage(byte[] message, uint messageLength)
        {
            // in case of UdpClient is null itself
            if (UdpClient == null)
            {
                return FunctionResult.Fail;
            }

            try
            {
                UdpClient.Send(message, (int)messageLength);
            }
            catch (SocketException)
            {
                return FunctionResult.SocketException;
            }

            return FunctionResult.Success;
        }

        /// <summary>  
        /// 接收发送给本机ip对应端口号的数据报  
        /// </summary>  
        public FunctionResult UdpConnectLocalReceiveMessage(ref byte[] message, ref uint messageLength)
        {
            // in case of UdpClient is null itself
            if (UdpClient == null)
            {
                return FunctionResult.Fail;
            }

            try
            {
                message = UdpClient.Receive(ref UdpConnectRemoteEndPoint);
                messageLength = (uint)message.Length;
            }
            catch (SocketException)
            {
                return FunctionResult.SocketException;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- UDP Functions --------------------------------------------
    }
}
