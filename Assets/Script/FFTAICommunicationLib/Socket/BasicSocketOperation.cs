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
    public class BasicSocketOperation
    {
        //-------------------------------------------- TCP Property ---------------------------------------------

        private string TcpConnectServerIpAddressString;
        private IPAddress TcpConnectServerIpAddress;
        private int TcpConnectServerPort;
        private EndPoint TcpConnectServerEndPoint;

        private string TcpConnectClientIpAddressString;
        private IPAddress TcpConnectClientIpAddress;
        private int TcpConnectClientPort;
        private EndPoint TcpConnectClientEndPoint;

        public bool TcpConnectStatus;

        private System.Net.Sockets.Socket TcpConnectServerSocket;
        private System.Net.Sockets.Socket TcpConnectClientSocket;

        //-------------------------------------------- TCP Property ---------------------------------------------

        //-------------------------------------------- UDP Property ---------------------------------------------

        private string UdpConnectRemoteIpAddressString;
        private IPAddress UdpConnectRemoteIpAddress;
        private int UdpConnectRemotePort;
        private EndPoint UdpConnectRemoteEndPoint;

        private string UdpConnectLocalIpAddressString;
        private IPAddress UdpConnectLocalIpAddress;
        private int UdpConnectLocalPort;
        private EndPoint UdpConnectLocalEndPoint;

        public bool UdpConnectStatus;

        private System.Net.Sockets.Socket UdpConnectRemoteSocket;
        private System.Net.Sockets.Socket UdpConnectLocalSocket;

        //-------------------------------------------- UDP Property ---------------------------------------------

        public BasicSocketOperation()
        {
        }

        //-------------------------------------------- TCP Functions --------------------------------------------

        /// <summary> 
        /// Function : Tcp Connection Set Server End Point
        /// </summary> 
        public FunctionResult TcpConnectSetServerEndPoint(string ipAddress, int port)
        {
            TcpConnectServerIpAddressString = ipAddress;
            TcpConnectServerPort = port;

            try
            {
                TcpConnectServerIpAddress = IPAddress.Parse(TcpConnectServerIpAddressString);
            }
            catch(ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch(FormatException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("FormatException", true);

                return FunctionResult.FormatException;
            }

            try
            {
                TcpConnectServerEndPoint = new IPEndPoint(TcpConnectServerIpAddress, TcpConnectServerPort);
            }
            catch(ArgumentOutOfRangeException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentOutOfRangeException", true);

                return FunctionResult.ArgumentOutOfRangeException;
            }

            return FunctionResult.Success;
        }

        /// <summary> 
        /// Function : Tcp Connection Set Client End Point
        /// </summary> 
        public FunctionResult TcpConnectSetClientEndPoint(string ipAddress, int port)
        {
            TcpConnectClientIpAddressString = ipAddress;
            TcpConnectClientPort = port;

            try
            {
                TcpConnectClientIpAddress = IPAddress.Parse(TcpConnectClientIpAddressString);
            }
            catch(ArgumentException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentException", true);

                return FunctionResult.ArgumentException;
            }
            catch(FormatException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("FormatException", true);

                return FunctionResult.FormatException;
            }

            try
            {
                TcpConnectClientEndPoint = new IPEndPoint(TcpConnectClientIpAddress, TcpConnectClientPort);
            }
            catch(ArgumentOutOfRangeException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentOutOfRangeException", true);

                return FunctionResult.ArgumentOutOfRangeException;
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// Function : Tcp Connect Set Server Up
        /// </summary>
        /// <returns></returns>
        public FunctionResult TcpConnectServerSetUp()
        {
            try
            {
                TcpConnectServerSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }

            try
            {
                TcpConnectServerSocket.Bind(TcpConnectServerEndPoint);
            }
            catch(ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch(SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }
            catch(ObjectDisposedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ObjectDisposedException", true);

                return FunctionResult.ObjectDisposedException;
            }
            catch(SecurityException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SecurityException", true);

                return FunctionResult.SecurityException;
            }

            TcpConnectServerSocket.Listen(10);
            System.Net.Sockets.Socket socket = TcpConnectServerSocket.Accept(); // 阻断程序

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public FunctionResult TcpConnectServerSetUp(string ipAddress, int port)
        {
            TcpConnectSetServerEndPoint(ipAddress, port);
            TcpConnectServerSetUp();

            return FunctionResult.Success;
        }

        /// <summary>
        /// Function : Tcp Connect Client To Server
        /// </summary>
        /// <returns></returns>
        public FunctionResult TcpConnectClientToServer()
        {
            try
            {
                TcpConnectClientSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch(SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }

            try
            {
                TcpConnectClientSocket.Connect(TcpConnectClientEndPoint);
            }
            catch(ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch(SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true); 

                return FunctionResult.Fail;
            }
            catch(ObjectDisposedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ObjectDisposedException", true);

                return FunctionResult.ObjectDisposedException;
            }
            catch(SecurityException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SecurityException", true);

                return FunctionResult.SecurityException;
            }
            catch(InvalidOperationException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("InvalidOperationException", true);

                return FunctionResult.InvalidOperationException;
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public FunctionResult TcpConnectClientToServer(string ipAddress, int port)
        {
            if(TcpConnectSetClientEndPoint(ipAddress, port) == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            if(TcpConnectClientToServer() == FunctionResult.Success)
            {
                
            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult TcpDisconnectClientToServer()
        {
            try
            {
                TcpConnectClientSocket.Disconnect(false); // un-reuseable socket
            }
            catch(PlatformNotSupportedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("PlatformNotSupportedException", true);

                return FunctionResult.PlatformNotSupportedException;
            }
            catch(ObjectDisposedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ObjectDisposedException", true);

                return FunctionResult.ObjectDisposedException;
            }
            catch(SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult TcpConnectClientSendMessage(byte[] message, uint messageLength)
        {
            try
            {
                TcpConnectClientSocket.Send(message, (int)messageLength, SocketFlags.None);
            }
            catch(ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch(SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }
            catch(ObjectDisposedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ObjectDisposedException", true);

                return FunctionResult.ObjectDisposedException;
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult TcpConnectClientReceiveMessage(ref byte[] message, ref uint messageLength)
        {
            try
            {
                // Receive() function is a blocking process.
                // the thread run this method will wait here, until next message received by hardware.
                messageLength = (uint)TcpConnectClientSocket.Receive(message);  // 阻断程序
            }
            catch(ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch(SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }
            catch(ObjectDisposedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ObjectDisposedException", true);

                return FunctionResult.ObjectDisposedException;
            }
            catch(SecurityException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SecurityException", true);

                return FunctionResult.SecurityException;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- TCP Functions --------------------------------------------

        //-------------------------------------------- UDP Functions --------------------------------------------

        /// <summary> 
        /// Function : Udp Connection Set Remote End Point
        /// </summary> 
        public FunctionResult UdpConnectSetRemoteEndPoint(string ipAddress, int port)
        {
            UdpConnectRemoteIpAddressString = ipAddress;
            UdpConnectRemotePort = port;

            UdpConnectRemoteIpAddress = IPAddress.Parse(UdpConnectRemoteIpAddressString);

            UdpConnectRemoteEndPoint = new IPEndPoint(UdpConnectRemoteIpAddress, UdpConnectRemotePort);

            return FunctionResult.Success;
        }

        /// <summary> 
        /// Function : Udp Connection Set Local End Point
        /// </summary> 
        public FunctionResult UdpConnectSetLocalEndPoint(string ipAddress, int port)
        {
            UdpConnectLocalIpAddressString = ipAddress;
            UdpConnectLocalPort = port;

            UdpConnectLocalIpAddress = IPAddress.Parse(UdpConnectLocalIpAddressString);

            UdpConnectLocalEndPoint = new IPEndPoint(UdpConnectLocalIpAddress, UdpConnectLocalPort);

            return FunctionResult.Success;
        }

        /// <summary> 
        /// Function :
        ///     Udp Local EndPoint Connect to Remote EndPoint 
        /// </summary> 
        public FunctionResult UdpConnectLocalToRemote()
        {
            UdpConnectRemoteSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            UdpConnectRemoteSocket.Bind(UdpConnectRemoteEndPoint);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public FunctionResult UdpConnectLocalToRemote(string ipAddress, int port)
        {
            UdpConnectSetRemoteEndPoint(ipAddress, port);
            UdpConnectLocalToRemote();

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult UdpDisconnectLocalToRemote()
        {
            UdpConnectRemoteSocket.Disconnect(true);

            return FunctionResult.Success;
        }

        /// <summary>  
        /// 向远端主机的端口发送数据报  
        /// </summary>  
        public FunctionResult UdpConnectLocalSendMessage(byte[] message, uint messageLength)
        {
            UdpConnectRemoteSocket.SendTo(message, UdpConnectRemoteEndPoint);

            return FunctionResult.Success;
        }

        /// <summary>  
        /// 接收发送给本机ip对应端口号的数据报  
        /// </summary>  
        public FunctionResult UdpConnectLocalReceiveMessage(ref byte[] message, ref uint messageLength)
        {
            messageLength = (uint)UdpConnectRemoteSocket.ReceiveFrom(message, ref UdpConnectRemoteEndPoint);

            return FunctionResult.Success;
        }

        //-------------------------------------------- UDP Functions --------------------------------------------
    }
}
