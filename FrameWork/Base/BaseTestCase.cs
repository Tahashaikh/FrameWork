using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Model;
using FrameWork.BrowserDriver;
using FrameWork.Config;
using FrameWork.Helper;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace FrameWork.Base
{
    
    public class BaseTestCase : BaseFrameWork

    {

        protected ExtentReportsHelper Extent;


        [OneTimeSetUp]
        public void SetUpReporter()
        {
            
            new DirectoryHelper().CreateLogFolder();
            new LogHelper().CreateLogFile();
            Extent = new ExtentReportsHelper(ConfigInitialization.GetApplicationName(),ConfigInitialization.GetEnvironment());
            LogHelper.Write("Assembly Initialized");

        }

        [SetUp]
        public void StartUpTest()
        {
            ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.ClassName+" / "+TestContext.CurrentContext.Test.Name);
            Browser.OpenBrowser(ConfigInitialization.GetExecutionBrowser());
            Browser.GoToUrl(ConfigInitialization.GetAppUrl()); 
            LogHelper.Write("Test Initialization Start");
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
                throw (e);
                
            }
            finally
            {
                ExtentReportsHelper.Close();
                LogHelper.FlushLogFiles();
                DriverContext.Driver.Close();
            }
        }
        [OneTimeTearDown]
        public void CloseAll()
        {
            LogHelper.Write("Executing One time Tear down");
            try
            {
               if(DriverContext.Driver  != null){

                   DriverContext.Driver.Quit();
                   DriverContext.Driver.Dispose();
               }
            }
            catch (Exception e)
            {
                throw e;
            }
            LogHelper.Write("Assembly CleanUp");
            LogHelper.CloseLogFile();

        }

    }
}
