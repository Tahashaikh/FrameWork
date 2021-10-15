using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace FrameWork.Helper
{
   public class ConditionsHelper
    {

        public static Func<IWebDriver, bool> ElementIsVisible(IWebElement element)
        {
            return delegate
            {
                try
                {
                    return element.Displayed;
                }
                catch (StaleElementReferenceException ex)
                {
                    return false;
                }
            };
        }
    }
}
