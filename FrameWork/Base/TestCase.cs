using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AventStack.ExtentReports.Model;
using FrameWork.BrowserDriver;
using FrameWork.Config;
using FrameWork.Extentions;
using FrameWork.Helper;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace FrameWork.Base
{
    
    public class TestCase :Base

    {

        protected ExtentReportsHelper Extent;


        [OneTimeSetUp]
        public void SetUpReporter()
        {

            try
            {
                DirectoryHelper directoryHelper = new DirectoryHelper(); directoryHelper.CreateLogFolder();
                new LogHelper().CreateLogFile();
                Extent = new ExtentReportsHelper(ConfigInitialization.GetApplicationName(), ConfigInitialization.GetEnvironment());
                LogHelper.Write("Assembly Initialized");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    LogHelper.Write("Exception one Time Setup"+e.Message+"/n"+e.StackTrace);
                }
                throw e;
            }

        }

        [SetUp]
        public void StartUpTest()
        {
            ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.ClassName+" / "+TestContext.CurrentContext.Test.Name);
            LogHelper.Write("Test Created in Extent Report");
            Browser.OpenBrowser(ConfigInitialization.GetExecutionBrowser());
            EyeHelper.BeforeEach(ConfigInitialization.GetBatchName());
            EyeHelper.eyes.Open(DriverContext.Driver, ConfigInitialization.GetApplicationName(), TestContext.CurrentContext.Test.Name);
            Browser.GoToUrl(ConfigInitialization.GetAppUrl()); 
            LogHelper.Write("Test Initialization Start");
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void AfterTest()
        { LogHelper.Write("After Test Execution");
            try
            {
               
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";
                switch (status)
                {
                    case TestStatus.Failed:
                        ExtentReportsHelper.SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>");
                        Extent.AddTestFailureScreenshots(DriverContext.Driver.ScreenCaptureAsBase64String());
                        break;
                    case TestStatus.Skipped:
                        ExtentReportsHelper.SetTestStatusSkipped();
                        break;
                    default:
                        ExtentReportsHelper.SetTestStatusPass();
                        break;
                }
            }
            catch (Exception e)
            {
                LogHelper.Write("TearDown Exception :" + e);
                throw;
                
            }
            finally
            {
                EyeHelper.AfterEach();
                EyeHelper.eyes.CloseAsync();
                ExtentReportsHelper.Close();
                DriverContext.Driver.Close();
            }
        }
        [OneTimeTearDown]
        public void CloseAll()
        {
            LogHelper.Write("Executing One time Tear down");
          
            try
            {
                DriverContext.Driver.Close();
                DriverContext.Driver.Quit();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                
               
                EyeHelper.eyes.AbortIfNotClosed();

                DriverContext.Driver.Dispose();
                LogHelper.FlushLogFiles();
            }
            LogHelper.Write("Assembly CleanUp");
            LogHelper.CloseLogFile();

        }

    }
}
