using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2Interface
    {

        //-------------------------------------------- Model Definition ----------------------------------------------------

        public FFTAICommunicationV2InterfaceModel Model;

        //-------------------------------------------- Model Definition ----------------------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Variables Definition (to MMU) ---------------------------------------

        public FFTAICommunicationInterface FFTAICommunicationInterface;

        //-------------------------------------------- Variables Definition (to MMU) ---------------------------------------

        //-------------------------------------------- Variables Definition (to Laptop) ------------------------------------

        public FFTAICommunicationV2CommunicationFrameType CommunicationFrameType;

        public FFTAICommunicationV2IAPInterface IAPInterface;

        public FFTAICommunicationV2SystemInterface SystemInterface;
        public FFTAICommunicationV2CommunicationInterface CommunicationInterface;
        public FFTAICommunicationV2HardwareInterface HardwareInterface;
        public FFTAICommunicationV2DriverInterface DriverInterface;
        public FFTAICommunicationV2MotorInterface MotorInterface;
        public FFTAICommunicationV2RobotInterface RobotInterface;

        public FFTAICommunicationV2M1RobotInterface M1RobotInterface;
        public FFTAICommunicationV2M2RobotInterface M2RobotInterface;
        public FFTAICommunicationV2X1RobotInterface X1RobotInterface;
        public FFTAICommunicationV2X2RobotInterface X2RobotInterface;

        public FFTAICommunicationV2M1TaskInterface M1TaskInterface;
        public FFTAICommunicationV2M2TaskInterface M2TaskInterface;
        public FFTAICommunicationV2X1TaskInterface X1TaskInterface;
        public FFTAICommunicationV2X2TaskInterface X2TaskInterface;

        //-------------------------------------------- Variables Definition (to Laptop) ------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) --------------------------------

        public FFTAICommunicationV2Interface()
        {
            Model = new FFTAICommunicationV2InterfaceModel();
        }

        //-------------------------------------------- Function Definition (Initialization) --------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult Parse()
        {
            uint i = 0;
            uint temp = 0;
            uint frameStartIndex = 0;
            uint frameLength = 0;
            byte dataSectionParity = 0;
            byte dataSectionParityCalculated = 0;
            byte endOfFrame = 0;

        _parseStart: // Note : label cannot be used before variables definition

            // is the buffer has enough the minimum requested length ?
            if (Model.ResponseModel.MessageBufLength - frameStartIndex
                < FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_PARITY_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_END_OF_FRAME_BUF)
            {
                return FunctionResult.Fail;
            }

            // is the buffer has the (0xA0 0xA1) frame header ?
            for (;
                i < Model.ResponseModel.MessageBufLength - 1;
                i++)
            {
                if (Model.ResponseModel.MessageBuf[i] == (byte)(FFTAICommunicationV2InterfaceResponseModel.HEADER_OF_RESPONSE_FRAME >> 8 & 0xFF)
                    && Model.ResponseModel.MessageBuf[i + 1] == (byte)(FFTAICommunicationV2InterfaceResponseModel.HEADER_OF_RESPONSE_FRAME >> 0 & 0xFF))
                {
                    temp = 1;
                    frameStartIndex = i;

                    break;
                }

                temp = 0;
            }

            if (temp == 0)
            {
                // no frame header
                return FunctionResult.Fail;
            }

            // 2. the minimum data frame length request
            if (frameStartIndex
                + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_PARITY_BUF
                + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_END_OF_FRAME_BUF
                < Model.ResponseModel.MessageBufLength)
            {
                Model.ResponseModel.DataSectionLength =
                    Model.ResponseModel.MessageBuf[frameStartIndex
                        + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                        + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF];
            }
            else
            {
                // not enough data
                i = frameStartIndex + 1;
                temp = 0;

                goto _parseStart;
            }

            // 3. is the buffer has the whole frame ?
            frameLength =
                FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                    + Model.ResponseModel.DataSectionLength
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_PARITY_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_END_OF_FRAME_BUF;

            if (frameStartIndex + frameLength <= Model.ResponseModel.MessageBufLength)
            {
            }
            else
            {
                // not enough data
                i = frameStartIndex + 1;
                temp = 0;

                goto _parseStart;
            }

            // 4. data section parity check
            dataSectionParity =
                Model.ResponseModel.MessageBuf[
                    frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                    + Model.ResponseModel.DataSectionLength];

            dataSectionParityCalculated = 0;

            for (int j = 0; j < Model.ResponseModel.DataSectionLength; j++)
            {
                dataSectionParityCalculated =
                    (byte)(
                        dataSectionParityCalculated
                        + Model.ResponseModel.MessageBuf[
                            frameStartIndex
                            + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                            + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                            + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                            + j]);
            }

            if (dataSectionParity == dataSectionParityCalculated)
            {

            }
            else
            {
                // not match parity check
                i = frameStartIndex + 1;
                temp = 0;

                goto _parseStart;
            }

            // 5. end of frame check
            endOfFrame =
                Model.ResponseModel.MessageBuf[
                    frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                    + Model.ResponseModel.DataSectionLength
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_PARITY_BUF];

            if (endOfFrame == FFTAICommunicationV2InterfaceResponseModel.END_OF_RESPONSE_FRAME)
            {

            }
            else
            {
                // not match end of frame
                i = frameStartIndex + 1;
                temp = 0;

                goto _parseStart;
            }
            
            // get function type
            Array.Copy(
                Model.ResponseModel.MessageBuf,
                (int)(frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF),
                Model.ResponseModel.FunctionTypeBuf,
                0,
                (int)(FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF));

            Model.ResponseModel.FunctionType =
                ((uint)Model.ResponseModel.FunctionTypeBuf[0] << 24)
                + ((uint)Model.ResponseModel.FunctionTypeBuf[1] << 16)
                + ((uint)Model.ResponseModel.FunctionTypeBuf[2] << 8)
                + ((uint)Model.ResponseModel.FunctionTypeBuf[3] << 0);

            // get data section length buffer
            Array.Copy(
                Model.ResponseModel.MessageBuf,
                (int)(frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF),
                 Model.ResponseModel.DataSectionLengthBuf,
                 0,
                 (int)FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF);

            // get data section
            Array.Copy(
                Model.ResponseModel.MessageBuf,
                (int)(frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF),
                Model.ResponseModel.DataSectionBuf,
                0,
                (int)Model.ResponseModel.DataSectionLength);

            // get data section parity
            Array.Copy(
                Model.ResponseModel.MessageBuf,
                (int)(frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                    + Model.ResponseModel.DataSectionLength),
                Model.ResponseModel.DataSectionParityBuf,
                0,
                (int)(FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_PARITY_BUF));

            Model.ResponseModel.DataSectionParity =
                ((uint)Model.ResponseModel.DataSectionParityBuf[0]);

            // get end of the frame
            Array.Copy(
                Model.ResponseModel.MessageBuf,
                (int)(frameStartIndex
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FRAME_HEADER_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_FUNCTION_TYPE_BUF
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_LENGTH_BUF
                    + Model.ResponseModel.DataSectionLength
                    + FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_DATA_SECTION_PARITY_BUF),
                Model.ResponseModel.EndOfFrameBuf,
                0,
                (int)(FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_END_OF_FRAME_BUF));

            Model.ResponseModel.EndOfFrame =
                (uint)Model.ResponseModel.EndOfFrameBuf[0];

            // copy frame buffer
            Model.ResponseModel.FrameBuf = new byte[frameLength];
            Model.ResponseModel.FrameBufLength = frameLength;

            Array.Copy(
                Model.ResponseModel.MessageBuf, 
                frameStartIndex, 
                Model.ResponseModel.FrameBuf,
                0, 
                Model.ResponseModel.FrameBufLength);

            // debug log
            if (FFTAICommunicationConfig.DEBUG_LOG_SUCCESS_PARSE_MESSAGE)
            {
                FFTAICommunicationManager.Instance.Logger.WriteLine("Success Parse Frame : " + BitConverter.ToString(Model.ResponseModel.FrameBuf));
            }

            // remove the data ahead of this frame and the data of this frame in the buffer
            Array.Clear(
                Model.ResponseModel.MessageBuf,
                0,
                (int)(frameStartIndex + frameLength));

            Array.Copy(
                Model.ResponseModel.MessageBuf,
                (int)(frameStartIndex + frameLength),
                Model.ResponseModel.MessageBuf,
                0,
                (int)(Model.ResponseModel.MessageBufLength - (frameStartIndex + frameLength)));

            Model.ResponseModel.MessageBufLength 
                = (uint)(Model.ResponseModel.MessageBufLength - (frameStartIndex + frameLength));

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult Receive(byte[] buffer, uint bufferLength)
        {
            FunctionResult functionResult;

            // check message length
            if (bufferLength > 0)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // copy message to local buff
            if (bufferLength 
                > (FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_MESSAGE_BUF - Model.ResponseModel.MessageBufLength))
            {
                Array.Copy(
                    buffer,
                    0,
                    Model.ResponseModel.MessageBuf,
                    Model.ResponseModel.MessageBufLength,
                    FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_MESSAGE_BUF - Model.ResponseModel.MessageBufLength);

                Model.ResponseModel.MessageBufLength = FFTAICommunicationV2InterfaceResponseModel.LENGTH_OF_MESSAGE_BUF;
            }
            else
            {
                Array.Copy(
                    buffer,
                    0,
                    Model.ResponseModel.MessageBuf,
                    Model.ResponseModel.MessageBufLength,
                    bufferLength);

                Model.ResponseModel.MessageBufLength = bufferLength;
            }

            // try to parse the message
            if ((functionResult = Parse()) == FunctionResult.Success)
            {
                // update request message and mesasgeLength
                FFTAICommunicationInterface.UpdateResponseMessage(
                    Model.ResponseModel.MessageBuf,
                    Model.ResponseModel.MessageBufLength);

                // find which robot platform it request

                // find which function it request
                switch ((FFTAICommunicationV2FunctionType)Model.ResponseModel.FunctionType)
                {
                    // IAP
					case FFTAICommunicationV2FunctionType.IAPInterface:
						functionResult = IAPInterface.Receive (Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    // communication
                    case FFTAICommunicationV2FunctionType.CommunicationInterface:
                        functionResult = CommunicationInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    // authentication
                    case FFTAICommunicationV2FunctionType.SystemInterface:
                        functionResult = SystemInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    // hardware layer
                    case FFTAICommunicationV2FunctionType.HardwareInterface:
                        functionResult = HardwareInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    // driver layer
                    case FFTAICommunicationV2FunctionType.DriverInterface:
                        break;
                    // motor layer
                    case FFTAICommunicationV2FunctionType.MotorInterface:
                        functionResult = MotorInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    // robot layer
                    case FFTAICommunicationV2FunctionType.M1RobotInterface:
                        functionResult = M1RobotInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    case FFTAICommunicationV2FunctionType.M2RobotInterface:
                        functionResult = M2RobotInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    case FFTAICommunicationV2FunctionType.X1RobotInterface:
                        functionResult = X1RobotInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    case FFTAICommunicationV2FunctionType.X2RobotInterface:
                        functionResult = X2RobotInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    // task layer
                    case FFTAICommunicationV2FunctionType.M1TaskInterface:
                        functionResult = M1TaskInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    case FFTAICommunicationV2FunctionType.M2TaskInterface:
                        functionResult = M2TaskInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    case FFTAICommunicationV2FunctionType.X1TaskInterface:
                        functionResult = X1TaskInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    case FFTAICommunicationV2FunctionType.X2TaskInterface:
                        functionResult = X2TaskInterface.Receive(Model.ResponseModel.DataSectionBuf, Model.ResponseModel.DataSectionLength);

                        if (functionResult == FunctionResult.Success)
                        {

                        }
                        else
                        {
                            return FunctionResult.Fail;
                        }

                        break;
                    default:
                        break;
                }
            }
            else
            {
                // reset receive message and mesasgeLength
                FFTAICommunicationInterface.UpdateResponseMessage(null, 0);

                // clear response buffer
                Array.Clear(Model.ResponseModel.MessageBuf, 0, Model.ResponseModel.MessageBuf.Length);
                Model.ResponseModel.MessageBufLength = 0;
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult Send(
            byte[] buffer, 
            uint bufferLength)
        {
            FunctionResult functionResult;

            // check if it is a request message and parse it
            functionResult = Model.RequestModel.Update(buffer, bufferLength);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // send the frame
            byte[] frame = Model.RequestModel.MessageBuf;
            uint frameLength = Model.RequestModel.MessageBufLength;

            functionResult = FFTAICommunicationInterface.SendFrame(frame, frameLength);

            if (functionResult == FunctionResult.Success)
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
        /// <param name="dataSectionLength"></param>
        /// <param name="dataSection"></param>
        /// <param name="dataSectionParity"></param>
        /// <returns></returns>
        public FunctionResult Send(
            uint dataSectionLength,
            byte[] dataSection,
            uint dataSectionParity)
        {
            FunctionResult functionResult;

            // check if it is a request message and parse it
            functionResult = Model.RequestModel.Update(
                                (uint)FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)Model.ProtocolVersion,
                                (uint)Model.RobotType,
                                (uint)Model.FunctionType,
                                dataSectionLength,
                                dataSection,
                                dataSectionParity,
                                (uint)FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // send the frame
            byte[] frame = Model.RequestModel.MessageBuf;
            uint frameLength = Model.RequestModel.MessageBufLength;

            functionResult = FFTAICommunicationInterface.SendFrame(frame, frameLength);

            if (functionResult == FunctionResult.Success)
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
        /// <param name="frameHeader"></param>
        /// <param name="communicationProtocolVersion"></param>
        /// <param name="robotType"></param>
        /// <param name="functionType"></param>
        /// <param name="dataSectionLength"></param>
        /// <param name="dataSection"></param>
        /// <param name="dataSectionParity"></param>
        /// <param name="endOfFrame"></param>
        /// <returns></returns>
        public FunctionResult Send(
            uint frameHeader,
            uint communicationProtocolVersion,
            uint robotType,
            uint functionType,
            uint dataSectionLength,
            byte[] dataSection,
            uint dataSectionParity,
            uint endOfFrame)
        {
            FunctionResult functionResult;

            uint dataSectionParityCalculated = 0;

            // calculate parity
            for (int i = 0; i < dataSectionLength; i++)
            {
                dataSectionParityCalculated =
                    (byte)(dataSectionParityCalculated + dataSection[i]);
            }

            // check if it is a request message and parse it
            functionResult = Model.RequestModel.Update(
                                frameHeader,
                                communicationProtocolVersion,
                                robotType,
                                functionType,
                                dataSectionLength,
                                dataSection,
                                dataSectionParityCalculated,
                                endOfFrame);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // send the frame
            byte[] frame = Model.RequestModel.MessageBuf;
            uint frameLength = Model.RequestModel.MessageBufLength;

            functionResult = FFTAICommunicationInterface.SendFrame(frame, frameLength);

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

        //-------------------------------------------- Function Definition -------------------------------------------------
    }
}
