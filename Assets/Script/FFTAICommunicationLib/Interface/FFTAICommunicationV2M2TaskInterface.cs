using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2M2TaskInterface
    {

        //-------------------------------------------- Model Definition ----------------------------------------------------------------------------------------

        private FFTAICommunicationV2M2TaskInterfaceModel Model;

        //-------------------------------------------- Model Definition ----------------------------------------------------------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition ------------------------------------------------------------------------------------

        //-------------------------------------------- Event Observer ------------------------------------------------------------------------------------------

        public List<IFFTAICommunicationV2M2TaskInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer ------------------------------------------------------------------------------------------


        //-------------------------------------------- Function Definition -------------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) --------------------------------------------------------------------

        public FFTAICommunicationV2M2TaskInterface()
        {
            Model = new FFTAICommunicationV2M2TaskInterfaceModel();

            Observers = new List<IFFTAICommunicationV2M2TaskInterfaceObserver>();
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

        public FunctionResult RequestSetWorkMode(FFTAICommunicationV2M2TaskInterfaceWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.BasicWorkMode,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.BasicFlagTaskInProcess,
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
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.BasicFlagInformation,
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
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public FunctionResult RequestSetDebugWorkMode(FFTAICommunicationV2M2TaskInterfaceDebugWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.DebugSetWorkMode,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
        public FunctionResult RequestSetRelayWorkMode(FFTAICommunicationV2M2TaskInterfaceRelayWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.RelaySetWorkMode,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
        public FunctionResult RequestSetMasterControlWorkMode(FFTAICommunicationV2M2TaskInterfaceMasterControlWorkMode workMode)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.MasterControlSetWorkMode,
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

            functionResult = FFTAICommunicationV2Interface.Send(
                                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float horizontalJointKinetic, float verticalJointKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetJointKineticControlKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointKinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointKinetic), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float horizontalJointAcceleration, float verticalJointAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetJointVelocityControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointAcceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float horizontalJointVelocity, float verticalJointVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetJointVelocityControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointVelocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float horizontalJointAcceleration, float verticalJointAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointAcceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float horizontalJointVelocity, float verticalJointVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointVelocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float horizontalJointPosition, float verticalJointPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlPosition,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(horizontalJointPosition), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(verticalJointPosition), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorXKinetic, float endEffectorYKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorKineticControlKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorXKinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorYKinetic), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorXAcceleration, float endEffectorYAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorVelocityControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorXAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorYAcceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorXVelocity, float endEffectorYVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorVelocityControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorXVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorYVelocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorXAcceleration, float endEffectorYAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorXAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorYAcceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorXVelocity, float endEffectorYVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorXVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorYVelocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorXPosition, float endEffectorYPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlPosition,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorXPosition), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorYPosition), 0), });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - End Effector Position Control) -------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - FInd Home) ---------------------------------------------

        public FunctionResult RequestSetSubtaskBasicFindHomeRequestKinetic(
            float endEffectorHorizontalKinetic, float endEffectorVerticalKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetFindHomeRequestKinetic,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorHorizontalKinetic), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorVerticalKinetic), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetPassiveLinearMotionAcceleration,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetPassiveLinearMotionVelocity,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float endEffectorHorizontalPosition, float endEffectorVerticalPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetPassiveLinearMotionPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorHorizontalPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorVerticalPosition), 0), });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xMass, float yMass)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetMassSimulationMass,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xMass), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yMass), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xFriction, float yFriction)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetMassSimulationFrictionFactor,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xFriction), 0),
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xForce, float yForce)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithoutSensorForce,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yForce), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
			float xForce, float yForce)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithSensorForce,
				(uint)FFTAICommunicationV2NumberOfParameter.Two,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0),
					BitConverter.ToUInt32(BitConverter.GetBytes(yForce), 0) });

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
				(uint)FFTAICommunicationV2RobotType.M2,
				(uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithTargetPositionForce,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetForceAssistWithTargetPositionPosition,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xForce, float yForce)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
								(uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetForceResistWithoutSensorForce,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yForce), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
			float xForce, float yForce)
		{
			FunctionResult functionResult;

			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetForceResistWithSensorForce,
				(uint)FFTAICommunicationV2NumberOfParameter.Two,
				(uint)FFTAICommunicationV2ReadWriteOperation.Set,
				(uint)FFTAICommunicationV2Saved.Zero,
				new uint[] {
					BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0),
					BitConverter.ToUInt32(BitConverter.GetBytes(yForce), 0) });

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
				(uint)FFTAICommunicationV2RobotType.M2,
				(uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlOriginPoint,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xM, float yM)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlM,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xM), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yM), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xB, float yB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlB,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xB), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yB), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xK, float yK)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlK,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xK), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yK), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Tunnel Guidance Control) -------------------------------

        public FunctionResult RequestSetSubtaskBasicTunnelGuidanceControlPointA(
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTunnelGuidanceControlPointA,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTunnelGuidanceControlPointB(
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTunnelGuidanceControlPointB,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTunnelGuidanceControlM(
            float xM, float yM)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTunnelGuidanceControlM,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xM), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yM), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTunnelGuidanceControlB(
            float xB, float yB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTunnelGuidanceControlB,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xB), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yB), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicTunnelGuidanceControlK(
            float xK, float yK)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTunnelGuidanceControlK,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xK), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yK), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Tunnel Guidance Control) -------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Draw Infinity Curve) -----------------------------------

        public FunctionResult RequestSetSubtaskBasicDrawInfinityCurveOriginPoint(
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetDrawInfinityCurveOriginPoint,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicDrawInfinityCurveTimePeriod(
            float timePeriod)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetDrawInfinityCurveTimePeriod,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(timePeriod), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicDrawInfinityCurveScale(
            float xScale, float yScale)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetDrawInfinityCurveScale,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xScale), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yScale), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Draw Infinity Curve) -----------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Draw Circle Curve) -------------------------------------

        public FunctionResult RequestSetSubtaskBasicDrawCircleCurveOriginPoint(
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetDrawCircleCurveOriginPoint,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicDrawCircleCurveTimePeriod(
            float timePeriod)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetDrawCircleCurveTimePeriod,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(timePeriod), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicDrawCircleCurveScale(
            float xScale, float yScale)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetDrawCircleCurveScale,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] {
                                    BitConverter.ToUInt32(BitConverter.GetBytes(xScale), 0),
                                    BitConverter.ToUInt32(BitConverter.GetBytes(yScale), 0) });

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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Draw Circle Curve) -------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Kinetic Control With Sensor) ---------------------------

        public FunctionResult RequestSetSubtaskBasicKineticControlWithSensorForce(
            float xForce, float yForce)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetKineticControlWithSensorForce,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xForce), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yForce), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xPosition, float yPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceOriginPoint,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xPosition), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yPosition), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xM, float yM)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceM,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xM), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yM), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xB, float yB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceB,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xB), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yB), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xK, float yK)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceK,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xK), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yK), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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
            float xLimitSpringForce, float yLimitSpringForce)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlWithLimitSpringForceLimitSpringForce,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xLimitSpringForce), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yLimitSpringForce), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Minimum Jerk Trajectory Control) -----------------------

        public FunctionResult RequestSetSubtaskBasicMinimumJerkTrajectoryControlPointA(
            float xPointA, 
            float yPointA)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicMinimumJerkTrajectoryControlPointA,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xPointA), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yPointA), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicMinimumJerkTrajectoryControlPointB(
            float xPointB, 
            float yPointB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicMinimumJerkTrajectoryControlPointB,
                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(xPointB), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(yPointB), 0) });

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicMinimumJerkTrajectoryControlInitialTime()
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicMinimumJerkTrajectoryControlInitialTime,
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

            functionResult = FFTAICommunicationV2Interface.Send(
                FFTAICommunicationV2InterfaceRequestModel.HEADER_OF_REQUEST_FRAME,
                (uint)FFTAICommunicationV2ProtocolVersion.Version2,
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        public FunctionResult RequestSetSubtaskBasicMinimumJerkTrajectoryControlTotalTime(
            float totalTime)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2M2TaskInterfaceOperationMode.SubtaskBasicMinimumJerkTrajectoryControlTotalTime,
                (uint)FFTAICommunicationV2NumberOfParameter.One,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                BitConverter.ToUInt32(BitConverter.GetBytes(totalTime), 0)});

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
                (uint)FFTAICommunicationV2RobotType.M2,
                (uint)FFTAICommunicationV2FunctionType.M2TaskInterface,
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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Minimum Jerk Trajectory Control) -----------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic) ---------------------------------------------------------

        //-------------------------------------------- Function Definition (Request) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Observer Notification) -------------------------------------------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2M2TaskInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2M2TaskInterfaceObserver observer)
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
