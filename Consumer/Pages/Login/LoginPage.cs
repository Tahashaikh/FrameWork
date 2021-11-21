using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Applitools.Selenium;
using FrameWork.Base;
using FrameWork.BrowserDriver;
using FrameWork.Config;
using FrameWork.Extentions;
using FrameWork.Helper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Consumer.Pages.Login
{
   public class LoginPage :Page
    {
       
   

        public static void Login()
        
        {
            LoginLocators loginLocators = new LoginLocators(); 
            EyeHelper.eyes.Check(Target.Window().Fully().WithName("Login Screen"));
            loginLocators.Username2.SendKeys("admin");
            Thread.Sleep(2000);
            loginLocators.Password2.SendKeys("admin");
            Thread.Sleep(2000);
            loginLocators.LoginButton2.Click();
            Thread.Sleep(2000);
            EyeHelper.eyes.Check(Target.Window().Fully().WithName("ReEnterPassword"));

        }


        public static void Login2nd()

        {
            LoginLocators loginLocators = new LoginLocators();
            EyeHelper.eyes.Check(Target.Window().Fully().WithName("Login Screen2"));
            loginLocators.Username2.SendKeys("admin");
            Thread.Sleep(2000);
            loginLocators.Password2.SendKeys("admin");
            Thread.Sleep(2000);
            loginLocators.LoginButton2.Click();
            Thread.Sleep(2000);
             EyeHelper.eyes.Check(Target.Window().Fully().WithName("ReEnterPassword2"));

        }
    }
}
