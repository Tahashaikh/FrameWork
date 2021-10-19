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
        private static IWebElement ElementIfVisible(IWebElement element)
        {
            return !element.Displayed ? (IWebElement)null : element;
        }

        public static Func<IWebDriver, bool> CheckElementDisplayed(IWebElement element)
        {
            return delegate
            {
                try
                {
                    if(element.Displayed)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            };
        }
        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a page
        /// and visible. Visibility means that the element is not only displayed but
        /// also has a height and width that is greater than 0.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The <see cref="T:OpenQA.Selenium.IWebElement" /> once it is located and visible.</returns>
        public static Func<IWebDriver, IWebElement> CheckElementIsVisible(IWebElement element)
        {
            return (Func<IWebDriver, IWebElement>)(driver =>
            {
                try
                {
                    return ElementIfVisible(element);
                }
                catch (StaleElementReferenceException ex)
                {
                    return (IWebElement)null;
                }
            });
        }


       

       
    }
}
