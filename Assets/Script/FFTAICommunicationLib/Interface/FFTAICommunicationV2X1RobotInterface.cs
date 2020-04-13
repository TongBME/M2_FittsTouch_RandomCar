using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2X1RobotInterface
    {

        //-------------------------------------------- Model Definition -----------------------------------------------------------------------------

        private FFTAICommunicationV2X1RobotInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------------------------------------------

        //-------------------------------------------- Variables Definition -------------------------------------------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition -------------------------------------------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------------------------------------------

        public List<IFFTAICommunicationV2X1RobotInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------------------------------------------


        //-------------------------------------------- Function Definition --------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------------------------------------------

        public FFTAICommunicationV2X1RobotInterface()
        {
            Model = new FFTAICommunicationV2X1RobotInterfaceModel();

            Observers = new List<IFFTAICommunicationV2X1RobotInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (Receive) ----------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult Receive(byte[] buffer, uint bufferLength)
        {
            if (Model.DataSectionModel.ResponseModel.Update(buffer, bufferLength) == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            if (Update() == FunctionResult.Success)
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
        public FunctionResult Update()
        {
            ObserverNotifyModelUpdate();

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Receive) ----------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request) ----------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Sensor) -------------------------------------------------------

        //--------------------------------------------- Function Definition (Request - Sensor - Button) ---------------------------------------------

        public FunctionResult RequestReadRedButtonValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.RedButtonValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadYellowButtonValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.YellowButtonValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadGreenButtonValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.GreenButtonValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadBlueButtonValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.BlueButtonValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadButtonSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.ButtonSensorInformation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //--------------------------------------------- Function Definition (Request - Sensor - Button) ---------------------------------------------

        //-------------------------------------------- Function Definition (Request - Sensor - Force Sensor) ----------------------------------------

        public FunctionResult RequestReadLeftLegThighForceSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.LeftLegThighForceSensorValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadLeftLegCalfForceSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.LeftLegCalfForceSensorValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadRightLegThighForceSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.RightLegThighForceSensorValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadRightLegCalfForceSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.RightLegCalfForceSensorValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadForceSensorInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.ForceSensorInformation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Sensor - Force Sensor) ----------------------------------------

        //-------------------------------------------- Function Definition (Request - Sensor - Foot Pressure Sensor) --------------------------------

        public FunctionResult RequestReadLeftLegFootPressureSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.LeftLegFootPressureSensorValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadRightLegFootPressureSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.RightLegFootPressureSensorValue,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadFootPressureSensorInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.FootPressureInformation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Sensor - Foot Pressure Sensor) --------------------------------

        //-------------------------------------------- Function Definition (Request - Basic Motor Control) ------------------------------------------

        public FunctionResult RequestReadModeOfOperation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.ModeOfOperation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteModeOfOperation(uint modeOfOperation)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.ModeOfOperation,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { modeOfOperation, modeOfOperation, modeOfOperation, modeOfOperation });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadControlWord()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.ControlWord,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteControlWord(uint controlWord)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.ControlWord,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { controlWord, controlWord, controlWord, controlWord });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadStatusWord()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.StatusWord,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteFaultClear()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.FaultClear,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteMotorEnable()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.MotorEnable,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteMotorDisable()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.MotorDisable,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Basic Motor Configuration) ------------------------------------

        //-------------------------------------------- Function Definition (Request - Motor Information) --------------------------------------------

        public FunctionResult RequestReadMotorCurrent()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.MotorCurrent,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadMotorInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.MotorInformation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Motor Information) --------------------------------------------

        //-------------------------------------------- Function Definition (Request - Joint Information) --------------------------------------------

        public FunctionResult RequestReadJointTorque()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointVelocity()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointPosition()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointInformation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Joint Information) --------------------------------------------

        //-------------------------------------------- Function Definition (Request - End Effector Information) -------------------------------------

        public FunctionResult RequestReadEndEffectorTorque()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorVelocity()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorPosition()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorInformation,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - End Effector Information) -------------------------------------

        //-------------------------------------------- Function Definition (Request - Protection) ---------------------------------------------------

        public FunctionResult RequestSetJointLimitKinetic(
            float horizontalJointLimitMinKinetic, float horizontalJointLimitMaxKinetic,
            float verticalJointLimitMinKinetic, float verticalJointLimitMaxKinetic)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxKinetic), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetJointLimitAcceleration(
            float horizontalJointLimitMinAcceleration, float horizontalJointLimitMaxAcceleration,
            float verticalJointLimitMinAcceleration, float verticalJointLimitMaxAcceleration)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinAcceleration), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxAcceleration), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinAcceleration), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxAcceleration), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetJointLimitVelocity(
            float horizontalJointLimitMinVelocity, float horizontalJointLimitMaxVelocity,
            float verticalJointLimitMinVelocity, float verticalJointLimitMaxVelocity)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinVelocity), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxVelocity), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinVelocity), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxVelocity), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetJointLimitPosition(
            float horizontalJointLimitMinPosition, float horizontalJointLimitMaxPosition,
            float verticalJointLimitMinPosition, float verticalJointLimitMaxPosition)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxPosition), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsJointLimitKinetic(
            uint horizontalJointLimitMinSetFlag, uint horizontalJointLimitMaxSetFlag,
            uint verticalJointLimitMinSetFlag, uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsJointLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsJointLimitAcceleration(
            uint horizontalJointLimitMinSetFlag, uint horizontalJointLimitMaxSetFlag,
            uint verticalJointLimitMinSetFlag, uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsJointLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsJointLimitVelocity(
            uint horizontalJointLimitMinSetFlag, uint horizontalJointLimitMaxSetFlag,
            uint verticalJointLimitMinSetFlag, uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsJointLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsJointLimitPosition(
            uint horizontalJointLimitMinSetFlag, uint horizontalJointLimitMaxSetFlag,
            uint verticalJointLimitMinSetFlag, uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsJointLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetEndEffectorLimitKinetic(
            float horizontalEndEffectorLimitMinKinetic, float horizontalEndEffectorLimitMaxKinetic,
            float verticalEndEffectorLimitMinKinetic, float verticalEndEffectorLimitMaxKinetic)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxKinetic), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetEndEffectorLimitAcceleration(
            float horizontalEndEffectorLimitMinAcceleration, float horizontalEndEffectorLimitMaxAcceleration,
            float verticalEndEffectorLimitMinAcceleration, float verticalEndEffectorLimitMaxAcceleration)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinAcceleration), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxAcceleration), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinAcceleration), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxAcceleration), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetEndEffectorLimitVelocity(
            float horizontalEndEffectorLimitMinVelocity, float horizontalEndEffectorLimitMaxVelocity,
            float verticalEndEffectorLimitMinVelocity, float verticalEndEffectorLimitMaxVelocity)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinVelocity), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxVelocity), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinVelocity), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxVelocity), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetEndEffectorLimitPosition(
            float horizontalEndEffectorLimitMinPosition, float horizontalEndEffectorLimitMaxPosition,
            float verticalEndEffectorLimitMinPosition, float verticalEndEffectorLimitMaxPosition)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxPosition), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsEndEffectorLimitKinetic(
            uint horizontalEndEffectorLimitMinSetFlag, uint horizontalEndEffectorLimitMaxSetFlag,
            uint verticalEndEffectorLimitMinSetFlag, uint verticalEndEffectorLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsEndEffectorLimitAcceleration(
            uint horizontalEndEffectorLimitMinSetFlag, uint horizontalEndEffectorLimitMaxSetFlag,
            uint verticalEndEffectorLimitMinSetFlag, uint verticalEndEffectorLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsEndEffectorLimitVelocity(
            uint horizontalEndEffectorLimitMinSetFlag, uint horizontalEndEffectorLimitMaxSetFlag,
            uint verticalEndEffectorLimitMinSetFlag, uint verticalEndEffectorLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetCurrentValueAsEndEffectorLimitPosition(
            uint horizontalEndEffectorLimitMinSetFlag, uint horizontalEndEffectorLimitMaxSetFlag,
            uint verticalEndEffectorLimitMinSetFlag, uint verticalEndEffectorLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(horizontalEndEffectorLimitMaxSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMinSetFlag), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(verticalEndEffectorLimitMaxSetFlag), 0), });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Protection) ---------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Home Control) -------------------------------------------------

        public FunctionResult RequestWriteHomeControlZeroHomePosition()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.HomeControlZeroHomePosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Home Control) -------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Joint Torque Control) -----------------------------------------

        public FunctionResult RequestReadJointTorqueControlTorque(
            float torque1, float torque2, float torque3, float torque4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointTorqueControlTorque(
            float torque1, float torque2, float torque3, float torque4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Joint Torque Control) -----------------------------------------

        //-------------------------------------------- Function Definition (Request - Joint Velocity Control) ---------------------------------------

        public FunctionResult RequestReadJointVelocityControlAcceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocityControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointVelocityControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocityControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointVelocityControlDeceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocityControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointVelocityControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocityControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointVelocityControlVelocity()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocityControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointVelocityControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointVelocityControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Joint Velocity Control) ---------------------------------------

        //-------------------------------------------- Function Definition (Request - Joint Position Control) ---------------------------------------

        public FunctionResult RequestReadJointPositionControlAcceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointPositionControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointPositionControlDeceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointPositionControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointPositionControlVelocity()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointPositionControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadJointPositionControlPosition()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteJointPositionControlPosition(
            float position1, float position2, float position3, float position4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.JointPositionControlPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Joint Position Control) ---------------------------------------

        //-------------------------------------------- Function Definition (Request - End Effector Torque Control) ----------------------------------

        public FunctionResult RequestReadEndEffectorTorqueControlTorque(
            float torque1, float torque2, float torque3, float torque4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorTorqueControlTorque(
            float torque1, float torque2, float torque3, float torque4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(torque4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - End Effector Torque Control) ----------------------------------

        //-------------------------------------------- Function Definition (Request - End Effector Velocity Control) --------------------------------

        public FunctionResult RequestReadEndEffectorVelocityControlAcceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocityControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorVelocityControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocityControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorVelocityControlDeceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocityControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorVelocityControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocityControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorVelocityControlVelocity()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocityControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorVelocityControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorVelocityControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - End Effector Velocity Control) --------------------------------

        //-------------------------------------------- Function Definition (Request - End Effector Position Control) --------------------------------

        public FunctionResult RequestReadEndEffectorPositionControlAcceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorPositionControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorPositionControlDeceleration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorPositionControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(deceleration4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorPositionControlVelocity()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorPositionControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestReadEndEffectorPositionControlPosition()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Read,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestWriteEndEffectorPositionControlPosition(
            float position1, float position2, float position3, float position4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X1RobotInterfaceOperationMode.EndEffectorPositionControlPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position1), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position2), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position3), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(position4), 0)});

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // build request frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.X1,
                                (uint)FFTAICommunicationV2FunctionType.X1RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - End Effector Position Control) --------------------------------

        //-------------------------------------------- Function Definition (Request) ----------------------------------------------------------------

        //-------------------------------------------- Function Definition (Observer Notification) --------------------------------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2X1RobotInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2X1RobotInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                Observers.Remove(observer);
            }

            return FunctionResult.Success;
        }

        public FunctionResult ObserverNotifyModelUpdate()
        {
            for (int i = 0; i < Observers.Count; i++)
            {
                Observers[i].ModelUpdateHandle(Model);
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Observer Notification) --------------------------------------------------

        //-------------------------------------------- Function Definition --------------------------------------------------------------------------
    }
}
