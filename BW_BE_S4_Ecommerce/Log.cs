using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BW_BE_S4_Ecommerce
{
    public class Log
    {
        public static bool _admin = false;
        
        static Log()
        {
            log = false;
        }

        public static bool log { get; set; }
    }
}