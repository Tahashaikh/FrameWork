using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Helper
{
    public class LogHelper 
    {
        private static StreamWriter _streamWriter;
        public void CreateLogFile()
        {

            if (Directory.Exists(DirectoryHelper.LogFolderDir))
            {
                _streamWriter = File.AppendText(DirectoryHelper.LogFolderDir + "ExecutionLogs.log");
            }
            else
            {
                Directory.CreateDirectory(DirectoryHelper.LogFolderDir);
                _streamWriter = File.AppendText(DirectoryHelper.LogFolderDir + "ExectionLogs.log");
            }

        }

        public static void Write(string logMessage)
        {
            _streamWriter.WriteLine("{0}{1}", DateTime.Now.ToLongTimeString() + " ", DateTime.Now.ToLongDateString() + "" + $"--->{logMessage}");
            //_streamWriter.Flush();
        }

        public static void FlushLogFiles()
        {
            Write("Flushing Log File");
            _streamWriter.Flush();
        }

        public static void CloseLogFile()
        {
            LogHelper.Write("Closing Log File");
            _streamWriter.Close();
        }
    }
}
