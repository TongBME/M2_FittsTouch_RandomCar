using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2M1TaskInterface
    {

        //-------------------------------------------- Model Definition ----------------------------------------------------------------------------------------

        private FFTAICommunicationV2M1TaskInterfaceModel Model;

        //-------------------------------------------- Model Definition ----------------------------------------------------------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition ------------------------------------------------------------------------------------

        //-------------------------------------------- Event Observer ------------------------------------------------------------------------------------------

        public List<IFFTAICommunicationV2M1TaskInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer ------------------------------------------------------------------------------------------


        //-------------------------------------------- Function Definition -------------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) --------------------------------------------------------------------

        public FFTAICommunicationV2M1TaskInterface()
        {
            Model = new FFTAICommunicationV2M1TaskInterfaceModel();

            Observers = new List<IFFTAICommunicationV2M1TaskInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) --------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Receive) ---------------------------------------------------------------------------

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

        //-------------------------------------------- Function Definition (Receive) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Basic) -------------------------------------------------------------------

        public FunctionResult RequestSetWorkMode(FFTAICommunicationV2M1TaskInterfaceWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.BasicWorkMode,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { (uint)workMode });

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
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestReadFlagTaskInProcess()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.BasicFlagTaskInProcess,
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
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.BasicFlagInformation,
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
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Basic) -------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Debug) -------------------------------------------------------------------

        public FunctionResult RequestSetDebugWorkMode(FFTAICommunicationV2M1TaskInterfaceDebugWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.DebugSetWorkMode,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { (uint)workMode });

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
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Debug) -------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Relay) -------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public FunctionResult RequestSetRelayWorkMode(FFTAICommunicationV2M1TaskInterfaceRelayWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.RelaySetWorkMode,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { (uint)workMode });

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
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Relay) -------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - MasterControl) -----------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWorkMode(uint workMode)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.MasterControlSetWorkMode,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { workMode });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - MasterControl) -----------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Kinetic Control) ---------------------------------

        public FunctionResult RequestSetSubtaskBasicJointKineticControlKinetic(
            float kinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetJointKineticControlKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(kinetic), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Kinetic Control) ---------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Velocity Control) --------------------------------

        public FunctionResult RequestSetSubtaskBasicJointVelocityControlAcceleration(
            float acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetJointVelocityControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(acceleration), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicJointVelocityControlVelocity(
            float velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetJointVelocityControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(velocity), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Velocity Control) --------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Position Control) --------------------------------

        public FunctionResult RequestSetSubtaskBasicJointPositionControlAcceleration(
            float acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(acceleration), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicJointPositionControlVelocity(
            float velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(velocity), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicJointPositionControlPosition(
            float position)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlPosition,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(position), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Velocity Control) --------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Kinetic Control) --------------------------

        public FunctionResult RequestSetSubtaskBasicEndEffectorKineticControlKinetic(
            float kinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorKineticControlKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(kinetic), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Kinetic Control) --------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Velocity Control) -------------------------

        public FunctionResult RequestSetSubtaskBasicEndEffectorVelocityControlAcceleration(
            float acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorVelocityControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(acceleration), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicEndEffectorVelocityControlVelocity(
            float velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorVelocityControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(velocity), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Velocity Control) -------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Position Control) -------------------------

        public FunctionResult RequestSetSubtaskBasicEndEffectorPositionControlAcceleration(
            float acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(acceleration), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicEndEffectorPositionControlVelocity(
            float velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(velocity), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicEndEffectorPositionControlPosition(
            float position)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlPosition,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(position), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Velocity Control) -------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - FInd Home) ---------------------------------------------

        public FunctionResult RequestSetSubtaskBasicFindHomeRequestKinetic(
            float endEffectorKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetFindHomeRequestKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorKinetic), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - FInd Home) ---------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Passive Linear Motion) ---------------------------------

        public FunctionResult RequestSetSubtaskBasicPassiveLinearMotionAcceleration(
            float acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetPassiveLinearMotionAcceleration,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(acceleration), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicPassiveLinearMotionVelocity(
            float velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetPassiveLinearMotionVelocity,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(velocity), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicPassiveLinearMotionPosition(
            float endEffectorPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetPassiveLinearMotionPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorPosition), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Passive Linear Motion) ---------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Mass Simulation) ---------------------------------------

        public FunctionResult RequestSetSubtaskBasicMassSimulationMass(
            float xMass)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetMassSimulationMass,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xMass), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicMassSimulationFriction(
            float xFriction)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetMassSimulationFrictionFactor,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xFriction), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Mass Simulation) ---------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Assist Without Sensor) ---------------------------

		public FunctionResult RequestSetSubtaskBasicForceAssistWithoutSensorForce(
			float xForce)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithoutSensorForce,
				(uint)FFTAICommunicationV2NumberOfParameter.One,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0) });

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			functionResult = FFTAICommunicationV2Interface.Send(
				FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
				(uint)FFTAICommunicationV2ProtocolVersion.Version2,
				(uint)FFTAICommunicationV2RobotType.M1,
				(uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

		//-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Assist Without Sensor) ---------------------------

		//-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Assist With Sensor) ------------------------------

		public FunctionResult RequestSetSubtaskBasicForceAssistWithSensorForce(
			float xForce)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithSensorForce,
				(uint)FFTAICommunicationV2NumberOfParameter.One,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0) });

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			functionResult = FFTAICommunicationV2Interface.Send(
				FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
				(uint)FFTAICommunicationV2ProtocolVersion.Version2,
				(uint)FFTAICommunicationV2RobotType.M1,
				(uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

		//-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Assist With Sensor) ------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Assist With Target Position) ---------------------

		public FunctionResult RequestSetSubtaskBasicForceAssistWithTargetPositionForce(
			float force)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithTargetPositionForce,
				(uint)FFTAICommunicationV2NumberOfParameter.One,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(force), 0) });

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			functionResult = FFTAICommunicationV2Interface.Send(
				FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
				(uint)FFTAICommunicationV2ProtocolVersion.Version2,
				(uint)FFTAICommunicationV2RobotType.M1,
				(uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

		public FunctionResult RequestSetSubtaskBasicForceAssistWithTargetPositionPosition(
			float xPosition)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithTargetPositionPosition,
				(uint)FFTAICommunicationV2NumberOfParameter.Two,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0) });

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			functionResult = FFTAICommunicationV2Interface.Send(
				FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
				(uint)FFTAICommunicationV2ProtocolVersion.Version2,
				(uint)FFTAICommunicationV2RobotType.M1,
				(uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Assist With Target Position) ---------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Resist Without Sensor) ---------------------------

		public FunctionResult RequestSetSubtaskBasicForceResistWithoutSensorForce(
			float xForce)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetForceResistWithoutSensorForce,
				(uint)FFTAICommunicationV2NumberOfParameter.One,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0) });

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			functionResult = FFTAICommunicationV2Interface.Send(
				FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
				(uint)FFTAICommunicationV2ProtocolVersion.Version2,
				(uint)FFTAICommunicationV2RobotType.M1,
				(uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

		//-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Resist Without Sensor) ---------------------------

		//-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Resist With Sensor) ------------------------------

		public FunctionResult RequestSetSubtaskBasicForceResistWithSensorForce(
			float xForce)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetForceResistWithSensorForce,
				(uint)FFTAICommunicationV2NumberOfParameter.One,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0) });

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			functionResult = FFTAICommunicationV2Interface.Send(
				FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
				(uint)FFTAICommunicationV2ProtocolVersion.Version2,
				(uint)FFTAICommunicationV2RobotType.M1,
				(uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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


		//-------------------------------------------- Function Definition (Request - Subtask - Basic - Force Resist With Sensor) ------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Transparent Control) -----------------------------------

        public FunctionResult RequestSetSubtaskBasicTransparentControlOriginPosition(
            float xPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlOriginPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlM(
            float xM)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlM,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xM), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlB(
            float xB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlB,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xB), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlK(
            float xK)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlK,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xK), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M1,
                                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Transparent Control) -----------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Kinetic Control With Sensor) ---------------------------

        public FunctionResult RequestSetSubtaskBasicKineticControlWithSensorForce(
            float xForce)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetKineticControlWithSensorForce,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Kinetic Control With Sensor) ---------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Transparent Control With Limit Spring Force) -----------

        public FunctionResult RequestSetSubtaskBasicTransparentControlWithLimitSpringForceOriginPosition(
            float xPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceOriginPoint,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlWithLimitSpringForceM(
            float xM)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceM,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xM), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlWithLimitSpringForceB(
            float xB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceB,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xB), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlWithLimitSpringForceK(
            float xK)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceK,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xK), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTransparentControlWithLimitSpringForceLimitSpringForce(
            float xLimitSpringForce)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M1TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceLimitSpringForce,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xLimitSpringForce), 0) });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M1,
                (uint)FFTAICommunicationV2FunctionType.M1TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Transparent Control With Limit Spring Force) -----------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (Request) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Observer Notification) -------------------------------------------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2M1TaskInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2M1TaskInterfaceObserver observer)
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

        //-------------------------------------------- Function Definition (Observer Notification) -------------------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------------------------------------------

    }
}
