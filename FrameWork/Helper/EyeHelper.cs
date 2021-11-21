using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Applitools;
using Applitools.Selenium;
using FrameWork.Config;
using Configuration = Applitools.Configuration;

namespace FrameWork.Helper
{
   public class EyeHelper
    {
        public static EyesRunner runner;
        public static Eyes eyes;

        public static void BeforeEach(string BatchName)
        {
            
            //Initialize the Runner for your test.
            runner = new ClassicRunner();

            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            eyes = new Eyes(runner);

            // Initialize the eyes configuration.
            Applitools.Selenium.Configuration config = new Applitools.Selenium.Configuration();

            // Add this configuration if your tested page includes fixed elements.
            //config.setStitchMode(StitchMode.CSS);


            // You can get your api key from the Applitools dashboard
            config.SetApiKey(Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY"));

            // set new batch
            config.SetBatch(new BatchInfo(BatchName));

            // set the configuration to eyes
            eyes.SetConfiguration(config);

        }
    
        public static void AfterEach()
        {

            //Wait and collect all test results
            TestResultsSummary allTestResults = runner.GetAllTestResults( false);

            LogHelper.Write(allTestResults.ToString());
        }

    }
}
