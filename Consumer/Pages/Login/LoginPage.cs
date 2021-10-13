using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.Base;

namespace Consumer.Pages.Login
{
    class LoginPage : BasePage
    {

        
        public static void Login()
        {/*
            EnterText(LoginLocators.Username,"admin");
            EnterText(LoginLocators.Password,"admin");
            ClickElement(LoginLocators.Username);
*/
                 LoginLocators loginLocators = new LoginLocators();
     
                 loginLocators.Username2.SendKeys("admin");
     
                 loginLocators.Password2.SendKeys("admin");

                 loginLocators.LoginButton2.Click();

        }
    }
}
