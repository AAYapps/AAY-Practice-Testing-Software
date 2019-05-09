using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAY_Transdumper_v2
{
    static class AppConstants
    {
        public static List<Question> QUESTIONS { get; } = new List<Question>();

        public static string LOCATION
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
            }
        }

        public static string TESTSLOCATION
        {
            get
            {
                return LOCATION + "trandumpertests\\";
            }
        }

        private static string TESTNAME = "";
        public static string TESTLOCATION
        {
            get
            {
                return TESTSLOCATION + TESTNAME + "\\";
            }
            set
            {
                TESTNAME = value;
            }
        }

        public static string IMGLOCATION
        {
            get
            {
                return TESTLOCATION + "IMG\\";
            }
        }

        public static string VIDEOLOCATION
        {
            get
            {
                return TESTLOCATION + "video\\";
            }
        }
    }
}
