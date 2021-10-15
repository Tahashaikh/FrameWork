using System;
using System.Threading;
using FrameWork.Base;
using FrameWork.BrowserDriver;
using FrameWork.Extentions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Consumer.Pages.Login
{
   public class LoginPage :Page
    {
        static LoginLocators loginLocators = new LoginLocators();

        public static void Login()
        {
            loginLocators.Username2.SendKeys("admin");

            loginLocators.Password2.SendKeys("admin");

            loginLocators.LoginButton2.Click();

        }
    }
}
