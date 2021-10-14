using System;
using System.Diagnostics;
using OpenQA.Selenium;
using FrameWork.Base;
using FrameWork.BrowserDriver;
using static FrameWork.Extentions.WebElementExtentions;
    
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;


namespace Consumer.Pages.Login
{
    public class LoginLocators : Locator
    {

    
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
