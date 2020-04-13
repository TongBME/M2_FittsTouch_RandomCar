using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2AuthenticationInterface
    {
        //-------------------------------------------- Model Definition -----------------------------------------

        public FFTAICommunicationV2AuthenticationInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------

        //-------------------------------------------- Constant Definition --------------------------------------

        public const int LENGTH_OF_AUTHENTICAION_NAME = 4;
        public const int LENGTH_OF_AUTHENTICAION_PASSWORD = 4;

        //-------------------------------------------- Constant Definition --------------------------------------

        //-------------------------------------------- Variables Definition -------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        public byte[] UserName = new byte[LENGTH_OF_AUTHENTICAION_NAME];
        public byte[] UserPassword = new byte[LENGTH_OF_AUTHENTICAION_PASSWORD];
        public int Priority = 0;

        //-------------------------------------------- Variables Definition -------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------

        public List<IFFTAICommunicationV2AuthenticationInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------

        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        public FFTAICommunicationV2AuthenticationInterface()
        {
            Model = new FFTAICommunicationV2AuthenticationInterfaceModel();

            Observers = new List<IFFTAICommunicationV2AuthenticationInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) ---------------------

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userNameByteArray"></param>
        /// <param name="userPasswordByteArray"></param>
        /// <returns></returns>
        public FunctionResult Authentication(byte[] userNameByteArray, byte[] userPasswordByteArray)
        {
            FunctionResult functionResult;

            if(userNameByteArray.Length != LENGTH_OF_AUTHENTICAION_NAME)
            {
                return FunctionResult.Fail;
            }

            if(userPasswordByteArray.Length != LENGTH_OF_AUTHENTICAION_PASSWORD)
            {
                return FunctionResult.Fail;
            }

            uint userNameUint =
                (uint)(userNameByteArray[0] << 24)
                + (uint)(userNameByteArray[1] << 16)
                + (uint)(userNameByteArray[2] << 8)
                + (uint)(userNameByteArray[3] << 0);

            uint userPasswordUint =
                (uint)(userPasswordByteArray[0] << 24)
                + (uint)(userPasswordByteArray[1] << 16)
                + (uint)(userPasswordByteArray[2] << 8)
                + (uint)(userPasswordByteArray[3] << 0);


            // build request model
            functionResult = Model.DataSectionModel.RequestModel.Update(
                                (uint)FFTAICommunicationV2AuthenticationInterfaceOperationMode.Authentication,
                                (uint)FFTAICommunicationV2NumberOfParameter.Two,
                                (uint)FFTAICommunicationV2ReadWriteOperation.Set,
                                (uint)FFTAICommunicationV2Saved.Zero,
                                new uint[] { userNameUint, userPasswordUint });

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }

            // send the frame
            functionResult = FFTAICommunicationV2Interface.Send(
                                Model.DataSectionModel.RequestModel.MessageBufLength,
                                Model.DataSectionModel.RequestModel.MessageBuf,
                                Model.DataSectionModel.RequestModel.MessageBufParity);

            if (functionResult == FunctionResult.Success)
            {

            }
            else
            {
                return FunctionResult.Fail;
            }
            
            return FunctionResult.Success;
        }
        
        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Observer Notification Function ---------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2AuthenticationInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2AuthenticationInterfaceObserver observer)
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
