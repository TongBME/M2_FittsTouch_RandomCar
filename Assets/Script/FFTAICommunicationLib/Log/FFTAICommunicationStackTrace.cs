using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FFTAICommunicationLib
{
    class FFTAICommunicationStackTrace
    {
        //-------------------------------------------- Variables Definition ------------------------------------------------

        // stack trace
        public StackTrace StackTrace;
        
        //-------------------------------------------- Variables Definition ------------------------------------------------

        //-------------------------------------------- Singleton -----------------------------------------------------------

        private static volatile FFTAICommunicationStackTrace instance;

        private FFTAICommunicationStackTrace()
        {
            StackTrace = new StackTrace(true);
        }

        public static FFTAICommunicationStackTrace Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FFTAICommunicationStackTrace();
                }

                return instance;
            }
        }

        //-------------------------------------------- Singleton -----------------------------------------------------------

        //-------------------------------------------- Function Definition -------------------------------------------------
        

        //-------------------------------------------- Function Definition -------------------------------------------------

    }
}
