using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2X2TaskInterface
    {

        //-------------------------------------------- Model Definition ----------------------------------------------------------------------------------------

        private FFTAICommunicationV2X2TaskInterfaceModel Model;

        //-------------------------------------------- Model Definition ----------------------------------------------------------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition ------------------------------------------------------------------------------------

        //-------------------------------------------- Event Observer ------------------------------------------------------------------------------------------

        public List<IFFTAICommunicationV2X2TaskInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer ------------------------------------------------------------------------------------------


        //-------------------------------------------- Function Definition -------------------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) --------------------------------------------------------------------

        public FFTAICommunicationV2X2TaskInterface()
        {
            Model = new FFTAICommunicationV2X2TaskInterfaceModel();

            Observers = new List<IFFTAICommunicationV2X2TaskInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) --------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Receive) ---------------------------------------------------------------------------

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

        //-------------------------------------------- Function Definition (Receive) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Basic) -------------------------------------------------------------------

        public FunctionResult RequestSetWorkMode(FFTAICommunicationV2X2TaskInterfaceWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.BasicWorkMode,
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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.BasicFlagTaskInProcess,
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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.BasicFlagInformation,
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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        public FunctionResult RequestSetDebugWorkMode(FFTAICommunicationV2X2TaskInterfaceDebugWorkMode workMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.DebugSetWorkMode,
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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set relay work mode.
        /// </summary>
        /// <returns>The set relay work mode.</returns>
        /// <param name="workMode">Work mode.</param>
        public FunctionResult RequestSetRelayWorkMode(FFTAICommunicationV2X2TaskInterfaceRelayWorkMode workMode)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.RelayControlSetWorkMode,
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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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

        //-------------------------------------------- Function Definition (Request - Master Control) ----------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workMode"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWorkMode(FFTAICommunicationV2X2TaskInterfaceMasterControlWorkMode workMode)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWorkMode,
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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Master Control) ----------------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Master Control - Basic) --------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWalkPassive1(uint command)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWalkPassive1Command,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { command });

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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <param name="command"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWalkPassive2(uint command)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWalkPassive2Command,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { command });

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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <param name="command"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWalkPassive3(uint command)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWalkPassive3Command,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { command });

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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <param name="command"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWalkPassive4(uint command)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWalkPassive4Command,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { command });

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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <param name="command"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWalkActive1(uint command)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWalkActive1Command,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { command });

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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <param name="command"></param>
        /// <returns></returns>
        public FunctionResult RequestSetMasterControlWalkActive2(uint command)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.MasterControlSetWalkActive2Command,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { command });

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
                                (uint)FFTAICommunicationV2RobotType.X2,
                                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Master Control - Basic) --------------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Joint Kinetic Control) ---------------------------------

        public FunctionResult RequestSetSubtaskBasicJointKineticControlKinetic(
            float leftLegHipJointKinetic, 
            float leftLegKneeJointKinetic,
            float rightLegHipJointKinetic, 
            float rightLegKneeJointKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetJointKineticControlKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipJointKinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeJointKinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipJointKinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeJointKinetic), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float leftLegHipJointAcceleration, 
            float leftLegKneeJointAcceleration,
            float rightLegHipJointAcceleration, 
            float rightLegKneeJointAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetJointVelocityControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeJointAcceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float leftLegHipJointVelocity, 
            float leftLegKneeJointVelocity,
            float rightLegHipJointVelocity, 
            float rightLegKneeJointVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetJointVelocityControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeJointVelocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float leftLegHipJointAcceleration, 
            float leftLegKneeJointAcceleration,
            float rightLegHipJointAcceleration, 
            float rightLegKneeJointAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeJointAcceleration), 0), 
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipJointAcceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeJointAcceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float leftLegHipJointVelocity, 
            float leftLegKneeJointVelocity,
            float rightLegHipJointVelocity, 
            float rightLegKneeJointVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeJointVelocity), 0), 
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipJointVelocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeJointVelocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float leftLegHipJointPosition, 
            float leftLegKneeJointPosition,
            float rightLegHipJointPosition, 
            float rightLegKneeJointPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetJointPositionControlPosition,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipJointPosition), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeJointPosition), 0), 
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipJointPosition), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeJointPosition), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float endEffectorX1Kinetic, 
            float endEffectorY1Kinetic,
            float endEffectorX2Kinetic, 
            float endEffectorY2Kinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorKineticControlKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX1Kinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY1Kinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX2Kinetic), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY2Kinetic), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float endEffectorX1Acceleration, 
            float endEffectorY1Acceleration,
            float endEffectorX2Acceleration, 
            float endEffectorY2Acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorVelocityControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX1Acceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY1Acceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX2Acceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY2Acceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float endEffectorX1Velocity, 
            float endEffectorY1Velocity,
            float endEffectorX2Velocity, 
            float endEffectorY2Velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorVelocityControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX1Velocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY1Velocity), 0), 
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX2Velocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY2Velocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float endEffectorX1Acceleration, 
            float endEffectorY1Acceleration,
            float endEffectorX2Acceleration, 
            float endEffectorY2Acceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX1Acceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY1Acceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX2Acceleration), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY2Acceleration), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float endEffectorX1Velocity, 
            float endEffectorY1Velocity,
            float endEffectorX2Velocity, 
            float endEffectorY2Velocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX1Velocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY1Velocity), 0), 
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX2Velocity), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY2Velocity), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
            float endEffectorX1Position, 
            float endEffectorY1Position,
            float endEffectorX2Position, 
            float endEffectorY2Position)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetEndEffectorPositionControlPosition,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] {
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX1Position), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY1Position), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorX2Position), 0),
                BitConverter.ToUInt32(BitConverter.GetBytes(endEffectorY2Position), 0), });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Passive Move) ------------------------------------------

        /// <summary>
        /// Requests the set subtask basic passive move acceleration.
        /// </summary>
        /// <returns>The set subtask basic passive move acceleration.</returns>
        /// <param name="leftLegHipAcceleration">Left leg hip acceleration.</param>
        /// <param name="leftLegKneeAcceleration">Left leg knee acceleration.</param>
        /// <param name="rightLegHipAcceleration">Right leg hip acceleration.</param>
        /// <param name="rightLegKneeAcceleration">Right leg knee acceleration.</param>
        public FunctionResult RequestSetSubtaskBasicPassiveMoveAcceleration(
            float leftLegHipAcceleration, float leftLegKneeAcceleration, float rightLegHipAcceleration, float rightLegKneeAcceleration)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetPassiveMoveAcceleration,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipAcceleration), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeAcceleration), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipAcceleration), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeAcceleration), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set subtask basic passive move velocity.
        /// </summary>
        /// <returns>The set subtask basic passive move velocity.</returns>
        /// <param name="leftLegHipVelocity">Left leg hip velocity.</param>
        /// <param name="leftLegKneeVelocity">Left leg knee velocity.</param>
        /// <param name="rightLegHipVelocity">Right leg hip velocity.</param>
        /// <param name="rightLegKneeVelocity">Right leg knee velocity.</param>
        public FunctionResult RequestSetSubtaskBasicPassiveMoveVelocity(
            float leftLegHipVelocity, float leftLegKneeVelocity, float rightLegHipVelocity, float rightLegKneeVelocity)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetPassiveMoveVelocity,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipVelocity), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeVelocity), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipVelocity), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeVelocity), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set subtask basic passive move position.
        /// </summary>
        /// <returns>The set subtask basic passive move position.</returns>
        /// <param name="leftLegHipPosition">Left leg hip position.</param>
        /// <param name="leftLegKneePosition">Left leg knee position.</param>
        /// <param name="rightLegHipPosition">Right leg hip position.</param>
        /// <param name="rightLegKneePosition">Right leg knee position.</param>
        public FunctionResult RequestSetSubtaskBasicPassiveMovePosition(
            float leftLegHipPosition, float leftLegKneePosition, float rightLegHipPosition, float rightLegKneePosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetPassiveMovePosition,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneePosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneePosition), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Passive Move) ------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Assist Move) -------------------------------------------

        /// <summary>
        /// Requests the set subtask basic assist move kinetic.
        /// </summary>
        /// <returns>The set subtask basic assist move kinetic.</returns>
        /// <param name="leftLegHipKinetic">Left leg hip kinetic.</param>
        /// <param name="leftLegKneeKinetic">Left leg knee kinetic.</param>
        /// <param name="rightLegHipKinetic">Right leg hip kinetic.</param>
        /// <param name="rightLegKneeKinetic">Right leg knee kinetic.</param>
        public FunctionResult RequestSetSubtaskBasicAssistMoveKinetic(
            float leftLegHipKinetic, float leftLegKneeKinetic, float rightLegHipKinetic, float rightLegKneeKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetAssistMoveKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipKinetic), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeKinetic), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipKinetic), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeKinetic), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set subtask basic assist move position.
        /// </summary>
        /// <returns>The set subtask basic assist move position.</returns>
        /// <param name="leftLegHipPosition">Left leg hip position.</param>
        /// <param name="leftLegKneePosition">Left leg knee position.</param>
        /// <param name="rightLegHipPosition">Right leg hip position.</param>
        /// <param name="rightLegKneePosition">Right leg knee position.</param>
        public FunctionResult RequestSetSubtaskBasicAssistMovePosition(
            float leftLegHipPosition, float leftLegKneePosition, float rightLegHipPosition, float rightLegKneePosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetAssistMovePosition,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneePosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneePosition), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Assist Move) -------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Active Move) -------------------------------------------

        /// <summary>
        /// Requests the set subtask basic active move kinetic.
        /// </summary>
        /// <returns>The set subtask basic active move kinetic.</returns>
        /// <param name="leftLegHipKinetic">Left leg hip kinetic.</param>
        /// <param name="leftLegKneeKinetic">Left leg knee kinetic.</param>
        /// <param name="rightLegHipKinetic">Right leg hip kinetic.</param>
        /// <param name="rightLegKneeKinetic">Right leg knee kinetic.</param>
        public FunctionResult RequestSetSubtaskBasicActiveMoveKinetic(
            float leftLegHipKinetic, float leftLegKneeKinetic, float rightLegHipKinetic, float rightLegKneeKinetic)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetActiveMoveKinetic,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipKinetic), 0), 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeKinetic), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipKinetic), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeKinetic), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Active Move) -------------------------------------------

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Transparent Control Move) ------------------------------

        /// <summary>
        /// Requests the set subtask basic transparent control move origin position.
        /// </summary>
        /// <returns>The set subtask basic transparent control move origin position.</returns>
        /// <param name="leftLegHipOriginPosition">Left leg hip origin position.</param>
        /// <param name="leftLegKneeOriginPosition">Left leg knee origin position.</param>
        /// <param name="rightLegHipOriginPosition">Right leg hip origin position.</param>
        /// <param name="rightLegKneeOriginPosition">Right leg knee origin position.</param>
        public FunctionResult RequestSetSubtaskBasicTransparentControlMoveOriginPosition(
            float leftLegHipOriginPosition, float leftLegKneeOriginPosition, float rightLegHipOriginPosition, float rightLegKneeOriginPosition)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlOriginPoint,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipOriginPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeOriginPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipOriginPosition), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeOriginPosition), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set subtask basic transparent control move m.
        /// </summary>
        /// <returns>The set subtask basic transparent control move m.</returns>
        /// <param name="leftLegHipM">Left leg hip m.</param>
        /// <param name="leftLegKneeM">Left leg knee m.</param>
        /// <param name="rightLegHipM">Right leg hip m.</param>
        /// <param name="rightLegKneeM">Right leg knee m.</param>
        public FunctionResult RequestSetSubtaskBasicTransparentControlMoveM(
            float leftLegHipM, float leftLegKneeM, float rightLegHipM, float rightLegKneeM)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlM,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipM), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeM), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipM), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeM), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set subtask basic transparent control move b.
        /// </summary>
        /// <returns>The set subtask basic transparent control move b.</returns>
        /// <param name="leftLegHipB">Left leg hip b.</param>
        /// <param name="leftLegKneeB">Left leg knee b.</param>
        /// <param name="rightLegHipB">Right leg hip b.</param>
        /// <param name="rightLegKneeB">Right leg knee b.</param>
        public FunctionResult RequestSetSubtaskBasicTransparentControlMoveB(
            float leftLegHipB, float leftLegKneeB, float rightLegHipB, float rightLegKneeB)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlB,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipB), 0), 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeB), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipB), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeB), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// Requests the set subtask basic transparent control move k.
        /// </summary>
        /// <returns>The set subtask basic transparent control move k.</returns>
        /// <param name="leftLegHipK">Left leg hip k.</param>
        /// <param name="leftLegKneeK">Left leg knee k.</param>
        /// <param name="rightLegHipK">Right leg hip k.</param>
        /// <param name="rightLegKneeK">Right leg knee k.</param>
        public FunctionResult RequestSetSubtaskBasicTransparentControlMoveK(
            float leftLegHipK, float leftLegKneeK, float rightLegHipK, float rightLegKneeK)
        {
            FunctionResult functionResult;

            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2X2TaskInterfaceOperationMode.SubtaskBasicSetTransparentControlK,
                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                (uint)FFTAICommunicationV2Saved.Zero,
                new uint[] { 
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegHipK), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(leftLegKneeK), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegHipK), 0),
                    BitConverter.ToUInt32(BitConverter.GetBytes(rightLegKneeK), 0) });

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
                (uint)FFTAICommunicationV2RobotType.X2,
                (uint)FFTAICommunicationV2FunctionType.X2TaskInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request - Subtask - Basic - Transparent Control Move) ------------------------------

        //-------------------------------------------- Function Definition (Request) ---------------------------------------------------------------------------

        //-------------------------------------------- Function Definition (Observer Notification) -------------------------------------------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2X2TaskInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2X2TaskInterfaceObserver observer)
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
