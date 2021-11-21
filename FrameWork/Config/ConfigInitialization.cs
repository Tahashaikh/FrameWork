using System;
using System.Collections.Generic;
using System.Configuration;
using FrameWork.Helper;

namespace FrameWork.Config
{
    public class ConfigInitialization
    {
        public static string GetAppUrl()
        {
            LogHelper.Write("Getting Application URL From Config");
            return ConfigurationManager.AppSettings["ApplicationURL"];
        }
        public static string GetExecutionBrowser()
        {
            LogHelper.Write("Getting Execution browser From Config");
            return ConfigurationManager.AppSettings["Browser"];
        }
        public static string GetApplicationName()
        {
            LogHelper.Write("Getting Application Name From Config");
            return ConfigurationManager.AppSettings["Application Name"];
        }
        public static string GetEnvironment()
        {
            LogHelper.Write("Getting Environment Type From Config");
            return ConfigurationManager.AppSettings["Environment"];
        }
        public static string ImplicitWaitValue()
        {
            LogHelper.Write("Getting Environment Type From Config");
            return ConfigurationManager.AppSettings["ImplicitWaitAndPageLoad_inSeconds"];
        }
        public static string GetBatchName()
        {
            LogHelper.Write("Getting BatchName");
            return ConfigurationManager.AppSettings["EyeBatchName"];
        }

    }
}

