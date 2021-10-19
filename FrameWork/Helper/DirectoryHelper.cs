using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Helper
{
    public class DirectoryHelper
    {
        private static readonly string CurrentDateTime = $"{DateTime.Now:yyMMdd_hhmmss}";


        public static string LogFolderDir { get; private set; }
        public static string CurrentFolder { get; private set; }


        public void CreateLogFolder()
        {


            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            DirectoryInfo directoryInfo = System.IO.Directory.GetParent(path).Parent.Parent.Parent.Parent;
            string dir = directoryInfo.FullName;
            CurrentFolder = CurrentDateTime;
            string directory = dir + @"\ExecutionLogs\" + CurrentFolder + @"\";
            LogFolderDir = directory;
            Directory.CreateDirectory(LogFolderDir);
        }

    }
}
