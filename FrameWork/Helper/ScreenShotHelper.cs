using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FrameWork.Helper
{
    public static class ScreenShotHelper
    {
        public static string ScreenCaptureAsBase64String(this IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            ExtentReportsHelper.SetStepStatusPass("screenShot Captured");
            return screenshot.AsBase64EncodedString;
        }
    }
}
