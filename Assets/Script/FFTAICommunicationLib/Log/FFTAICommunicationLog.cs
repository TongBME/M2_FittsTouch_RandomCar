using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationLog
    {
        public FunctionResult WriteLine(string information)
        {
            if (FFTAICommunicationConfig.DEBUG_LOG_ON == true)
            {
                Debug.Log(information);
            }

            return FunctionResult.Success;
        }

        public FunctionResult WriteLine(string information, bool tag)
        {
            if (tag == true)
            {
                //UnityStackTrace.Instance.StackTrace = new System.Diagnostics.StackTrace(true);

                //// Note:
                ////      the frame number here represent the stack depth
                //information = UnityStackTrace.Instance.StackTrace.GetFrame(1).GetFileName().ToString()
                //                + " - "
                //                + UnityStackTrace.Instance.StackTrace.GetFrame(1).GetMethod().ToString()
                //                + " - "
                //                + UnityStackTrace.Instance.StackTrace.GetFrame(1).GetFileLineNumber().ToString()
                //                + " : "
                //                + information;
            }
            else
            {
            }

            if (FFTAICommunicationConfig.DEBUG_LOG_ON == true)
            {
                Debug.Log(information);
            }

            return FunctionResult.Success;
        }

        public FunctionResult WriteLine(string fileName, string methodName, string lineNumber, string information)
        {
            if (FFTAICommunicationConfig.DEBUG_LOG_ON == true)
            {
                Debug.Log(fileName + " - " + methodName + " - " + lineNumber + " : " + information);
            }

            return FunctionResult.Success;
        }
    }
}
