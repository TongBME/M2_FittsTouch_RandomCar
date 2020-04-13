using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2DriverInterface
    {

        //-------------------------------------------- Model Definition -----------------------------------------

        private FFTAICommunicationV2DriverInterfaceModel Model;

        //-------------------------------------------- Model Definition -----------------------------------------
        
        //-------------------------------------------- Variables Definition -------------------------------------

        public FFTAICommunicationV2Interface FFTAICommunicationV2Interface;

        //-------------------------------------------- Variables Definition -------------------------------------

        //-------------------------------------------- Event Observer -------------------------------------------

        public List<IFFTAICommunicationV2DriverInterfaceObserver> Observers;

        //-------------------------------------------- Event Observer -------------------------------------------


        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        public FFTAICommunicationV2DriverInterface()
        {
            Model = new FFTAICommunicationV2DriverInterfaceModel();

            Observers = new List<IFFTAICommunicationV2DriverInterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) ---------------------

        public FunctionResult Receive(byte[] buffer, uint bufferLength)
        {
            return FunctionResult.Success;
        }

        public FunctionResult Update()
        {
            ObserverNotifyModelUpdate();

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition --------------------------------------

        //-------------------------------------------- Observer Notification Function ---------------------------

        public FunctionResult AddObserver(IFFTAICommunicationV2DriverInterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        public FunctionResult RemoveObserver(IFFTAICommunicationV2DriverInterfaceObserver observer)
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
