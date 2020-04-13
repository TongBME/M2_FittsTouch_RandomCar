using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2M2RobotInterface
    {

        //-------------------------------------------- Model Definition -----------------------------------------------------------------------------

        private FFTAICommunicationV2M2RobotInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------------------------------------------

        //-------------------------------------------- Variables Definition -------------------------------------------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition -------------------------------------------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------------------------------------------

        public List<IFFTAICommunicationV2M2RobotInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------------------------------------------


        //-------------------------------------------- Function Definition --------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------------------------------------------

        public FFTAICommunicationV2M2RobotInterface()
        {
            Model = new FFTAICommunicationV2M2RobotInterfaceModel();

            Observers = new List<IFFTAICommunicationV2M2RobotInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (Receive) ----------------------------------------------------------------

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

        public FunctionResult Update()
        {
            ObserverNotifyModelUpdate();

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Receive) ----------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request) ----------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Flag) ---------------------------------------------------------

        public FunctionResult RequestReadFlagCalibration()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagCalibration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadFlagServoOn()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagServoOn,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadFlagEmergentStop()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagEmergentStop,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadFlagFault()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagFault,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadFlagInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagInformation,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadFlagOutOfJointLimitInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagOutOfJointLimitInformation,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadFlagOutOfEndEffectorLimitInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FlagOutOfEndEffectorLimitInformation,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        //-------------------------------------------- Function Definition (Request - Flag) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Sensor - Button) ----------------------------------------------

        public FunctionResult RequestReadEmergentStopButtonValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EmergentStopButton,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadButtonSensorInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ButtonSensorInformation,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadButtonSensorInstalled()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ButtonSensorInstalled,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadButtonSensorAccessible()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ButtonSensorAccessible,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetButtonSensorCalibrate()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ButtonSensorCalibrate,
                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        //-------------------------------------------- Function Definition (Request - Sensor - Button) ----------------------------------------------

        //-------------------------------------------- Function Definition (Request - Sensor - Force Sensor) ----------------------------------------

        public FunctionResult RequestReadHorizontalForceSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.HorizontalForceSensorValue,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
        
        public FunctionResult RequestReadVerticalForceSensorValue()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.VerticalForceSensorValue,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ForceSensorInformation,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadForceSensorInstalled()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ForceSensorInstalled,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadForceSensorAccessible()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ForceSensorAccessible,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetForceSensorCalibrate()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ForceSensorCalibrate,
                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        //-------------------------------------------- Function Definition (Request - Driver) -------------------------------------------------------

        public FunctionResult RequestReadDriverInformation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.DriverInformation,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadDriverInstalled()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.DriverInstalled,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestReadDriverAccessible()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.DriverAccessible,
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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        //-------------------------------------------- Function Definition (Request - Driver) -------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Basic Motor Configuration) ------------------------------------

        public FunctionResult RequestReadModeOfOperation()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ModeOfOperation,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetModeOfOperation(uint modeOfOperation)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ModeOfOperation,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ControlWord,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetControlWord(uint controlWord)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.ControlWord,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.StatusWord,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetFaultClear()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.FaultClear,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetMotorEnable()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.MotorEnable,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetMotorDisable()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.MotorDisable,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.MotorCurrent,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.MotorInformation,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointTorque,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPosition,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointInformation,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorTorque,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPosition,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorInformation,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            float horizontalJointLimitMinKinetic, 
            float horizontalJointLimitMaxKinetic, 
            float verticalJointLimitMinKinetic, 
            float verticalJointLimitMaxKinetic)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            float horizontalJointLimitMinAcceleration,
            float horizontalJointLimitMaxAcceleration,
            float verticalJointLimitMinAcceleration, 
            float verticalJointLimitMaxAcceleration)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            float horizontalJointLimitMinVelocity, 
            float horizontalJointLimitMaxVelocity,
            float verticalJointLimitMinVelocity, 
            float verticalJointLimitMaxVelocity)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            float horizontalJointLimitMinPosition, 
            float horizontalJointLimitMaxPosition, 
            float verticalJointLimitMinPosition,
            float verticalJointLimitMaxPosition)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            uint horizontalJointLimitMinSetFlag, 
            uint horizontalJointLimitMaxSetFlag, 
            uint verticalJointLimitMinSetFlag,
            uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsJointLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            uint horizontalJointLimitMinSetFlag, 
            uint horizontalJointLimitMaxSetFlag, 
            uint verticalJointLimitMinSetFlag, 
            uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsJointLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            uint horizontalJointLimitMinSetFlag, 
            uint horizontalJointLimitMaxSetFlag,
            uint verticalJointLimitMinSetFlag, 
            uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsJointLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            uint horizontalJointLimitMinSetFlag, 
            uint horizontalJointLimitMaxSetFlag,
            uint verticalJointLimitMinSetFlag, 
            uint verticalJointLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsJointLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            float horizontalEndEffectorLimitMinKinetic, 
            float horizontalEndEffectorLimitMaxKinetic,
            float verticalEndEffectorLimitMinKinetic, 
            float verticalEndEffectorLimitMaxKinetic)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
            uint horizontalEndEffectorLimitMinSetFlag, 
            uint horizontalEndEffectorLimitMaxSetFlag,
            uint verticalEndEffectorLimitMinSetFlag, 
            uint verticalEndEffectorLimitMaxSetFlag)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.CurrentValueAsEndEffectorLimitPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetHomeControlZeroHomePosition()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.HomeControlZeroHomePosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointTorqueControlTorque(
            float torque1, float torque2, float torque3, float torque4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocityControlAcceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointVelocityControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocityControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocityControlDeceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointVelocityControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocityControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocityControlVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointVelocityControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointVelocityControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlAcceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointPositionControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlDeceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointPositionControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointPositionControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlPosition,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetJointPositionControlPosition(
            float position1, float position2, float position3, float position4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.JointPositionControlPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorTorqueControlTorque(
            float torque1, float torque2, float torque3, float torque4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorTorqueControlTorque,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocityControlAcceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorVelocityControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocityControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocityControlDeceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorVelocityControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocityControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocityControlVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorVelocityControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorVelocityControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlAcceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorPositionControlAcceleration(
            float acceleration1, float acceleration2, float acceleration3, float acceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlDeceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorPositionControlDeceleration(
            float deceleration1, float deceleration2, float deceleration3, float deceleration4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlDeceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorPositionControlVelocity(
            float velocity1, float velocity2, float velocity3, float velocity4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlPosition,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        public FunctionResult RequestSetEndEffectorPositionControlPosition(
            float position1, float position2, float position3, float position4)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2RobotInterfaceOperationMode.EndEffectorPositionControlPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2RobotInterface,
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

        //-------------------------------------------- Function Definition --------------------------------------------------------------------------

        //-------------------------------------------- Observer Notification Function ---------------------------------------------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2M2RobotInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2M2RobotInterfaceObserver observer)
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

        //-------------------------------------------- Observer Notification Function ---------------------------------------------------------------
    }
}
