using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using FrameWork.Extentions;
using FrameWork.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace FrameWork.Base
{
    public class BaseFrameWork
    {

        private static void StaticWait(int waitTime)
        {
            Thread.Sleep(waitTime);
        }

        public static void EnterText(IWebElement element, string setValue, int timeToReadyElement = 30 )
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            try
            {

               
                if (element.Displayed == false || element.Enabled == false)
                {
                    StaticWait(500);
                }
                element.SendKeys(setValue);


                ExtentReportsHelper.SetStepStatusWarning("Element :" + element + "  Element Value:" + setValue);
                LogHelper.Write("[Text Entered] in Element: " + element.ToString() + "| Data Entered:" + setValue);
            }
            catch (Exception ex) // Element Not found
            {
                ExtentReportsHelper.SetStepStatusWarning(element + "[" + setValue + ": Not Performed] [Exception: " + ex.ToString() + "]");
                LogHelper.Write("Element Not Found " + element.ToString());
            }
        }

        public static void ClickElement(IWebElement element, int waitForElementToBeClickAble = 30)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            void ClickedCheck()
            {
                var elementClicked = true;
                while (elementClicked)
                {
                    try
                    {
                        if (element.Enabled && element.Displayed)
                        {
                            element.Click();
                            elementClicked = false;
                        }
                    }
                    catch (ElementClickInterceptedException)
                    {
                        LogHelper.Write("ClickedCheck : unable to click on element ElementClickInterceptedException");
                        elementClicked = true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        LogHelper.Write("ClickedCheck : unable to click on element StaleElementException");
                        elementClicked = true;
                    }
                }
            }
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(30));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
                ClickedCheck();
                LogHelper.Write("[Element Clicked] " + element.ToString());
                ExtentReportsHelper.SetStepStatusError(element + "ClickElement");
            }

            catch (Exception ex)
            {
                LogHelper.Write("[Element Not Clicked ]" + element +"Exception :" +ex); 
                throw ex;
            }
        }


        public static void SelectDropDownList(IWebElement element, string value)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByText(value);
        }

        public static IList<IWebElement> GetAllSelectedOptions(IWebElement element)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions;
        }

        public static string GetFirstSelectedDropDown(IWebElement element)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions.First().ToString();
        }

        public static string GetLinkText(IWebElement element)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            return element.Text;
        }

        public static void AssertElementPresence(IWebElement element)
        {
            if (!(element.Displayed))
            {
                throw new Exception(string.Format("Element Not Present exception"));
            }
        }


        public static void Hover(IWebElement element)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            Actions actions = new Actions(DriverContext.Driver);
            actions.MoveToElement(element).Perform();
        }

        public static bool IsElementPresent(IWebElement element)
        {
            WebDriverExtensions.WaitForPageLoaded(DriverContext.Driver);
            bool result = false;
            try
            {
                if (element.Displayed)
                    return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            return result;
        }




    }
}
