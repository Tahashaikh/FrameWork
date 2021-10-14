using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FrameWork.Helper
{
    public class ExtentReportsHelper 
    {
        public static ExtentReports Extent { get; set; }
        public static ExtentHtmlReporter HtmlReporter { get; set; }
        public static ExtentTest test { get; set; }
   
        public ExtentReportsHelper(string applicationName = null,string enviroment = null)
        {
            try
            {
                string reportPath = DirectoryHelper.LogFolderDir + @"ExtentReport/";
                HtmlReporter = new ExtentHtmlReporter(reportPath);
                HtmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
                Extent = new ExtentReports();
                Extent.AttachReporter(HtmlReporter);
                Extent.AddSystemInfo("Application Under Test", applicationName);
                Extent.AddSystemInfo("Environment", enviroment);
                Extent.AddSystemInfo("Machine", Environment.MachineName);
                Extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
                LogHelper.Write("Extent Report Initialize successful");

            }
            catch (Exception e)
            {
                LogHelper.Write("Exception in ExtentReportHelper :"+e);
                throw;
            }
      
        }
       

       
        public static void CreateTest(string testName)
        {
            if (GetTestCategory() != null)
            {
                test = Extent.CreateTest(testName).AssignCategory(GetTestCategory());
            }
            else
            {
                test = Extent.CreateTest(testName);
            }
           
        }

        public static void SetStepStatusPass(string stepDescription)
        {
            test.Log(Status.Pass, stepDescription);
        }

        public static void SetStepStatusWarning(string stepDescription)
        {
            test.Log(Status.Warning, stepDescription);
        }

        public static void SetStepStatusError(string stepDescription)
        {
            test.Log(Status.Error, stepDescription);
        }

        public static void SetTestStatusPass()
        {
            test.Pass("Test Executed Successfully!");
        }

        public static void SetTestStatusFail(string message = null)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }

            test.Fail(printMessage);
        }

        public void AddTestFailureScreenshots(string base64ScreenCapture)
        {
            test.AddScreenCaptureFromBase64String(base64ScreenCapture, "Error ScreenShot:");
        }

        public static void SetTestStatusSkipped()
        {
            test.Skip("Test skipped!");
        }

        public static void Close()
        {
            Extent.Flush();
            
        }
        private static String GetTestCategory()
        {
            string category = null;
            var cats = TestContext.CurrentContext.Test?.Properties["Category"];
            foreach (var cat in cats)
            {
                category = category + "" + cat;
            }

            return category;
        }
    
    }
}
