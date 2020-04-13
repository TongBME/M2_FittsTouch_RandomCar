using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2IAPInterface
    {
        //-------------------------------------------- Model Definition -----------------------------------------

        public FFTAICommunicationV2IAPInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------

        //-------------------------------------------- Constant Definition --------------------------------------
      
        //-------------------------------------------- Constant Definition --------------------------------------

        //-------------------------------------------- Variables Definition -------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition -------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------

        public List<IFFTAICommunicationV2IAPInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------

        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        public FFTAICommunicationV2IAPInterface()
        {
            Model = new FFTAICommunicationV2IAPInterfaceModel();

            Observers = new List<IFFTAICommunicationV2IAPInterfaceObserver>();
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

        //-------------------------------------------- Function Definition (Receive) ----------------------------

        //-------------------------------------------- Function Definition (Request) ----------------------------

        public FunctionResult RequestGetInitedFlag()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.InitedFlag,
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
                (uint)FFTAICommunicationV2RobotType.All,
                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestGetSoftwareVersion()
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.SoftwareVersion,
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
                (uint)FFTAICommunicationV2RobotType.All,
                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                Model.DataSectionModel.RequestModel.MessageBufLength,
                Model.DataSectionModel.RequestModel.MessageBuf,
                Model.DataSectionModel.RequestModel.MessageBufParity,
                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

		public FunctionResult RequestGetBootMode()
		{
			FunctionResult functionResult;

			// build request model
			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2IAPInterfaceOperationMode.BootMode,
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
				(uint)FFTAICommunicationV2RobotType.All,
				(uint)FFTAICommunicationV2FunctionType.IAPInterface,
				Model.DataSectionModel.RequestModel.MessageBufLength,
				Model.DataSectionModel.RequestModel.MessageBuf,
				Model.DataSectionModel.RequestModel.MessageBufParity,
				FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			return FunctionResult.Success;
		}

        public FunctionResult RequestSetBootMode(
            uint bootMode)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.BootMode,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { bootMode });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }
        
		public FunctionResult RequestGetWorkStatus()
		{
			FunctionResult functionResult;

			// build request model
			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2IAPInterfaceOperationMode.WorkStatus,
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
				(uint)FFTAICommunicationV2RobotType.All,
				(uint)FFTAICommunicationV2FunctionType.IAPInterface,
				Model.DataSectionModel.RequestModel.MessageBufLength,
				Model.DataSectionModel.RequestModel.MessageBuf,
				Model.DataSectionModel.RequestModel.MessageBufParity,
				FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			return FunctionResult.Success;
		}

        public FunctionResult RequestSetIAPStartAddress(
            uint iapStartAddress)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.IapStartAddress,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { iapStartAddress });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetIAPSize(
            uint iapSize)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.IapSize,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { iapSize });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetAPPStartAddress(
            uint appStartAddress)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.AppStartAddress,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { appStartAddress });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetAPPSize(
            uint appSize)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
								(uint)FFTAICommunicationV2IAPInterfaceOperationMode.AppSize,
                                (uint)FFTAICommunicationV2NumberOfParameter.One,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { appSize });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

        public FunctionResult RequestSetUpgradeIAP(
            uint addressOffset, uint value)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeIap,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { addressOffset, value });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

		public FunctionResult RequestSetUpgradeIAPFast(
			uint addressOffset, byte[] buffer, uint bufferSize)
		{
			FunctionResult functionResult;

			// set parameters
			uint parameterSize = 1 + (bufferSize / 4);
			uint[] parameters = new uint[parameterSize];

			parameters [0] = addressOffset;

			for (uint i = 0; i < (parameterSize - 1); i++) {
				parameters [i + 1] = 
					((uint)buffer [i * 4 + 0] << 24) 
					+ ((uint)buffer [i * 4 + 1] << 16)
					+ ((uint)buffer [i * 4 + 2] << 8)
					+ ((uint)buffer [i * 4 + 3] << 0);
			}

			// build request model
			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeIapFast,
				(uint)parameterSize,
				(uint)FFTAICommunicationV2ReadWriteOperation.Write,
				(uint)FFTAICommunicationV2Saved.Zero,
				parameters);

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
				(uint)FFTAICommunicationV2FunctionType.IAPInterface,
				Model.DataSectionModel.RequestModel.MessageBufLength,
				Model.DataSectionModel.RequestModel.MessageBuf,
				Model.DataSectionModel.RequestModel.MessageBufParity,
				FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			return FunctionResult.Success;
		}

        public FunctionResult RequestSetUpgradeAPP(
            uint addressOffset, byte value)
        {
            FunctionResult functionResult;

            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeApp,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Write,
                                (uint)FFTAICommunicationV2Saved.Zero,
				new uint[] { addressOffset, (uint)value });

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
                                (uint)FFTAICommunicationV2FunctionType.IAPInterface,
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity,
                                FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            return FunctionResult.Success;
        }

		public FunctionResult RequestSetUpgradeAPPFast(
			uint addressOffset, byte[] buffer, uint bufferSize)
		{
			FunctionResult functionResult;

			// set parameters
			uint parameterSize = 1 + (bufferSize / 4);
			uint[] parameters = new uint[parameterSize];

			parameters [0] = addressOffset;

			for (uint i = 0; i < (parameterSize - 1); i++) {
				parameters [i + 1] = 
					((uint)buffer [i * 4 + 0] << 24) 
					+ ((uint)buffer [i * 4 + 1] << 16)
					+ ((uint)buffer [i * 4 + 2] << 8)
					+ ((uint)buffer [i * 4 + 3] << 0);
			}

			// build request model
			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2IAPInterfaceOperationMode.UpgradeAppFast,
				(uint)parameterSize,
				(uint)FFTAICommunicationV2ReadWriteOperation.Write,
				(uint)FFTAICommunicationV2Saved.Zero,
				parameters);

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
				(uint)FFTAICommunicationV2FunctionType.IAPInterface,
				Model.DataSectionModel.RequestModel.MessageBufLength,
				Model.DataSectionModel.RequestModel.MessageBuf,
				Model.DataSectionModel.RequestModel.MessageBufParity,
				FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			return FunctionResult.Success;
		}

		public FunctionResult RequestSetEraseIAP()
		{
			FunctionResult functionResult;

			// build request model
			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2IAPInterfaceOperationMode.EraseIap,
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
				(uint)FFTAICommunicationV2RobotType.All,
				(uint)FFTAICommunicationV2FunctionType.IAPInterface,
				Model.DataSectionModel.RequestModel.MessageBufLength,
				Model.DataSectionModel.RequestModel.MessageBuf,
				Model.DataSectionModel.RequestModel.MessageBufParity,
				FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

			if (functionResult == FunctionResult.Success)
			{

			}
			else
			{
				return FunctionResult.Fail;
			}

			return FunctionResult.Success;
		}

		public FunctionResult RequestSetEraseAPP()
		{
			FunctionResult functionResult;

			// build request model
			functionResult = Model.DataSectionModel.RequestModel.Update(
				(uint)FFTAICommunicationV2IAPInterfaceOperationMode.EraseApp,
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
				(uint)FFTAICommunicationV2RobotType.All,
				(uint)FFTAICommunicationV2FunctionType.IAPInterface,
				Model.DataSectionModel.RequestModel.MessageBufLength,
				Model.DataSectionModel.RequestModel.MessageBuf,
				Model.DataSectionModel.RequestModel.MessageBufParity,
				FFTAICommunicationV2InterfaceRequestModel.END_OF_REQUEST_FRAME);

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

        public FunctionResult AddObserver(IFFTAICommunicationV2IAPInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2IAPInterfaceObserver observer)
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
