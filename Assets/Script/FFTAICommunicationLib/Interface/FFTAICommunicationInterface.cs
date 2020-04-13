using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{

    public class FFTAICommunicationInterface : IFFTAICommunicationOperationObserver
    {
        //-------------------------------------------- Model Definition ----------------------------------------------------

        public FFTAICommunicationInterfaceModel Model;

        //-------------------------------------------- Model Definition ----------------------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Variables Definition (to MMU) ---------------------------------------

        public FFTAICommunicationOperation FFTAICommunicationOperation;

        //-------------------------------------------- Variables Definition (to MMU) ---------------------------------------

        //-------------------------------------------- Variables Definition (to Laptop) ------------------------------------

        public FFTAICommunicationV1Interface FFTAICommunicationV1Interface;
        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition (to Laptop) ------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------

        public FFTAICommunicationInterface()
        {
            Model = new FFTAICommunicationInterfaceModel();

            Model.FFTAICommunicationProtocolVersion = FFTAICommunicationProtocolVersion.Version2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult ReceiveFrame(byte[] buffer, uint bufferLength)
        {
            FunctionResult functionResult;

            // update model
            UpdateResponseMessage(buffer, bufferLength);

            // receive message
            switch (Model.FFTAICommunicationProtocolVersion)
            {
                case FFTAICommunicationProtocolVersion.Version1:
                    functionResult = FFTAICommunicationV1Interface.Receive(Model.ReceiveMessageBuf, Model.ReceiveMessageBufLength);

                    if (functionResult == FunctionResult.Success)
                    {

                    }
                    else
                    {
                        return FunctionResult.Fail;
                    }

                    break;
                case FFTAICommunicationProtocolVersion.Version2:
                    functionResult = FFTAICommunicationV2Interface.Receive(Model.ReceiveMessageBuf, Model.ReceiveMessageBufLength);

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

            return FunctionResult.Success;
        }

        /// <summary>
        /// Send Frame
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="frameLength"></param>
        /// <returns></returns>
        public FunctionResult SendFrame(byte[] buffer, uint bufferLength)
        {
            FunctionResult functionResult;

            // print the message
            byte[] _buffer = new byte[bufferLength];
            Array.Copy(buffer, _buffer, bufferLength);

            // debug log
            if (FFTAICommunicationConfig.DEBUG_LOG_SEND_MESSAGE)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Send Message : " + BitConverter.ToString(_buffer), true);
            }

            // send frame
            functionResult = FFTAICommunicationOperation.SendMessage(buffer, bufferLength);

            if (functionResult == FunctionResult.Success)
            {

            }
            else if (functionResult == FunctionResult.SocketException)
            {
                return FunctionResult.SocketException;
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
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult UpdateResponseMessage(byte[] buffer, uint bufferLength)
        {
            if (buffer == null 
                || bufferLength == 0)
            {
                Array.Clear(Model.ReceiveMessageBuf, 0, Model.ReceiveMessageBuf.Length);
                Model.ReceiveMessageBufLength = 0;

                return FunctionResult.Success;
            }

            // update model
            Array.Copy(
                buffer,
                0,
                Model.ReceiveMessageBuf,
                0,
                bufferLength);

            Model.ReceiveMessageBufLength = bufferLength;

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (IFFTAICommunicationOperationObserver) ----------

        public FunctionResult ReceiveMessageHandle(byte[] message, uint messageLength)
        {
            FunctionResult functionResult;

            // copy buffer
            byte[] _message = new byte[messageLength];
            uint _messageLength = messageLength;
            Array.Copy(message, _message, _messageLength);

            // debug log
            if (FFTAICommunicationConfig.DEBUG_LOG_RECEIVE_MESSAGE)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Receive Message : " + BitConverter.ToString(_message), true);
            }

            UpdateResponseMessage(_message, _messageLength);

            // receive message handle
            while (ReceiveFrame(Model.ReceiveMessageBuf, Model.ReceiveMessageBufLength) == FunctionResult.Success)
            {
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (IFFTAICommunicationOperationObserver) ----------

        //-------------------------------------------- Function Definition -------------------------------------------------

    }

}
