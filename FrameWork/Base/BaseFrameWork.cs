using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using FrameWork.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FrameWork.Base
{
    public class BaseFrameWork
    {
        private readonly IWebDriver _driver = DriverContext.Driver;

        private void StaticWait(int waitTime)
        {
            Thread.Sleep(waitTime);
        }

        private IWebElement WaitForElement(By by, int timeInSeconds = 30)
        {
            IWebElement element = null;
            try
            {
                if (_driver != null)
                {
                    WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, timeInSeconds))
                    {
                        Timeout = TimeSpan.FromSeconds(timeInSeconds),
                        PollingInterval = TimeSpan.FromMilliseconds(100)
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                        typeof(ElementNotInteractableException),
                        typeof(ElementClickInterceptedException),
                        typeof(ElementNotVisibleException),
                        typeof(StaleElementReferenceException),
                        typeof(ElementNotSelectableException));

                    wait.Until(driver => IsPageReady(driver));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                }

                if (_driver != null) element = _driver.FindElement(by);
            }
            catch (Exception e)
            {
                LogHelper.Write("[Element Not Found] " + by.ToString());
            }
            return element;
        }

        public Boolean IsPageReady(IWebDriver driver, int timeoutSec = 15)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
            return wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public void EnterText(By by, string setValue, int timeToReadyElement = 1, bool isCaptureScreenShot = false)
        {
            try
            {

                IWebElement element = WaitForElement(by);
                if (element.Displayed == false)
                {
                    StaticWait(300);
                }

                if (element.Enabled == false)
                {
                    StaticWait(1000);
                }

                element.SendKeys(setValue);


                ExtentReportsHelper.SetStepStatusWarning("Element :" + by + "  Element Value:" + setValue);
                LogHelper.Write("[Text Entered] in Element: " + by.ToString() + "| Data Entered:" + setValue);
            }
            catch (Exception ex) // Element Not found
            {
                ExtentReportsHelper.SetStepStatusWarning(by + "[" + setValue + ": Not Performed] [Exception: " + ex.ToString() + "]");
                LogHelper.Write("Element Not Found " + by.ToString());
            }
        }


        public void ClickElement(By by, int waitForElementTobeclickable = 30)
        {
            IWebElement element = WaitForElement(by, waitForElementTobeclickable);


            Func<IWebDriver, bool> IsClicked()
            {
                return (Func<IWebDriver, bool>)(driver =>
                {
                    try
                    {
                        if (!element.Displayed || !element.Enabled)
                        {
                            StaticWait(200);
                        }
                        element.Click();
                        return true;

                    }
                    catch
                    {
                        return false;
                    }

                });
            }

            try
            {

                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30))
                {
                    Timeout = TimeSpan.FromSeconds(30),
                    PollingInterval = TimeSpan.FromMilliseconds(150)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                    typeof(ElementNotInteractableException),
                    typeof(ElementClickInterceptedException),
                    typeof(ElementNotVisibleException),
                    typeof(StaleElementReferenceException),
                    typeof(ElementNotSelectableException));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
                wait.Until(IsClicked());

                LogHelper.Write("[Element Clicked] " + by.ToString());
                ExtentReportsHelper.SetStepStatusError(by + "ClickElement");

            }

            catch (Exception ex)
            {
                LogHelper.Write("[Element Not Found ]" + by.ToString());
                throw ex;
            }
        }

    }
}
