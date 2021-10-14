using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using OpenQA.Selenium;

namespace FrameWork.Extentions
{
    public static class WebDriverExtensions
    {


        private static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeout)
        {
            Func<T, bool> execute = (arg) =>
            {
                try
                {
                    return condition(arg);
                }
                catch (Exception e)
                {
                    return false;
                }
            };
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < timeout)
            {
                if (execute(obj))
                {
                    break;
                }
            }
        }

        internal static object ExecuteJs(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)DriverContext.Driver).ExecuteScript(script);
        }
    }
}
