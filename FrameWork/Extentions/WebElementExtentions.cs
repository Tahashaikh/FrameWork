using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using FrameWork.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace FrameWork.Extentions
{
    public class WebElementExtentions
    {

        private IWebElement WaitforElement(IWebElement element, double timeInSeconds, int timeToReadyElement = 0)
        {
            int i = timeToReadyElement + (int)timeInSeconds;
           
            try
            {
                if (timeToReadyElement != 0 && timeToReadyElement.ToString() != null)
                {
                    WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(i));
                    wait.Message = "Element not Found";
                    wait.Timeout = TimeSpan.FromSeconds(i);
                    wait.PollingInterval = TimeSpan.FromMilliseconds(300);
                    
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                        typeof(ElementNotInteractableException),
                        typeof(ElementClickInterceptedException),
                        typeof(ElementNotVisibleException),
                        typeof(StaleElementReferenceException),
                        typeof(ElementNotSelectableException));
               
                    
                    wait.Until(ConditionsHelper.ElementIsVisible(element));
                  

                }

            }
            catch (Exception e)
            {
                LogHelper.Write("[Element Not Found] " + element.Text.ToString());
            }
            return element;
        }
    }
}
