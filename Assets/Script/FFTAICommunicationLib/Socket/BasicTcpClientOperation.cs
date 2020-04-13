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
    public class BasicTcpClientOperation
    {
        //-------------------------------------------- TCP Property ---------------------------------------------

        private string TcpConnectServerIpAddressString;
        private IPAddress TcpConnectServerIpAddress;
        private int TcpConnectServerPort;
        private IPEndPoint TcpConnectServerEndPoint;
        
        public bool TcpConnectStatus;
        
        private TcpClient TcpClient;

        //-------------------------------------------- TCP Property ---------------------------------------------

        public BasicTcpClientOperation()
        {
            
        }

        //-------------------------------------------- TCP Functions --------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public FunctionResult TcpConnectClientToServer(string ipAddress, int port)
        {
            // build server ip end point
            TcpConnectServerIpAddressString = ipAddress;
            TcpConnectServerPort = port;

            try
            {
                TcpConnectServerIpAddress = IPAddress.Parse(TcpConnectServerIpAddressString);
            }
            catch (ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch (FormatException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("FormatException", true);

                return FunctionResult.FormatException;
            }

            try
            {
                TcpConnectServerEndPoint = new IPEndPoint(TcpConnectServerIpAddress, TcpConnectServerPort);
            }
            catch (ArgumentOutOfRangeException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentOutOfRangeException", true);

                return FunctionResult.ArgumentOutOfRangeException;
            }

            // build tcp model
            if (TcpClient == null)
            {
                TcpClient = new TcpClient();
            }

            // connect client to server
            if (TcpClient.Connected == true)
            {

            }
            else
            {
                TcpClient.Connect(TcpConnectServerEndPoint);
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult TcpDisconnectClientToServer()
        {
            // in case of TcpClient is null itself
            if (TcpClient == null)
            {
                return FunctionResult.Success;
            }

            if (TcpClient.Connected == true)
            {
                TcpClient.Close();
            }
            else
            {

            }

            TcpClient = null;
            
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
            // in case of TcpClient is null itself
            if (TcpClient == null)
            {
                return FunctionResult.Fail;
            }

            try
            {
                TcpClient.Client.Send(message, (int)messageLength, SocketFlags.None);
            }
            catch (ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch (SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }
            catch (ObjectDisposedException)
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
            // in case of TcpClient is null itself
            if (TcpClient == null)
            {
                return FunctionResult.Fail;
            }

            try
            {
                // Receive() function is a blocking process.
                // the thread run this method will wait here, until next message received by hardware.
                messageLength = (uint)TcpClient.Client.Receive(message);
            }
            catch (ArgumentNullException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ArgumentNullException", true);

                return FunctionResult.ArgumentNullException;
            }
            catch (SocketException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SocketException", true);

                return FunctionResult.SocketException;
            }
            catch (ObjectDisposedException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("ObjectDisposedException", true);

                return FunctionResult.ObjectDisposedException;
            }
            catch (SecurityException)
            {
                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("SecurityException", true);

                return FunctionResult.SecurityException;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- TCP Functions --------------------------------------------
        
    }
}
