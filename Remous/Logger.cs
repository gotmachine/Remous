using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remous
{



    public static class Logger
    {
        static StreamWriter writer;

        public static void Log(string text)
        {
            if (writer == null)
            {
                string filename = "RemousLog_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".txt";

                writer = new StreamWriter(filename, true);
            }

            writer.WriteLine(DateTime.Now.ToString("HH:mm:ss:ff") + " " + text);
        }

        public static void WriteLog()
        {
            if (writer != null)
                writer.Flush();
        }
    }
}
