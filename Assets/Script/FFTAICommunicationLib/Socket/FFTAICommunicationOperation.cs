using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationOperation
    {
        private enum ConnectStatus
        {
            DisConnected,
            Connected,
        }

        private BasicPingOperation BasicPingOperation;
        private BasicSocketOperation BasicSocketOperation;
        private BasicTcpClientOperation BasicTcpClientOperation;
        private BasicUdpClientOperation BasicUdpClientOperation;

        public const string DEFAULT_MMU_IP_ADDRESS = "192.168.1.30";
        public const int DEFAULT_MMU_PORT = 5000;
        public const NetworkConnectionType DEFAULT_NETWORK_CONNECTION_TYPE = NetworkConnectionType.TCP;

        private string MMUIpAddress;
        private int MMUPort;
        private NetworkConnectionType MMUConnectionType;
        private ConnectStatus MMUConnectStatus;

        private byte[] ReceiveMessageBuf = new byte[200];
        private uint ReceiveMessageBufLength = 0;

        private byte[] SendMessageBuf = new byte[200];
        private uint SendMessageBufLength = 0;

        //-------------------------------------------- Receive and Sned Message Handler Interface ---------------------------------------------

        private Thread FFTAICommunicationReceiveListenerThread;
		private static bool FFTAICommunicationReceiveListenerThreadActiveFlag;

        private Thread FFTAICommunicationSendListenerThread;
		private static bool FFTAICommunicationSendListenerThreadActiveFlag;

        //-------------------------------------------- Receive and Sned Message Handler Interface ---------------------------------------------

        //-------------------------------------------- Server Monitor -------------------------------------------------------------------------

        private Timer ServerMonitorTimer;

        //-------------------------------------------- Server Monitor -------------------------------------------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------------------------------------

        private List<IFFTAICommunicationOperationObserver> Observers;
        private List<IFFTAICommunicationOperationConnectionStatusObserver> ConnectionStatusObservers;

        //-------------------------------------------- Event Observer -------------------------------------------------------------------------

        /// <summary>
        /// FFTAICommunicationOperation Init
        /// </summary>
        public FFTAICommunicationOperation()
        {
            BasicPingOperation = new BasicPingOperation();
            BasicSocketOperation = new BasicSocketOperation();
            BasicTcpClientOperation = new BasicTcpClientOperation();
            BasicUdpClientOperation = new BasicUdpClientOperation();
            
            MMUConnectStatus = ConnectStatus.DisConnected;

            Observers = new List<IFFTAICommunicationOperationObserver>();
            ConnectionStatusObservers = new List<IFFTAICommunicationOperationConnectionStatusObserver>();
        }

        //-------------------------------------------- Connection Functions -------------------------------------------------------------------

        /// <summary>
        /// Connect to MMU
        /// </summary>
        /// <param name="mmuIpAddress"></param>
        /// <param name="mmuPort"></param>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        public FunctionResult Connect(
            string mmuIpAddress = DEFAULT_MMU_IP_ADDRESS, 
            int mmuPort = DEFAULT_MMU_PORT, 
            NetworkConnectionType connectionType = DEFAULT_NETWORK_CONNECTION_TYPE)
        {
            MMUIpAddress = mmuIpAddress;
            MMUPort = mmuPort;
            MMUConnectionType = connectionType;

            if (MMUConnectStatus == ConnectStatus.DisConnected)
            {
                switch (MMUConnectionType)
                {
                   
                    case NetworkConnectionType.TCP:
                        if (BasicTcpClientOperation.TcpConnectClientToServer(MMUIpAddress, MMUPort) == FunctionResult.Success)
                        {
                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }
                        break;
                    case NetworkConnectionType.UDP:
                        if (BasicUdpClientOperation.UdpConnectLocalToRemote(MMUIpAddress, MMUPort) == FunctionResult.Success)
                        {
                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }
                        break;
                    default:
                        return FunctionResult.Fail;
                }
                
                // Note : MMUConnectStatus change must before create FFTAICommunicationReceiveListenerThread !!!
                MMUConnectStatus = ConnectStatus.Connected;
                
                // build children thread to handler data receive listening
                if (FFTAICommunicationReceiveListenerThread == null)
                {

                }
                else
                {
                    FFTAICommunicationReceiveListenerThread = null;
                }

                FFTAICommunicationReceiveListenerThread = new Thread(ReceiveMessageListener);

				FFTAICommunicationReceiveListenerThreadActiveFlag = false;
                FFTAICommunicationReceiveListenerThread.Start();

                // sleep to let main thread give time to create children thread.
				while (FFTAICommunicationReceiveListenerThreadActiveFlag == false) {
					Thread.Sleep (10);
				}

                // log information
                FFTAICommunicationManager.Instance.Logger.WriteLine("FFTAICommunication 创建网络连接和监听子线程成功.", true);
            }
            else
            {
                ObserverNotifyConnect();

                return FunctionResult.NetworkAlreadyConnected;
            }

            ObserverNotifyConnect();

            return FunctionResult.Success;
        }

        /// <summary>
        /// Disconnect From MMU
        /// </summary>
        /// <returns></returns>
        public FunctionResult Disconnect()
        {
            FunctionResult functionResult;

            if (MMUConnectStatus == ConnectStatus.Connected)
            {
                switch (MMUConnectionType)
                {
                    case NetworkConnectionType.TCP:
                        functionResult = BasicTcpClientOperation.TcpDisconnectClientToServer();

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }
                        break;
                    case NetworkConnectionType.UDP:
                        functionResult = BasicUdpClientOperation.UdpDisconnectLocalToRemote();

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }
                        break;
                    default:
                        return FunctionResult.Fail;
                }

                // abort children thread handling data receive listening
                if (FFTAICommunicationReceiveListenerThread.ThreadState == ThreadState.Running)
                {
                    FFTAICommunicationReceiveListenerThread.Abort();

                    int maxWaitTime = 100;
                    int waitTime = 0;

                    while (FFTAICommunicationReceiveListenerThread.ThreadState != ThreadState.Aborted)
                    {
                        Thread.Sleep(10);

                        waitTime = waitTime + 10;
                        if (waitTime >= maxWaitTime)
                        {
                            break;
                        }
                    }

                    FFTAICommunicationReceiveListenerThread = null;
                }

                MMUConnectStatus = ConnectStatus.DisConnected;
            }
            else
            {
                ObserverNotifyDisconnect();

                return FunctionResult.NetworkAlreadyDisConnected;
            }

            ObserverNotifyDisconnect();
            
            return FunctionResult.Success;
        }

        /// <summary>
        /// Receive Message Listener
        /// </summary>
        public void ReceiveMessageListener()
        {
			// set active flag
			FFTAICommunicationReceiveListenerThreadActiveFlag = true;
			
            while (true)
            {
                FunctionResult functionResult = ReceiveMessage();

                if (functionResult == FunctionResult.Success)
                {
                    try
                    {
                        ReceiveMessageHandler();
                    }
                    catch(Exception exception)
                    {
                        FFTAICommunicationManager.Instance.Logger.WriteLine("ReceiveMessageHandler() catch an exception : " + exception);
                    }
                }
                else if (functionResult == FunctionResult.NoData)
                {
                    // zero data received, continue
                    continue;
                }
                else
                {
                    break;
                }
            }

			// clear active flag
			FFTAICommunicationReceiveListenerThreadActiveFlag = false;

            // abort the thread itself
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// Receive Message Handler
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult ReceiveMessageHandler()
        {
            ObserverNotifyReceiveMessage(ReceiveMessageBuf, ReceiveMessageBufLength);

            return FunctionResult.Success;
        }

        //-------------------------------------------- Connection Functions -------------------------------------------------------------------

        //-------------------------------------------- Data Operation Functions ---------------------------------------------------------------

        /// <summary>
        /// Receive Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult ReceiveMessage()
        {
            FunctionResult functionResult;

            if (MMUConnectStatus == ConnectStatus.Connected)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            switch (MMUConnectionType)
            {
                case NetworkConnectionType.TCP:
                    functionResult = BasicTcpClientOperation.TcpConnectClientReceiveMessage(ref ReceiveMessageBuf, ref ReceiveMessageBufLength);

                    if (functionResult == FunctionResult.Success)
                    {
                    }
                    else if (functionResult == FunctionResult.SocketException)
                    {
                        Disconnect();

                        return FunctionResult.Fail;
                    }
                    else
                    {
                        return FunctionResult.Fail;
                    }
                    break;
                case NetworkConnectionType.UDP:
                    functionResult = BasicUdpClientOperation.UdpConnectLocalReceiveMessage(ref ReceiveMessageBuf, ref ReceiveMessageBufLength);

                    if (functionResult == FunctionResult.Success)
                    {
                    }
                    else if (functionResult == FunctionResult.SocketException)
                    {
                        Disconnect();

                        return FunctionResult.Fail;
                    }
                    else
                    {
                        return FunctionResult.Fail;
                    }
                    break;
                default:
                    {
                        ObserverNotifyDisconnect();

                        return FunctionResult.Fail;
                    }
            }

            if (ReceiveMessageBufLength == 0)
            {
                return FunctionResult.NoData;
            }
            else
            {
                // only receive more than 0 byte. return success.
                return FunctionResult.Success;
            }
        }

        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult SendMessage(byte[] message, uint messageLength)
        {
            FunctionResult functionResult;

            if (MMUConnectStatus == ConnectStatus.Connected)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            switch (MMUConnectionType)
            {
                case NetworkConnectionType.TCP:
                    functionResult = BasicTcpClientOperation.TcpConnectClientSendMessage(message, messageLength);

                    if (functionResult == FunctionResult.Success)
                    {
                    }
                    else if (functionResult == FunctionResult.SocketException)
                    {
                        Disconnect();

                        return FunctionResult.SocketException;
                    }
                    else
                    {
                        return FunctionResult.Fail;
                    }
                    break;
                case NetworkConnectionType.UDP:
                    functionResult = BasicUdpClientOperation.UdpConnectLocalSendMessage(message, messageLength);
                    
                    if (functionResult == FunctionResult.Success)
                    {
                    }
                    else if (functionResult == FunctionResult.SocketException)
                    {
                        Disconnect();

                        return FunctionResult.SocketException;
                    }
                    else
                    {
                        return FunctionResult.Success;
                    }
                    break;
                default:
                    {
                        ObserverNotifyDisconnect();

                        return FunctionResult.Fail;
                    }
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Data Operation Functions ---------------------------------------------------------------

        //-------------------------------------------- Observer Notification Function ---------------------------------------------------------
        
        public FunctionResult AddObserver(IFFTAICommunicationOperationObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }
        
        public FunctionResult RemoveObserver(IFFTAICommunicationOperationObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                Observers.Remove(observer);
            }

            return FunctionResult.Success;
        }
        
        public FunctionResult ObserverNotifyReceiveMessage(byte[] message, uint messageLength)
        {
            for (int i = 0; i < Observers.Count; i++)
            {
                Observers[i].ReceiveMessageHandle(message, messageLength);
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Notification Function (Connection Status Observer) ----------------------------
        
        public FunctionResult AddObserver(IFFTAICommunicationOperationConnectionStatusObserver observer)
        {
            if (ConnectionStatusObservers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }
            
            ConnectionStatusObservers.Add(observer);

            return FunctionResult.Success;
        }
        
        public FunctionResult RemoveObserver(IFFTAICommunicationOperationConnectionStatusObserver observer)
        {
            if (ConnectionStatusObservers.Contains(observer) == true)
            {
                ConnectionStatusObservers.Remove(observer);
            }

            return FunctionResult.Success;
        }

        public FunctionResult ObserverNotifyConnect()
        {
            for (int i = 0; i < ConnectionStatusObservers.Count; i++)
            {
                ConnectionStatusObservers[i].ConnectedHandle();
            }

            return FunctionResult.Success;
        }

        public FunctionResult ObserverNotifyDisconnect()
        {
            for (int i = 0; i < ConnectionStatusObservers.Count; i++)
            {
                ConnectionStatusObservers[i].DisconnectedHandle();
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Notification Function (Connection Status Observer) ----------------------------

        //-------------------------------------------- Observer Notification Function ---------------------------------------------------------

    }
}
