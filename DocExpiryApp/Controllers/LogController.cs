using System;
using System.IO;
using System.Text;

namespace DocExpiryApp.Controllers
{
    public class LogController
    {
        private static LogController instance = new LogController();
        private LogController(){}

        public static void Log(object text, string type)
        {
            Console.WriteLine("{0,-10}: {1}",type,text);
        }

        public static void Information(object text) { Log(text,"Information"); }
        public static void Error(object text)       { Log(text,"Error");       }
        public static void Warning(object text)     { Log(text,"Warning");     }
    }
}