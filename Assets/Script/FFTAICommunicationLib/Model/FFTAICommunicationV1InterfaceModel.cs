using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV1InterfaceStatusMotRT
    {
        public int PosDataJ1;
        public int PosDataJ2;

        public int SpdDataJ1;
        public int SpdDataJ2;

        public int TorDataJ1;
        public int TorDataJ2;

        public int RedunTorDataJ1;
        public int RedunTorDataJ2;
    }

    public class FFTAICommunicationV1InterfaceStatusADC
    {
        public int AdcDataS1;
        public int AdcDataS2;
        public int AdcDataS3;
        public int AdcDataS4;

        public int AdcDiagBatt;

        public float RealVolt;

        public float RealFnS1;
        public float RealFnS2;
    }
    
    public class FFTAICommunicationV1InterfaceStatusDigiInput
    {
        public int[] IDL = new int[4];
    }
    
    public class FFTAICommunicationV1InterfaceRequestModel
    {
        private const int LENGTH_OF_MESSAGE_BUF = 200;

        private const int LENGTH_OF_TARGET_ID_BUF = 1;
        private const int LENGTH_OF_SOURCE_ID_BUF = 1;
        private const int LENGTH_OF_OPERATION_MODE_BUF = 1;
        private const int LENGTH_OF_DATA_SECTION_BUF = 21;
        private const int LENGTH_OF_END_OF_FRAME_BUF = 1;

        private const int LENGTH_OF_DATA_SECTION = 21;

        // whole message buffer
        public byte[] MessageBuf;
        public uint MessageBufLength;
        public uint MessageBufParity;

        // different part buffer
        public byte[] TargetIDBuf;
        public byte[] SourceIDBuf;
        public byte[] OperationModeBuf;
        public byte[] DataSectionBuf;
        public byte[] EndOfFrameBuf;

        // different part variable
        public uint TargetID;
        public uint SourceID;
        public uint OperationMode;
        public uint[] DataSection;
        public uint EndOfFrame;

        public FFTAICommunicationV1InterfaceRequestModel()
        {
            // whole message buffer
            MessageBuf = new byte[LENGTH_OF_MESSAGE_BUF];
            MessageBufLength = 0;
            MessageBufParity = 0;

            // different part buffer
            TargetIDBuf = new byte[LENGTH_OF_TARGET_ID_BUF];
            SourceIDBuf = new byte[LENGTH_OF_SOURCE_ID_BUF];
            OperationModeBuf = new byte[LENGTH_OF_OPERATION_MODE_BUF];
            DataSectionBuf = new byte[LENGTH_OF_DATA_SECTION_BUF];
            EndOfFrameBuf = new byte[LENGTH_OF_END_OF_FRAME_BUF];

            // different part variable
            TargetID = 0;
            SourceID = 0;
            OperationMode = 0;
            DataSection = new uint[LENGTH_OF_DATA_SECTION];
            EndOfFrame = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult Update(byte[] message, uint messageLength)
        {
            // update whole message buffer
            Array.Copy(
                message,
                MessageBuf,
                messageLength);

            MessageBufLength = messageLength;

            // update different part buffer and variables
            // get target id
            Array.Copy(
                MessageBuf,
                0,
                TargetIDBuf,
                0,
                LENGTH_OF_TARGET_ID_BUF);

            TargetID =
                (uint)(TargetIDBuf[0]);

            // get source id
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF,
                SourceIDBuf,
                0,
                LENGTH_OF_SOURCE_ID_BUF);

            SourceID =
                (uint)(SourceIDBuf[0]);

            // get operation mode
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF
                    + LENGTH_OF_SOURCE_ID_BUF,
                OperationModeBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            OperationMode =
                +(uint)(OperationModeBuf[3] << 0);

            // get data section
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF
                    + LENGTH_OF_SOURCE_ID_BUF
                    + LENGTH_OF_OPERATION_MODE_BUF,
                DataSectionBuf,
                0,
                LENGTH_OF_DATA_SECTION_BUF);

            for (int i = 0; i < LENGTH_OF_DATA_SECTION_BUF; i++)
            {
                DataSection[i] =
                    +(uint)(DataSectionBuf[i] << 0);
            }

            // get end of frame
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF
                    + LENGTH_OF_SOURCE_ID_BUF
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_DATA_SECTION_BUF,
                EndOfFrameBuf,
                0,
                LENGTH_OF_END_OF_FRAME_BUF);

            EndOfFrame =
                (uint)(EndOfFrameBuf[0]);

            return FunctionResult.Success;
        }
    }

    public class FFTAICommunicationV1InterfaceResponseModel
    {
        private const int LENGTH_OF_MESSAGE_BUF = 200;

        private const int LENGTH_OF_TARGET_ID_BUF = 1;
        private const int LENGTH_OF_SOURCE_ID_BUF = 1;
        private const int LENGTH_OF_OPERATION_MODE_BUF = 1;
        private const int LENGTH_OF_DATA_SECTION_BUF = 21;
        private const int LENGTH_OF_END_OF_FRAME_BUF = 1;

        private const int LENGTH_OF_DATA_SECTION = 21;

        // whole message buffer
        public byte[] MessageBuf;
        public uint MessageBufLength;
        public uint MessageBufParity;

        // different part buffer
        public byte[] TargetIDBuf;
        public byte[] SourceIDBuf;
        public byte[] OperationModeBuf;
        public byte[] DataSectionBuf;
        public byte[] EndOfFrameBuf;

        // different part variable
        public uint TargetID;
        public uint SourceID;
        public uint OperationMode;
        public uint[] DataSection;
        public uint EndOfFrame;

        public FFTAICommunicationV1InterfaceResponseModel()
        {
            // whole message buffer
            MessageBuf = new byte[LENGTH_OF_MESSAGE_BUF];
            MessageBufLength = 0;
            MessageBufParity = 0;

            // different part buffer
            TargetIDBuf = new byte[LENGTH_OF_TARGET_ID_BUF];
            SourceIDBuf = new byte[LENGTH_OF_SOURCE_ID_BUF];
            OperationModeBuf = new byte[LENGTH_OF_OPERATION_MODE_BUF];
            DataSectionBuf = new byte[LENGTH_OF_DATA_SECTION_BUF];
            EndOfFrameBuf = new byte[LENGTH_OF_END_OF_FRAME_BUF];

            // different part variable
            TargetID = 0;
            SourceID = 0;
            OperationMode = 0;
            DataSection = new uint[LENGTH_OF_DATA_SECTION];
            EndOfFrame = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageLength"></param>
        /// <returns></returns>
        public FunctionResult Update(byte[] message, uint messageLength)
        {
            // update whole message buffer
            Array.Copy(
                message,
                MessageBuf,
                messageLength);

            MessageBufLength = messageLength;

            // update different part buffer and variables
            // get target id
            Array.Copy(
                MessageBuf,
                0,
                TargetIDBuf,
                0,
                LENGTH_OF_TARGET_ID_BUF);

            TargetID =
                (uint)(TargetIDBuf[0]);

            // get source id
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF,
                SourceIDBuf,
                0,
                LENGTH_OF_SOURCE_ID_BUF);

            SourceID =
                (uint)(SourceIDBuf[0]);

            // get operation mode
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF
                    + LENGTH_OF_SOURCE_ID_BUF,
                OperationModeBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            OperationMode =
                + (uint)(OperationModeBuf[0] << 0);

            // get data section
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF
                    + LENGTH_OF_SOURCE_ID_BUF
                    + LENGTH_OF_OPERATION_MODE_BUF,
                DataSectionBuf,
                0,
                LENGTH_OF_DATA_SECTION_BUF);

            for (int i = 0; i < LENGTH_OF_DATA_SECTION_BUF; i++)
            {
                DataSection[i] =
                    + (uint)(DataSectionBuf[i] << 0);
            }

            // get end of frame
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_TARGET_ID_BUF
                    + LENGTH_OF_SOURCE_ID_BUF
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_DATA_SECTION_BUF,
                EndOfFrameBuf,
                0,
                LENGTH_OF_END_OF_FRAME_BUF);

            EndOfFrame =
                (uint)(EndOfFrameBuf[0]);
            
            return FunctionResult.Success;
        }
    }

    public class FFTAICommunicationV1InterfaceModel
    {
        public FFTAICommunicationV1InterfaceRequestModel RequestModel;
        public FFTAICommunicationV1InterfaceResponseModel ResponseModel;

        public FFTAICommunicationV1InterfaceStatusMotRT StatusMotRT;
        public FFTAICommunicationV1InterfaceStatusADC StatusADC;
        public FFTAICommunicationV1InterfaceStatusDigiInput StatusDigiInput;

        // model initilization
        public FFTAICommunicationV1InterfaceModel()
        {
            RequestModel = new FFTAICommunicationV1InterfaceRequestModel();
            ResponseModel = new FFTAICommunicationV1InterfaceResponseModel();

            StatusMotRT = new FFTAICommunicationV1InterfaceStatusMotRT();
            StatusADC = new FFTAICommunicationV1InterfaceStatusADC();
            StatusDigiInput = new FFTAICommunicationV1InterfaceStatusDigiInput();
        }
    
    }

}
