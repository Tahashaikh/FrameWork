using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace Consumer.Pages.Login
{
    public class LoginLocators : BaseLocator
    {
       
        
        [FindsBy(How = How.Name, Using = "user")]
        public IWebElement Username { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Login button']")]
        public IWebElement LoginButton { get; set; }

     
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
