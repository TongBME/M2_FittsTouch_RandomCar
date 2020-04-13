using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2RobotInterface
    {
        //-------------------------------------------- Model Definition -----------------------------------------

        public FFTAICommunicationV2RobotInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------

        //-------------------------------------------- Constant Definition --------------------------------------
      
        //-------------------------------------------- Constant Definition --------------------------------------

        //-------------------------------------------- Variables Definition -------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition -------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------

        public List<IFFTAICommunicationV2RobotInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------

        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        public FFTAICommunicationV2RobotInterface()
        {
            Model = new FFTAICommunicationV2RobotInterfaceModel();

            Observers = new List<IFFTAICommunicationV2RobotInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        //-------------------------------------------- Function Definition (Receive) ----------------------------

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

            Update();

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

        //-------------------------------------------- Function Definition (Receive) ----------------------------

        //-------------------------------------------- Function Definition (Request) ----------------------------
        
        public FunctionResult RequestGetRobotType()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2RobotInterfaceOperationMode.RobotType,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Get,
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
                                (uint)FFTAICommunicationV2RobotType.All,
                                (uint)FFTAICommunicationV2FunctionType.RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetRobotType(uint robotType)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2RobotInterfaceOperationMode.RobotType,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { robotType });

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
                                (uint)FFTAICommunicationV2RobotType.All,
                                (uint)FFTAICommunicationV2FunctionType.RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestGetMechanismVersion()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2RobotInterfaceOperationMode.MechanismVersion,
                                (uint)FFTAICommunicationV2NumberOfParameter.Zero,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Get,
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
                                (uint)FFTAICommunicationV2RobotType.All,
                                (uint)FFTAICommunicationV2FunctionType.RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetMechanismVersion(uint mechanismVersion)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2RobotInterfaceOperationMode.MechanismVersion,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { mechanismVersion });

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
                                (uint)FFTAICommunicationV2RobotType.All,
                                (uint)FFTAICommunicationV2FunctionType.RobotInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request) ----------------------------

        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Observer Notification Function ---------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2RobotInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2RobotInterfaceObserver observer)
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

        //-------------------------------------------- Observer Notification Function ---------------------------

    }
}
