using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2HardwareInterface
    {

        //-------------------------------------------- Model Definition -----------------------------------------

        private FFTAICommunicationV2HardwareInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------

        //-------------------------------------------- Variables Definition -------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition -------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------

        public List<IFFTAICommunicationV2HardwareInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------


        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        public FFTAICommunicationV2HardwareInterface()
        {
            Model = new FFTAICommunicationV2HardwareInterfaceModel();

            Observers = new List<IFFTAICommunicationV2HardwareInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        //-------------------------------------------- Function Definition (Receive) ----------------------------

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

        //-------------------------------------------- Function Definition (Receive) ----------------------------

        //-------------------------------------------- Function Definition (Request) ----------------------------
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DO1Status"></param>
        /// <param name="DO2Status"></param>
        /// <param name="DO3Status"></param>
        /// <param name="DO4Status"></param>
        /// <returns></returns>
        public FunctionResult RequestSetGpioIoStatus(uint DO1Status, uint DO2Status, uint DO3Status, uint DO4Status)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2HardwareInterfaceOperationMode.ODLGpioIOStatus,
                                (uint)FFTAICommunicationV2NumberOfParameter.Four,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { DO1Status, DO2Status, DO3Status, DO4Status });

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
                                (uint)FFTAICommunicationV2FunctionType.HardwareInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <returns></returns>
        public FunctionResult RequestGetGpioIoStatus()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2HardwareInterfaceOperationMode.IDLGpioIOStatus,
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
                                (uint)FFTAICommunicationV2RobotType.M2,
                                (uint)FFTAICommunicationV2FunctionType.HardwareInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <returns></returns>
        public FunctionResult RequestSetGpioLedRedLedStatus()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2HardwareInterfaceOperationMode.GpioLedRedLedStatus,
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
                                (uint)FFTAICommunicationV2FunctionType.HardwareInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <returns></returns>
        public FunctionResult RequestSetGpioLedYellowLedStatus()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2HardwareInterfaceOperationMode.GpioLedYellowLedStatus,
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
                                (uint)FFTAICommunicationV2FunctionType.HardwareInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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
        /// <returns></returns>
        public FunctionResult RequestSetGpioLedBlueLedStatus()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2HardwareInterfaceOperationMode.GpioLedBlueLedStatus,
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
                                (uint)FFTAICommunicationV2FunctionType.HardwareInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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

        //-------------------------------------------- Observer Notification Function ---------------------------

        /// <summary>
        /// Add Observer to Hardware Interface
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public FunctionResult AddObserver(IFFTAICommunicationV2HardwareInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2HardwareInterfaceObserver observer)
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
