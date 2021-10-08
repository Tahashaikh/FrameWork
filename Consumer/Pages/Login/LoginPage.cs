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
        {
            LoginLocators loginLocators = new LoginLocators();

            loginLocators.Username.SendKeys("admin");

            loginLocators.Password.SendKeys("admin");

            loginLocators.LoginButton.Click();
        }
    }
}
