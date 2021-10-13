using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.Base;
using MongoDB.Bson.Serialization.Serializers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace Consumer.Pages.Login
{
    public class LoginLocators : BaseLocator
    {
       



        public By Username = By.Name("user");
        public By Password = By.Name("password");
        public By LoginButton = By.XPath("//button[@aria-label='Login button']");
        // public static LoginLocators _loginLocators = new LoginLocators();


        [FindsBy(How = How.Name, Using = "password", Priority = 0)]
        [FindsBy(How = How.Id, Using = "Tst", Priority = 1)]
        public IWebElement Username2 { get; set; }

        [FindsBy(How = How.Name, Using = "password", Priority = 0)] 
        [FindsBy(How = How.Id, Using = "Tst", Priority = 1)]
        public IWebElement Password2 { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@aria-label='Login button']")]
        public IWebElement LoginButton2 { get; set; }
        
     
    }
/*    public static partial class Consumer
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
    }*/
}
