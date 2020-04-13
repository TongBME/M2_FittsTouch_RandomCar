using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFTAICommunicationLib
{
    public class FFTAICommunicationConfig
    {
        public static bool DEBUG_LOG_ON = true;
        public static bool DEBUG_LOG_RECEIVE_MESSAGE = false;
        public static bool DEBUG_LOG_SEND_MESSAGE = false;
        public static bool DEBUG_LOG_SUCCESS_PARSE_MESSAGE = false;

        public static int SLEEP_TIME_FOR_CREATING_CHILDREN_THREAD = 300;
    }
}
