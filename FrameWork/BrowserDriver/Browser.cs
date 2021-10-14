using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.Base;
using FrameWork.Config;
using FrameWork.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace FrameWork.BrowserDriver
{
    public class Browser
    {
        private readonly IWebDriver _driver;
        public Browser(IWebDriver driver)
        {
            _driver = driver;
        }

        //   public static BrowserType browserType { get; set; }
        public static void GoToUrl(string url)
        {
            LogHelper.Write("Opening URL in Browser");
            try
            {
                DriverContext.Driver.Url = url;
            }
            catch (Exception e)
            {
                LogHelper.Write("Exception: Unable to Open URL" + e);
                throw;
            }

        }
        public static void OpenBrowser(String BrowserType)
        {
            void BrowserStartUpSetup()
            {
                try
                {
                    LogHelper.Write("Preparing Browser to Run");
                    DriverContext.Driver.Manage().Window.Maximize();
                    DriverContext.Driver.Manage().Cookies.DeleteAllCookies();
                    DriverContext.Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    DriverContext.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(30);
                    LogHelper.Write("Browser Window Maximized");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            switch (BrowserType)
            {
                case "Firefox":
                    try
                    {
                        DriverContext.Driver = new FirefoxDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        LogHelper.Write("Browser Initialize Success " + BrowserType);
                    }
                    catch (Exception e)
                    {
                        LogHelper.Write("Exception: Unable to Load Firefox Browser  " + e);
                        throw;
                    }

                    BrowserStartUpSetup();
                    break;
                case "Chrome":
                    try
                    {
                        DriverContext.Driver = new ChromeDriver();
                        DriverContext.Browser = new Browser(DriverContext.Driver);
                        LogHelper.Write("Browser Initialize Success " + BrowserType);
                    }
                    catch (Exception e)
                    {
                        LogHelper.Write("Exception: Unable to Load Chrome Browser  " + e);
                        throw;
                    }

                    BrowserStartUpSetup();
                    break;
                default:
                    break;
            }


        }

    }
}
