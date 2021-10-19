using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FrameWork.Base;
using FrameWork.BrowserDriver;
using FrameWork.Extentions;
using FrameWork.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Consumer.Pages.Login
{
   public class LoginPage :Page
    {
        static LoginLocators loginLocators = new LoginLocators();
        private static IWebDriver driver = DriverContext.Driver;

        public static void Login()
        
        {
            loginLocators.Username2.SendKeys("admin");
            Thread.Sleep(2000);
            loginLocators.Password2.SendKeys("admin");
            Thread.Sleep(2000);
            loginLocators.LoginButton2.Click();
            Thread.Sleep(2000);

           /* driver.WaitforElement(loginLocators.Username2,30).SendKeys("admin");
          driver.WaitforElement(loginLocators.Password2,30).SendKeys("admin");
          driver.WaitforElement(loginLocators.LoginButton2,30).Click();*/

        }


        public static void Login2()

        {
            driver.EnterText(loginLocators.Username2,"admin",5);
            driver.EnterText(loginLocators.Password2,"admin",5);
            driver.Click(loginLocators.LoginButton2,5);

        }
    }
}
