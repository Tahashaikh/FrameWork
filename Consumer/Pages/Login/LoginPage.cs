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

        
        public static void Login()
        {
            LoginLocators loginLocators = new LoginLocators();
         /*   WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(30));
            wait.Until(driver => loginLocators.IsClicked(loginLocators.Username2));*/
            EnterText(loginLocators.Username2, "admin", 10);
            EnterText(loginLocators.Password2, "admin", 10);
            ClickElement(loginLocators.LoginButton2, 10);
      
        }
    }
}
