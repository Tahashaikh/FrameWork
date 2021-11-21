using System;
using System.Diagnostics;
using OpenQA.Selenium;
using FrameWork.Base;
using FrameWork.BrowserDriver;
using NUnit.Framework;
using static FrameWork.Extentions.WebElementExtentions;
    
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace Consumer.Pages.Login
{
    public class LoginLocators : Locator
    {


        //[FindsBy(How = How.Name, Using = "user1", Priority = 0)]
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='email or username']", Priority = 1)]
        public IWebElement Username2;

        [FindsBy(How = How.Name, Using = "password", Priority = 0)] [FindsBy(How = How.Id, Using = "Tst", Priority = 1)]
        public IWebElement Password2;

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Login button']")]
        public IWebElement LoginButton2;









    }















































    public static partial class Consumer
    {
        #region Reg PAGE OBJECT

        private static LoginLocators loginLocators;
        public static LoginLocators LoginLocators
        {
            get
            {
                if (loginLocators == null)
                {
                    loginLocators = new LoginLocators();
                }

                return loginLocators;
            }
            set
            {
                loginLocators = value;
            }
        }


        #endregion
    }
}
