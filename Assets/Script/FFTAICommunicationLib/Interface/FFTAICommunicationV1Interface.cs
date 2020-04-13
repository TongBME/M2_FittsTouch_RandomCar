using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV1Interface
    {

        //-------------------------------------------- Model Definition ----------------------------------------------------

        private FFTAICommunicationV1InterfaceModel Model;

        //-------------------------------------------- Model Definition ----------------------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Variables Definition (to MMU) ---------------------------------------

        public FFTAICommunicationInterface FFTAICommunicationInterface;

        //-------------------------------------------- Variables Definition (to MMU) ---------------------------------------

        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Event Observer ------------------------------------------------------

        private List<IFFTAICommunicationV1InterfaceObserver> Observers;

        //-------------------------------------------- Event Handler -------------------------------------------------------


        //-------------------------------------------- Function Definition -------------------------------------------------

        //-------------------------------------------- Function Definition (Initialization) --------------------------------

        public FFTAICommunicationV1Interface()
        {
            Model = new FFTAICommunicationV1InterfaceModel();

            Observers = new List<IFFTAICommunicationV1InterfaceObserver>();
        }

        //-------------------------------------------- Function Definition (Initialization) --------------------------------

        //-------------------------------------------- Function Definition (Receive) ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult Parse(byte[] buffer, uint bufferLength)
        {
            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="bufferLength"></param>
        /// <returns></returns>
        public FunctionResult Receive(byte[] buffer, uint bufferLength)
        {
            if (Model.ResponseModel.Update(buffer, bufferLength) == FunctionResult.Success)
            {
                FFTAICommunicationV1OperationMode operationMode =
                    (FFTAICommunicationV1OperationMode)Model.ResponseModel.OperationMode;

                switch (operationMode)
                {
                    case FFTAICommunicationV1OperationMode.None:
                        break;
                    case FFTAICommunicationV1OperationMode.MiscMotionFeedback:
                        Model.StatusMotRT.RedunTorDataJ1 = BitConverter.ToInt16(new byte[] { Model.ResponseModel.MessageBuf[3], Model.ResponseModel.MessageBuf[4] }, 0);
                        Model.StatusMotRT.RedunTorDataJ2 = BitConverter.ToInt16(new byte[] { Model.ResponseModel.MessageBuf[5], Model.ResponseModel.MessageBuf[6] }, 0);
                        break;
                    case FFTAICommunicationV1OperationMode.CommandFeeback:
                        break;
                    case FFTAICommunicationV1OperationMode.MotorMotionFeedback:
                        Model.StatusMotRT.PosDataJ1 =
                            BitConverter.ToInt32(
                                new byte[] { Model.ResponseModel.MessageBuf[3], Model.ResponseModel.MessageBuf[4], Model.ResponseModel.MessageBuf[5], Model.ResponseModel.MessageBuf[6] }, 0);
                        Model.StatusMotRT.SpdDataJ1 =
                            BitConverter.ToInt32(
                                new byte[] { Model.ResponseModel.MessageBuf[7], Model.ResponseModel.MessageBuf[8], Model.ResponseModel.MessageBuf[9], Model.ResponseModel.MessageBuf[10] }, 0);
                        Model.StatusMotRT.TorDataJ1 =
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[11], Model.ResponseModel.MessageBuf[12] }, 0);

                        Model.StatusMotRT.PosDataJ2 =
                            BitConverter.ToInt32(
                                new byte[] { Model.ResponseModel.MessageBuf[13], Model.ResponseModel.MessageBuf[14], Model.ResponseModel.MessageBuf[15], Model.ResponseModel.MessageBuf[16] }, 0);
                        Model.StatusMotRT.SpdDataJ2 =
                            BitConverter.ToInt32(
                                new byte[] { Model.ResponseModel.MessageBuf[17], Model.ResponseModel.MessageBuf[18], Model.ResponseModel.MessageBuf[19], Model.ResponseModel.MessageBuf[20] }, 0);
                        Model.StatusMotRT.TorDataJ2 =
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[21], Model.ResponseModel.MessageBuf[22] }, 0);

                        Model.StatusDigiInput.IDL[0] = (Model.ResponseModel.MessageBuf[23] >> 0 & 0x01);
                        Model.StatusDigiInput.IDL[1] = (Model.ResponseModel.MessageBuf[23] >> 1 & 0x01);
                        Model.StatusDigiInput.IDL[2] = (Model.ResponseModel.MessageBuf[23] >> 2 & 0x01);
                        Model.StatusDigiInput.IDL[3] = (Model.ResponseModel.MessageBuf[23] >> 3 & 0x01);
                        break;
                    case FFTAICommunicationV1OperationMode.SysStatusFeedback:
                        Model.StatusADC.AdcDataS1 = 
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[3], Model.ResponseModel.MessageBuf[4] }, 0);
                        Model.StatusADC.AdcDataS2 =
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[5], Model.ResponseModel.MessageBuf[6] }, 0);
                        Model.StatusADC.AdcDataS3 =
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[7], Model.ResponseModel.MessageBuf[8] }, 0);
                        Model.StatusADC.AdcDataS4 =
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[9], Model.ResponseModel.MessageBuf[10] }, 0);

                        Model.StatusADC.AdcDiagBatt =
                            BitConverter.ToInt16(
                                new byte[] { Model.ResponseModel.MessageBuf[11], Model.ResponseModel.MessageBuf[12] }, 0);
                        
                        Model.StatusADC.RealVolt = (float)Model.StatusADC.AdcDiagBatt / 96.96f;
                        Model.StatusADC.RealFnS1 = (float)Model.StatusADC.AdcDataS1 / 3f;
                        Model.StatusADC.RealFnS2 = (float)Model.StatusADC.AdcDataS2 / 3f;

                        break;
                    default:
                        break;
                }
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

        //-------------------------------------------- Function Definition (Receive) ---------------------------------------

        //-------------------------------------------- Function Definition (Request) ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opType"></param>
        /// <returns></returns>
        public FunctionResult RequestInitOp(byte opType)
        {
            byte[] data = new byte[]
            {
                48, 49, 255,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };
            
            byte[] opTypeArray = new byte[4];

            opTypeArray = BitConverter.GetBytes(opType);

            Array.Copy(opTypeArray, 0, data, 3, 1);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult RequestHomeCal()
        {
            byte[] data = new byte[]
            {
                48, 49, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult RequestClearAlm()
        {
            byte[] data = new byte[]
            {
                48, 49, 2,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult RequestClearFault()
        {
            byte[] data = new byte[]
            {
                48, 49, 3,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult RequestServoOn()
        {
            byte[] data = new byte[]
            {
                48, 49, 4,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult RequestServoOff()
        {
            byte[] data = new byte[]
            {
                48, 49, 5,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult RequestPauseMotion()
        {
            byte[] data = new byte[]
            {
                48, 49, 7,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="spdData"></param>
        /// <returns></returns>
        public FunctionResult RequestLinePassive(int xPos, int yPos, int spdData)
        {
            byte[] data = new byte[]
            {
                48, 49, 8,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] XPosArray = new byte[4];
            byte[] YPosArray = new byte[4];
            byte[] SpdDataArray = new byte[4];

            XPosArray = BitConverter.GetBytes((int)xPos);
            YPosArray = BitConverter.GetBytes((int)yPos);
            SpdDataArray = BitConverter.GetBytes((int)spdData);

            Array.Copy(XPosArray, 0, data, 3, 4);
            Array.Copy(YPosArray, 0, data, 7, 4);
            Array.Copy(SpdDataArray, 0, data, 15, 4);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotatePos"></param>
        /// <param name="spdData"></param>
        /// <returns></returns>
        public FunctionResult RequestPassiveRotate(int rotatePos, int spdData)
        {
            byte[] data = new byte[]
            {
                48, 49, 8,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] RotatePosArray = new byte[4];
            byte[] SpdDataArray = new byte[4];

            RotatePosArray = BitConverter.GetBytes((int)rotatePos);
            SpdDataArray = BitConverter.GetBytes((int)spdData);

            Array.Copy(RotatePosArray, 0, data, 3, 4);
            Array.Copy(SpdDataArray, 0, data, 15, 4);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radiusVal"></param>
        /// <param name="spdVal"></param>
        /// <param name="dirVal"></param>
        /// <returns></returns>
        public FunctionResult RequestCirclePassive(int radiusVal, int spdVal, int dirVal)
        {
            byte[] data = new byte[]
            {
                48, 49, 15,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] RadiusValArray = new byte[4];
            byte[] SpdValArray = new byte[4];
            byte[] DirValArray = new byte[4];

            RadiusValArray = BitConverter.GetBytes((int)radiusVal);
            SpdValArray = BitConverter.GetBytes((int)spdVal);
            DirValArray = BitConverter.GetBytes((byte)dirVal);

            Array.Copy(RadiusValArray, 0, data, 3, 4);
            Array.Copy(SpdValArray, 0, data, 7, 4);
            Array.Copy(DirValArray, 0, data, 11, 1);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="massValueMN"></param>
        /// <param name="frictionFactor"></param>
        /// <returns></returns>
        public FunctionResult RequestMassSim(int massValueMN, int frictionFactor)
        {
            byte[] data = new byte[]
            {
                48, 49, 9,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] massValueMNArray = new byte[4];
            byte[] frictionFactorArray = new byte[4];

            massValueMNArray = BitConverter.GetBytes(massValueMN);
            frictionFactorArray = BitConverter.GetBytes(frictionFactor);

            Array.Copy(massValueMNArray, 0, data, 3, 2);
            Array.Copy(frictionFactorArray, 0, data, 5, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assistTor"></param>
        /// <returns></returns>
        public FunctionResult RequestAssistLT(int assistTor)
        {
            byte[] data = new byte[]
            {
                48, 49, 10,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] assistTorArray = new byte[4];

            assistTorArray = BitConverter.GetBytes(assistTor);

            Array.Copy(assistTorArray, 0, data, 3, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resistTor"></param>
        /// <returns></returns>
        public FunctionResult RequestResistLT(int resistTor)
        {
            byte[] data = new byte[]
            {
                48, 49, 11,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] resistTorArray = new byte[4];

            resistTorArray = BitConverter.GetBytes(resistTor);

            Array.Copy(resistTorArray, 0, data, 3, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);
            
            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="maxTor"></param>
        /// <returns></returns>
        public FunctionResult RequestTracAssistVT(int xPos, int yPos, int maxTor)
        {
            byte[] data = new byte[]
            {
                48, 49, 12,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] xPosArray = new byte[4];
            byte[] yPosArray = new byte[4];
            byte[] maxTorArray = new byte[4];

            xPosArray = BitConverter.GetBytes(xPos);
            yPosArray = BitConverter.GetBytes(yPos);
            maxTorArray = BitConverter.GetBytes(maxTor);

            Array.Copy(xPosArray, 0, data, 3, 4);
            Array.Copy(yPosArray, 0, data, 7, 4);
            Array.Copy(maxTorArray, 0, data, 15, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="maxTor"></param>
        /// <returns></returns>
        public FunctionResult RequestSyncVectorTorqueAst(int xPos, int yPos, int maxTor)
        {
            byte[] data = new byte[]
            {
                48, 49, 13,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] xPosArray = new byte[4];
            byte[] yPosArray = new byte[4];
            byte[] maxTorArray = new byte[4];

            xPosArray = BitConverter.GetBytes(xPos);
            yPosArray = BitConverter.GetBytes(yPos);
            maxTorArray = BitConverter.GetBytes(maxTor);

            Array.Copy(xPosArray, 0, data, 3, 4);
            Array.Copy(yPosArray, 0, data, 7, 4);
            Array.Copy(maxTorArray, 0, data, 15, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="maxTor"></param>
        /// <returns></returns>
        public FunctionResult RequestSyncVectorTorqueTrapezoidalAst(int xPos, int yPos, int maxTor)
        {
            byte[] data = new byte[]
            {
                48, 49, 14,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] xPosArray = new byte[4];
            byte[] yPosArray = new byte[4];
            byte[] maxTorArray = new byte[4];

            xPosArray = BitConverter.GetBytes(xPos);
            yPosArray = BitConverter.GetBytes(yPos);
            maxTorArray = BitConverter.GetBytes(maxTor);

            Array.Copy(xPosArray, 0, data, 3, 4);
            Array.Copy(yPosArray, 0, data, 7, 4);
            Array.Copy(maxTorArray, 0, data, 15, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 该命令仅限M1中使用，是带抗阻的助力模式
        /// </summary>
        /// <param name="XPos"></param>
        /// <param name="YPos"></param>
        /// <param name="MaxTor"></param>
        public FunctionResult RequestSyncVectorReleaseTrap(int xPos, int yPos, int maxTor)
        {
            byte[] data = new byte[]
            {
                48, 49, 28,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] xPosArray = new byte[4];
            byte[] yPosArray = new byte[4];
            byte[] maxTorArray = new byte[4];

            xPosArray = BitConverter.GetBytes(xPos);
            yPosArray = BitConverter.GetBytes(yPos);
            maxTorArray = BitConverter.GetBytes(maxTor);

            Array.Copy(xPosArray, 0, data, 3, 4);
            Array.Copy(yPosArray, 0, data, 7, 4);
            Array.Copy(maxTorArray, 0, data, 15, 2);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setExit"></param>
        /// <returns></returns>
        public FunctionResult RequestXColliderSetCw(int setExit)
        {
            byte[] data = new byte[]
            {
                48, 49, 19,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] setExitArray = new byte[4];

            setExitArray = BitConverter.GetBytes(setExit);

            Array.Copy(setExitArray, 0, data, 3, 1);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setExit"></param>
        /// <returns></returns>
        public FunctionResult RequestXColliderSetCcw(int setExit)
        {
            byte[] data = new byte[]
            {
                48, 49, 20,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] setExitArray = new byte[4];

            setExitArray = BitConverter.GetBytes(setExit);

            Array.Copy(setExitArray, 0, data, 3, 1);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setExit"></param>
        /// <returns></returns>
        public FunctionResult RequestYColliderSetCw(int setExit)
        {
            byte[] data = new byte[]
            {
                48, 49, 21,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] setExitArray = new byte[4];

            setExitArray = BitConverter.GetBytes(setExit);

            Array.Copy(setExitArray, 0, data, 3, 1);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setExit"></param>
        /// <returns></returns>
        public FunctionResult RequestYColliderSetCcw(int setExit)
        {
            byte[] data = new byte[]
            {
                48, 49, 22,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] setExitArray = new byte[4];

            setExitArray = BitConverter.GetBytes(setExit);

            Array.Copy(setExitArray, 0, data, 3, 1);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="protectTorValueX"></param>
        /// <param name="protectTorValueY"></param>
        /// <returns></returns>
        public FunctionResult RequestChgCalProtectTor(int protectTorValueX, int protectTorValueY)
        {
            byte[] data = new byte[]
            {
                48, 49, 23,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] protectTorValueXArray = new byte[4];
            byte[] protectTorValueYArray = new byte[4];

            protectTorValueXArray = BitConverter.GetBytes(protectTorValueX);
            protectTorValueYArray = BitConverter.GetBytes(protectTorValueY);

            Array.Copy(protectTorValueXArray, 0, data, 3, 2);
            Array.Copy(protectTorValueYArray, 0, data, 5, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="step"></param>
        /// <param name="maxTime"></param>
        /// <param name="xScale"></param>
        /// <param name="yScale"></param>
        /// <returns></returns>
        public FunctionResult RequestChgVectorTorqueTrapezoidalCal(int step, int maxTime, float xScale, float yScale)
        {
            byte[] data = new byte[]
            {
                48, 49, 25,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] stepArray;
            byte[] maxTimeArray = new byte[4];
            byte[] xScaleArray = new byte[4];
            byte[] yScaleArray = new byte[4];

            stepArray = BitConverter.GetBytes(step);
            maxTimeArray = BitConverter.GetBytes(maxTime);
            xScaleArray = BitConverter.GetBytes(xScale);
            yScaleArray = BitConverter.GetBytes(yScale);

            Array.Copy(stepArray, 0, data, 3, 2);
            Array.Copy(maxTimeArray, 0, data, 5, 2);
            Array.Copy(xScaleArray, 0, data, 7, 2);
            Array.Copy(yScaleArray, 0, data, 9, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setODL0"></param>
        /// <param name="setODL1"></param>
        /// <param name="setODL2"></param>
        /// <param name="setODL3"></param>
        /// <returns></returns>
        public FunctionResult RequestOperateDOFun(byte setODL0, byte setODL1, byte setODL2, byte setODL3)
        {
            byte[] data = new byte[]
            {
                48, 49, 6,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] setODL0Array = new byte[4];
            byte[] setODL1Array = new byte[4];
            byte[] setODL2Array = new byte[4];
            byte[] setODL3Array = new byte[4];

            setODL0Array = BitConverter.GetBytes(setODL0);
            setODL1Array = BitConverter.GetBytes(setODL1);
            setODL2Array = BitConverter.GetBytes(setODL2);
            setODL3Array = BitConverter.GetBytes(setODL3);

            Array.Copy(setODL0Array, 0, data, 3, 1);
            Array.Copy(setODL1Array, 0, data, 4, 1);
            Array.Copy(setODL2Array, 0, data, 5, 1);
            Array.Copy(setODL3Array, 0, data, 6, 1);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limitValX"></param>
        /// <param name="limitValY"></param>
        /// <returns></returns>
        public FunctionResult RequestChgCwLimitFun(int limitValX, int limitValY)
        {
            byte[] data = new byte[]
            {
                48, 49, 16,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };
            
            byte[] limitValXArray = new byte[4];
            byte[] limitValYArray = new byte[4];

            limitValXArray = BitConverter.GetBytes(limitValX);
            limitValYArray = BitConverter.GetBytes(limitValY);

            Array.Copy(limitValXArray, 0, data, 3, 4);
            Array.Copy(limitValYArray, 0, data, 7, 4);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limitValX"></param>
        /// <param name="limitValY"></param>
        /// <returns></returns>
        public FunctionResult RequestChgCcwLimitFun(int limitValX, int limitValY)
        {
            byte[] data = new byte[]
            {
                48, 49, 17,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] limitValXArray = new byte[4];
            byte[] limitValYArray = new byte[4];

            limitValXArray = BitConverter.GetBytes(limitValX);
            limitValYArray = BitConverter.GetBytes(limitValY);

            Array.Copy(limitValXArray, 0, data, 3, 4);
            Array.Copy(limitValYArray, 0, data, 7, 4);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="massValueMN"></param>
        /// <param name="frictionFactor"></param>
        /// <returns></returns>
        public FunctionResult RequestInertiaSim(int massValueMN, int frictionFactor)
        {
            byte[] data = new byte[]
            {
                48, 49, 26,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] massValueMNArray = new byte[4];
            byte[] frictionFactorArray = new byte[4];

            massValueMNArray = BitConverter.GetBytes(massValueMN);
            frictionFactorArray = BitConverter.GetBytes(frictionFactor);

            Array.Copy(massValueMNArray, 0, data, 3, 2);
            Array.Copy(frictionFactorArray, 0, data, 5, 2);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public FunctionResult RequestLaserOperate(byte status)
        {
            byte[] statusArray = new byte[4];

            statusArray = BitConverter.GetBytes(status);

            byte[] data = new byte[]
            {
                48, 49, 27,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            Array.Copy(statusArray, 0, data, 3, 1);

            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="spdData"></param>
        /// <returns></returns>
        public FunctionResult RequestTestMode(int xPos, int yPos, int spdData)
        {
            byte[] data = new byte[]
            {
                48, 49, 153,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                160
            };

            byte[] xPosArray = new byte[4];
            byte[] yPosArray = new byte[4];
            byte[] spdDataArray = new byte[4];

            xPosArray = BitConverter.GetBytes(xPos);
            yPosArray = BitConverter.GetBytes(yPos);
            spdDataArray = BitConverter.GetBytes(spdData);

            Array.Copy(xPosArray, 0, data, 3, 4);
            Array.Copy(yPosArray, 0, data, 7, 4);
            Array.Copy(spdDataArray, 0, data, 15, 4);
            
            FFTAICommunicationInterface.SendFrame(data, (uint)data.Length);

            return FunctionResult.Success;
        }

        //-------------------------------------------- Function Definition (Request) ---------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------

        //-------------------------------------------- Observer Notification Function --------------------------------------

        /// <summary>
        /// Add Observer to FFTAI Communication V1 Interface
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public FunctionResult AddObserver(IFFTAICommunicationV1InterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                return FunctionResult.Success;
            }

            Observers.Add(observer);

            return FunctionResult.Success;
        }

        /// <summary>
        /// Remove Observer from FFTAI Communication V1 Interface
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        public FunctionResult RemoveObserver(IFFTAICommunicationV1InterfaceObserver observer)
        {
            if (Observers.Contains(observer) == true)
            {
                Observers.Remove(observer);
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FunctionResult ObserverNotifyModelUpdate()
        {
            for (int i = 0; i < Observers.Count; i++)
            {
                Observers[i].ModelUpdateHandle(Model);
            }

            return FunctionResult.Success;
        }

        //-------------------------------------------- Observer Notification Function --------------------------------------

    }
}
