using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FFTAICommunicationLib;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationV2DataSectionRequestModel
    {
        public const int LENGTH_OF_MESSAGE_BUF = 512;

        public const int LENGTH_OF_OPERATION_MODE_BUF = 4;
        public const int LENGTH_OF_NUMBER_OF_PARAMETER_BUF = 1;
        public const int LENGTH_OF_READ_WRITE_OPERATION_BUF = 1;
        public const int LENGTH_OF_OPERATION_RESULT_BUF = 1;
        public const int LENGTH_OF_REQUEST_SAVED_BUF = 1;
        public const int LENGTH_OF_NUMBER_OF_REQUEST_BUF = 1;
        public const int LENGTH_OF_REQUEST_PARAMETER_BUF = 4 * 100;

        // whole message buffer
        public byte[] MessageBuf;
        public uint MessageBufLength;
        public uint MessageBufParity;

        // different part buffer
        public byte[] OperationModeBuf;
        public byte[] NumberOfParameterBuf;
        public byte[] ReadWriteOperationBuf;
        public byte[] SavedBuf;
        public byte[] ParameterBuf;

        // different part variable
        public uint OperationMode;
        public uint NumberOfParameter;
        public uint ReadWriteOperation;
        public uint Saved;
        public uint[] Parameter;

        public FFTAICommunicationV2DataSectionRequestModel()
        {
            MessageBuf = new byte[LENGTH_OF_MESSAGE_BUF];
            MessageBufLength = 0;
            MessageBufParity = 0;

            OperationModeBuf = new byte[LENGTH_OF_OPERATION_MODE_BUF];
            NumberOfParameterBuf = new byte[LENGTH_OF_NUMBER_OF_PARAMETER_BUF];
            ReadWriteOperationBuf = new byte[LENGTH_OF_READ_WRITE_OPERATION_BUF];
            SavedBuf = new byte[LENGTH_OF_REQUEST_SAVED_BUF];
            ParameterBuf = new byte[LENGTH_OF_REQUEST_PARAMETER_BUF];

            OperationMode = 0;
            NumberOfParameter = 0;
            ReadWriteOperation = 0;
            Saved = 0;
            Parameter = new uint[LENGTH_OF_REQUEST_PARAMETER_BUF / 4];
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
            // get operation mode
            Array.Copy(
                MessageBuf,
                0,
                OperationModeBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            OperationMode =
                (uint)(OperationModeBuf[0] << 24)
                + (uint)(OperationModeBuf[1] << 16)
                + (uint)(OperationModeBuf[2] << 8)
                + (uint)(OperationModeBuf[3] << 0);

            // get number of parameter
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF,
                NumberOfParameterBuf,
                0,
                LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            NumberOfParameter =
                (uint)(NumberOfParameterBuf[0]);

            // get read/write operation
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF,
                ReadWriteOperationBuf,
                0,
                LENGTH_OF_READ_WRITE_OPERATION_BUF);

            ReadWriteOperation =
                (uint)(ReadWriteOperationBuf[0]);

            // get saved buffer
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF,
                SavedBuf,
                0,
                LENGTH_OF_REQUEST_SAVED_BUF);

            // get parameter
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_REQUEST_SAVED_BUF,
                ParameterBuf,
                0,
                NumberOfParameter * 4);

            for (int i = 0; i < NumberOfParameter; i++)
            {
                Parameter[i] =
                    (uint)(ParameterBuf[i * 4 + 0] << 24)
                    + (uint)(ParameterBuf[i * 4 + 1] << 16)
                    + (uint)(ParameterBuf[i * 4 + 2] << 8)
                    + (uint)(ParameterBuf[i * 4 + 3] << 0);
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationModeBuf"></param>
        /// <param name="numberOfParameterBuf"></param>
        /// <param name="readWriteOperationBuf"></param>
        /// <param name="savedBuf"></param>
        /// <param name="parameterBuf"></param>
        /// <returns></returns>
        public FunctionResult Update(
            byte[] operationModeBuf,
            byte[] numberOfParameterBuf,
            byte[] readWriteOperationBuf,
            byte[] savedBuf,
            byte[] parameterBuf)
        {
            // update different part buffer and variables
            // get operation mode
            Array.Copy(operationModeBuf, OperationModeBuf, LENGTH_OF_OPERATION_MODE_BUF);

            OperationMode =
                (uint)(OperationModeBuf[0] << 24)
                + (uint)(OperationModeBuf[1] << 16)
                + (uint)(OperationModeBuf[2] << 8)
                + (uint)(OperationModeBuf[3] << 0);

            // get number of parameter
            Array.Copy(numberOfParameterBuf, NumberOfParameterBuf, LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            NumberOfParameter =
                (uint)(NumberOfParameterBuf[0]);

            // get read/write operation
            Array.Copy(readWriteOperationBuf, ReadWriteOperationBuf, LENGTH_OF_READ_WRITE_OPERATION_BUF);

            ReadWriteOperation =
                (uint)(ReadWriteOperationBuf[0]);

            // get saved buffer
            Array.Copy(savedBuf, SavedBuf, LENGTH_OF_REQUEST_SAVED_BUF);

            // get parameter
            Array.Copy(parameterBuf, ParameterBuf, NumberOfParameter * 4);

            for (int i = 0; i < NumberOfParameter; i++)
            {
                Parameter[i] =
                    (uint)(ParameterBuf[i * 4 + 0] << 24)
                    + (uint)(ParameterBuf[i * 4 + 1] << 16)
                    + (uint)(ParameterBuf[i * 4 + 2] << 8)
                    + (uint)(ParameterBuf[i * 4 + 3] << 0);
            }

            // update whole message buffer
            Array.Copy(
                OperationModeBuf,
                0,
                MessageBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            Array.Copy(
                NumberOfParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF,
                LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            Array.Copy(
                ReadWriteOperationBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF,
                LENGTH_OF_READ_WRITE_OPERATION_BUF);

            Array.Copy(
                SavedBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF,
                LENGTH_OF_REQUEST_SAVED_BUF);

            Array.Copy(
                ParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_REQUEST_SAVED_BUF,
                NumberOfParameter * 4);

            MessageBufLength =
                LENGTH_OF_OPERATION_MODE_BUF
                + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                + LENGTH_OF_READ_WRITE_OPERATION_BUF
                + LENGTH_OF_REQUEST_SAVED_BUF
                + NumberOfParameter * 4;

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationMode"></param>
        /// <param name="numberOfParameter"></param>
        /// <param name="readWriteOperation"></param>
        /// <param name="saved"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public FunctionResult Update(
            uint operationMode,
            uint numberOfParameter,
            uint readWriteOperation,
            uint saved,
            uint[] parameter)
        {
            // update different part buffer and variables
            OperationMode = operationMode;
            NumberOfParameter = numberOfParameter;
            ReadWriteOperation = readWriteOperation;
            Saved = saved;
            Array.Copy(parameter, Parameter, NumberOfParameter);

            for (int i = 0; i < LENGTH_OF_OPERATION_MODE_BUF; i++)
            {
                OperationModeBuf[i] = (byte)((OperationMode >> (8 * (LENGTH_OF_OPERATION_MODE_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_NUMBER_OF_PARAMETER_BUF; i++)
            {
                NumberOfParameterBuf[i] = (byte)((NumberOfParameter >> (8 * (LENGTH_OF_NUMBER_OF_PARAMETER_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_READ_WRITE_OPERATION_BUF; i++)
            {
                ReadWriteOperationBuf[i] = (byte)((ReadWriteOperation >> (8 * (LENGTH_OF_READ_WRITE_OPERATION_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_REQUEST_SAVED_BUF; i++)
            {
                SavedBuf[i] = (byte)((Saved >> (8 * (LENGTH_OF_REQUEST_SAVED_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < NumberOfParameter; i++)
            {
                ParameterBuf[i * 4 + 0] = (byte)((Parameter[i] >> 24) & 0xFF);
                ParameterBuf[i * 4 + 1] = (byte)((Parameter[i] >> 16) & 0xFF);
                ParameterBuf[i * 4 + 2] = (byte)((Parameter[i] >> 8) & 0xFF);
                ParameterBuf[i * 4 + 3] = (byte)((Parameter[i] >> 0) & 0xFF);
            }

            // update whole message buffer
            Array.Copy(
                OperationModeBuf,
                0,
                MessageBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            Array.Copy(
                NumberOfParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF,
                LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            Array.Copy(
                ReadWriteOperationBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF,
                LENGTH_OF_READ_WRITE_OPERATION_BUF);

            Array.Copy(
                SavedBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF,
                LENGTH_OF_REQUEST_SAVED_BUF);

            Array.Copy(
                ParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_REQUEST_SAVED_BUF,
                NumberOfParameter * 4);

            MessageBufLength =
                LENGTH_OF_OPERATION_MODE_BUF
                + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                + LENGTH_OF_READ_WRITE_OPERATION_BUF
                + LENGTH_OF_REQUEST_SAVED_BUF
                + NumberOfParameter * 4;

            return FunctionResult.Success;
        }
    }

    public class FFTAICommunicationV2DataSectionResponseModel
    {
        public const int LENGTH_OF_MESSAGE_BUF = 512;

        public const int LENGTH_OF_OPERATION_MODE_BUF = 4;
        public const int LENGTH_OF_NUMBER_OF_PARAMETER_BUF = 1;
        public const int LENGTH_OF_READ_WRITE_OPERATION_BUF = 1;
        public const int LENGTH_OF_OPERATION_RESULT_BUF = 1;
        public const int LENGTH_OF_RESPONSE_SAVED_BUF = 1;
        public const int LENGTH_OF_NUMBER_OF_RESPONSE_BUF = 1;
        public const int LENGTH_OF_RESPONSE_PARAMETER_BUF = 4 * 100;

        // whole message buffer
        public byte[] MessageBuf;
        public uint MessageBufLength;
        public uint MessageBufParity;

        // different part buffer
        public byte[] OperationModeBuf;
        public byte[] NumberOfParameterBuf;
        public byte[] ReadWriteOperationBuf;
        public byte[] SavedBuf;
        public byte[] OperationResultBuf;
        public byte[] ParameterBuf;

        // different part variable
        public uint OperationMode;
        public uint NumberOfParameter;
        public uint ReadWriteOperation;
        public uint Saved;
        public uint OperationResult;
        public uint[] Parameter;

        /// <summary>
        /// 
        /// </summary>
        public FFTAICommunicationV2DataSectionResponseModel()
        {
            MessageBuf = new byte[LENGTH_OF_MESSAGE_BUF];
            MessageBufLength = 0;
            MessageBufParity = 0;

            OperationModeBuf = new byte[LENGTH_OF_OPERATION_MODE_BUF];
            NumberOfParameterBuf = new byte[LENGTH_OF_NUMBER_OF_PARAMETER_BUF];
            ReadWriteOperationBuf = new byte[LENGTH_OF_READ_WRITE_OPERATION_BUF];
            SavedBuf = new byte[LENGTH_OF_RESPONSE_SAVED_BUF];
            OperationResultBuf = new byte[LENGTH_OF_OPERATION_RESULT_BUF];
            ParameterBuf = new byte[LENGTH_OF_RESPONSE_PARAMETER_BUF];

            OperationMode = 0;
            NumberOfParameter = 0;
            ReadWriteOperation = 0;
            Saved = 0;
            OperationResult = 0;
            Parameter = new uint[LENGTH_OF_RESPONSE_PARAMETER_BUF / 4];
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
            // get operation mode
            Array.Copy(
                MessageBuf,
                0,
                OperationModeBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            OperationMode =
                (uint)(OperationModeBuf[0] << 24)
                + (uint)(OperationModeBuf[1] << 16)
                + (uint)(OperationModeBuf[2] << 8)
                + (uint)(OperationModeBuf[3] << 0);

            // get number of parameter
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF,
                NumberOfParameterBuf,
                0,
                LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            NumberOfParameter =
                (uint)(NumberOfParameterBuf[0]);

            // get read/write operation
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF,
                ReadWriteOperationBuf,
                0,
                LENGTH_OF_READ_WRITE_OPERATION_BUF);

            ReadWriteOperation =
                (uint)(ReadWriteOperationBuf[0]);

            // get saved buffer
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF,
                SavedBuf,
                0,
                LENGTH_OF_RESPONSE_SAVED_BUF);

            // get operation result
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_RESPONSE_SAVED_BUF,
                OperationResultBuf,
                0,
                LENGTH_OF_OPERATION_RESULT_BUF);

            OperationResult =
                (uint)(OperationResultBuf[0]);

            // get parameter
            Array.Copy(
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_RESPONSE_SAVED_BUF
                    + LENGTH_OF_OPERATION_RESULT_BUF,
                ParameterBuf,
                0,
                NumberOfParameter * 4);

            for (int i = 0; i < NumberOfParameter; i++)
            {
                Parameter[i] =
                    (uint)(ParameterBuf[i * 4 + 0] << 24)
                    + (uint)(ParameterBuf[i * 4 + 1] << 16)
                    + (uint)(ParameterBuf[i * 4 + 2] << 8)
                    + (uint)(ParameterBuf[i * 4 + 3] << 0);
            }

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationModeBuf"></param>
        /// <param name="numberOfParameterBuf"></param>
        /// <param name="readWriteOperationBuf"></param>
        /// <param name="savedBuf"></param>
        /// <param name="operationResultBuf"></param>
        /// <param name="parameterBuf"></param>
        /// <returns></returns>
        public FunctionResult Update(
            byte[] operationModeBuf,
            byte[] numberOfParameterBuf,
            byte[] readWriteOperationBuf,
            byte[] savedBuf,
            byte[] operationResultBuf,
            byte[] parameterBuf)
        {
            // update different part buffer and variables
            Array.Copy(operationModeBuf, OperationModeBuf, LENGTH_OF_OPERATION_MODE_BUF);

            OperationMode =
                (uint)(OperationModeBuf[0] << 24)
                + (uint)(OperationModeBuf[1] << 16)
                + (uint)(OperationModeBuf[2] << 8)
                + (uint)(OperationModeBuf[3] << 0);

            Array.Copy(numberOfParameterBuf, NumberOfParameterBuf, LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            NumberOfParameter =
                (uint)(NumberOfParameterBuf[0]);

            Array.Copy(readWriteOperationBuf, ReadWriteOperationBuf, LENGTH_OF_READ_WRITE_OPERATION_BUF);

            ReadWriteOperation =
                (uint)(ReadWriteOperationBuf[0]);

            Array.Copy(savedBuf, SavedBuf, LENGTH_OF_RESPONSE_SAVED_BUF);

            Array.Copy(operationResultBuf, operationResultBuf, LENGTH_OF_OPERATION_RESULT_BUF);

            OperationResult =
                (uint)(OperationResultBuf[0]);

            Array.Copy(parameterBuf, ParameterBuf, NumberOfParameter * 4);

            for (int i = 0; i < NumberOfParameter; i++)
            {
                Parameter[i] =
                    (uint)(ParameterBuf[i * 4 + 0] << 24)
                    + (uint)(ParameterBuf[i * 4 + 1] << 16)
                    + (uint)(ParameterBuf[i * 4 + 2] << 8)
                    + (uint)(ParameterBuf[i * 4 + 3] << 0);
            }

            // update whole message buffer
            Array.Copy(
                OperationModeBuf,
                0,
                MessageBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            Array.Copy(
                NumberOfParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF,
                LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            Array.Copy(
                ReadWriteOperationBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF,
                LENGTH_OF_READ_WRITE_OPERATION_BUF);

            Array.Copy(
                SavedBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF,
                LENGTH_OF_RESPONSE_SAVED_BUF);

            Array.Copy(
                OperationResultBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_RESPONSE_SAVED_BUF,
                LENGTH_OF_OPERATION_RESULT_BUF);

            Array.Copy(
                ParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_RESPONSE_SAVED_BUF
                    + LENGTH_OF_OPERATION_RESULT_BUF,
                NumberOfParameter * 4);

            MessageBufLength =
                LENGTH_OF_OPERATION_MODE_BUF
                + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                + LENGTH_OF_READ_WRITE_OPERATION_BUF
                + LENGTH_OF_RESPONSE_SAVED_BUF
                + LENGTH_OF_OPERATION_RESULT_BUF
                + NumberOfParameter * 4;

            return FunctionResult.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationMode"></param>
        /// <param name="numberOfParameter"></param>
        /// <param name="readWriteOperation"></param>
        /// <param name="saved"></param>
        /// <param name="operationResult"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public FunctionResult Update(
            uint operationMode,
            uint numberOfParameter,
            uint readWriteOperation,
            uint saved,
            uint operationResult,
            uint[] parameter)
        {
            // update different part buffer and variables
            OperationMode = operationMode;
            NumberOfParameter = numberOfParameter;
            ReadWriteOperation = readWriteOperation;
            Saved = saved;
            OperationResult = operationResult;
            Array.Copy(parameter, Parameter, NumberOfParameter);

            for (int i = 0; i < LENGTH_OF_OPERATION_MODE_BUF; i++)
            {
                OperationModeBuf[i] = (byte)((OperationMode >> (8 * (LENGTH_OF_OPERATION_MODE_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_NUMBER_OF_PARAMETER_BUF; i++)
            {
                NumberOfParameterBuf[i] = (byte)((NumberOfParameter >> (8 * (LENGTH_OF_NUMBER_OF_PARAMETER_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_READ_WRITE_OPERATION_BUF; i++)
            {
                ReadWriteOperationBuf[i] = (byte)((ReadWriteOperation >> (8 * (LENGTH_OF_READ_WRITE_OPERATION_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_RESPONSE_SAVED_BUF; i++)
            {
                SavedBuf[i] = (byte)((Saved >> (8 * (LENGTH_OF_RESPONSE_SAVED_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < LENGTH_OF_OPERATION_RESULT_BUF; i++)
            {
                OperationResultBuf[i] = (byte)((OperationResult >> (8 * (LENGTH_OF_OPERATION_RESULT_BUF - i - 1))) & 0xFF);
            }

            for (int i = 0; i < NumberOfParameter; i++)
            {
                ParameterBuf[i * 4 + 0] = (byte)((Parameter[i] >> 24) & 0xFF);
                ParameterBuf[i * 4 + 1] = (byte)((Parameter[i] >> 16) & 0xFF);
                ParameterBuf[i * 4 + 2] = (byte)((Parameter[i] >> 8) & 0xFF);
                ParameterBuf[i * 4 + 3] = (byte)((Parameter[i] >> 0) & 0xFF);
            }

            // update whole message buffer
            Array.Copy(
                OperationModeBuf,
                0,
                MessageBuf,
                0,
                LENGTH_OF_OPERATION_MODE_BUF);

            Array.Copy(
                NumberOfParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF,
                LENGTH_OF_NUMBER_OF_PARAMETER_BUF);

            Array.Copy(
                ReadWriteOperationBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF,
                LENGTH_OF_READ_WRITE_OPERATION_BUF);

            Array.Copy(
                SavedBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF,
                LENGTH_OF_RESPONSE_SAVED_BUF);

            Array.Copy(
                OperationResultBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_RESPONSE_SAVED_BUF,
                LENGTH_OF_OPERATION_RESULT_BUF);

            Array.Copy(
                ParameterBuf,
                0,
                MessageBuf,
                0
                    + LENGTH_OF_OPERATION_MODE_BUF
                    + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                    + LENGTH_OF_READ_WRITE_OPERATION_BUF
                    + LENGTH_OF_RESPONSE_SAVED_BUF
                    + LENGTH_OF_OPERATION_RESULT_BUF,
                NumberOfParameter * 4);

            MessageBufLength =
                LENGTH_OF_OPERATION_MODE_BUF
                + LENGTH_OF_NUMBER_OF_PARAMETER_BUF
                + LENGTH_OF_READ_WRITE_OPERATION_BUF
                + LENGTH_OF_RESPONSE_SAVED_BUF
                + LENGTH_OF_OPERATION_RESULT_BUF
                + NumberOfParameter * 4;

            return FunctionResult.Success;
        }
    }

    public class FFTAICommunicationV2DataSectionModel
    {
        public FFTAICommunicationV2DataSectionRequestModel RequestModel;
        public FFTAICommunicationV2DataSectionResponseModel ResponseModel;

        // model initilization
        public FFTAICommunicationV2DataSectionModel()
        {
            RequestModel = new FFTAICommunicationV2DataSectionRequestModel();
            ResponseModel = new FFTAICommunicationV2DataSectionResponseModel();
        }
    
    }

}
