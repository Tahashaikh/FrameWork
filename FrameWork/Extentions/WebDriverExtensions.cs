using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using FrameWork.Helper;
using MongoDB.Driver.Core.Operations;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Selenium.WebDriver.WaitExtensions.WaitConditions;

namespace FrameWork.Extentions
{



    /// <summary>
    /// A set of CSS and form based extension methods for <see cref="IWebDriver"/>.
    /// </summary>
    public static class WebDriverExtensions
    {

        public static void EnterText(this IWebDriver driver,IWebElement element,string value, int waitInMilliseconds= 10)
        {   
            try
            {
                driver.StaticWait(1000);
                element.SendKeys(value);
                ExtentReportsHelper.SetStepStatusPass("Enter Text In an Element "+element.ToString()+" Value send is :"+value);
                LogHelper.Write("Enter Text In an Element" + element.TagName + " Value send is :" + value);
            }
            catch (Exception e)
            {
                ExtentReportsHelper.SetStepStatusError("Unable to EnterText in Element "+e);
                LogHelper.Write("Error Unable to EnterText in Element " + e);
                throw;
            }
           
        }

        public static void StaticWait(this IWebDriver driver, int waitInMilliseconds)
        {
           Thread.Sleep(TimeSpan.FromMilliseconds(waitInMilliseconds));
        }
        public static void Click(this IWebDriver driver,IWebElement element,double timeToReadyElementInSeconds)
        {
            try
            {
                driver.StaticWait(1000);
                element.Click();
                ExtentReportsHelper.SetStepStatusPass("Element Clicked" + element.TagName);
                LogHelper.Write("Element Clicked" + element.TagName);
            }
            catch (Exception)
            {
                ExtentReportsHelper.SetStepStatusError("Element not Clicked" + element.TagName);
                LogHelper.Write("Element not Clicked" + element.TagName);
                throw;
            }
        }


        /// <summary>
        /// wait for Elemet for a period of time.
        /// </summary>
        /// <param name="element">Page Factory Element</param>
        /// <param name="timeToReadyElementinSeconds">Amount of time in seconds (Optional)</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        private static IWebElement WaitForElement( IWebElement element, double timeToReadyElementInSeconds = 10)
        {
            try
            {
                   

                WebDriverWait wait = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(timeToReadyElementInSeconds));
                wait.Message = "Element not Found";
                wait.Timeout = TimeSpan.FromSeconds(timeToReadyElementInSeconds);
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);

                wait.IgnoreExceptionTypes(typeof(NoSuchElementException),
                    typeof(ElementNotInteractableException),
                    typeof(ElementClickInterceptedException),
                    typeof(ElementNotVisibleException),
                    typeof(StaleElementReferenceException),
                    typeof(ElementNotSelectableException));

                wait.Until(ConditionsHelper.CheckElementIsVisible(element));


                return element;
            }
            catch (Exception e)
            {
                LogHelper.Write("No such element found" + e);
                return null;
            }
        }

        /// <summary>
        /// Clicks a button that has the given value.
        /// </summary>
        /// <param name="buttonValue">The button's value (input[value=])</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void ClickButtonWithValue(this IWebDriver webdriver, string buttonValue)
        {
            webdriver.FindElement(By.CssSelector("input[value='" + buttonValue + "']")).Click();
        }

        /// <summary>
        /// Clicks a button with the id ending with the provided value.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void ClickButtonWithId(this IWebDriver webdriver, string idEndsWith)
        {
            webdriver.FindElement(By.CssSelector("input[id$='" + idEndsWith + "']")).Click();
        }

        /// <summary>
        /// Selects an item from a drop down box using the given CSS id and the itemIndex.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="itemIndex">A zero-based index that determines which drop down box to target from the CSS selector (assuming
        /// the CSS selector returns more than one drop down box).</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void SelectItemInList(this IWebDriver webdriver, string selector, int itemIndex)
        {
            SelectElement element = new SelectElement(webdriver.FindElement(By.CssSelector(selector)));
            element.SelectByIndex(itemIndex);
        }

        /// <summary>
        /// Selects an item from the nth drop down box (based on the elementIndex argument), using the given CSS id and the itemIndex.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="itemIndex">A zero-based index that determines which drop down box to target from the CSS selector (assuming
        /// the CSS selector returns more than one drop down box).</param>
        /// <param name="elementIndex">The element in the drop down list to select.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void SelectItemInList(this IWebDriver webdriver, string selector, int itemIndex,
            int elementIndex)
        {
            SelectElement element =
                new SelectElement(webdriver.FindElements(By.CssSelector(selector))[elementIndex]);
            element.SelectByIndex(itemIndex);
        }

        /// <summary>
        /// Returns the number of elements that match the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The number of elements found.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static int ElementCount(this IWebDriver webdriver, string selector)
        {
            return webdriver.FindElements(By.CssSelector(selector)).Count;
        }

        /// <summary>
        /// Gets the selected index from a drop down box using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The index of the selected item in the drop down box</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static int SelectedIndex(this IWebDriver webdriver, string selector)
        {
            SelectElement element = new SelectElement(webdriver.FindElement(By.CssSelector(selector)));

            for (int i = 0; i < element.Options.Count; i++)
            {
                if (element.Options[i].Selected)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Gets the selected index from the nth drop down box (based on the elementIndex argument), using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="itemIndex">A zero-based index that determines which drop down box to target from the CSS selector (assuming
        /// the CSS selector returns more than one drop down box).</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The index of the selected item in the drop down box</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static int SelectedIndex(this IWebDriver webdriver, string selector, int itemIndex)
        {
            SelectElement element = new SelectElement(webdriver.FindElements(By.CssSelector(selector))[itemIndex]);

            for (int i = 0; i < element.Options.Count; i++)
            {
                if (element.Options[i].Selected)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Gets the selected value from a drop down box using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The value of the selected item.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static string SelectedItemValue(this IWebDriver webdriver, string selector)
        {
            SelectElement element = (SelectElement)webdriver.FindElement(By.CssSelector(selector));
            return element.SelectedOption.GetAttribute("value");
        }

        /// <summary>
        /// Clicks a link with the text provided. This is case sensitive and searches using an Xpath contains() search.
        /// </summary>
        /// <param name="linkContainsText">The link text to search for.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void ClickLinkWithText(this IWebDriver webdriver, string linkContainsText)
        {
            webdriver.FindElement(By.XPath("//a[contains(text(),'" + linkContainsText + "')]")).Click();
        }

        /// <summary>
        /// Clicks a link with the id ending with the provided value.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void ClickLinkWithId(this IWebDriver webdriver, string idEndsWith)
        {
            webdriver.FindElement(By.CssSelector("a[id$='" + idEndsWith + "']")).Click();
        }

        /// <summary>
        /// Clicks an element using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void Click(this IWebDriver webdriver, string selector)
        {
            webdriver.FindElement(By.CssSelector(selector)).Click();
        }

        /// <summary>
        /// Clicks an element using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="itemIndex">A zero-based index that determines which element to target from the CSS selector (assuming
        /// the CSS selector returns more than one element).</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void Click(this IWebDriver webdriver, string selector, int itemIndex)
        {
            webdriver.FindElements(By.CssSelector(selector))[itemIndex].Click();
        }

        /// <summary>
        /// Gets an input element with the id ending with the provided value.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>An <see cref="IWebElement"/> for the item matched.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static IWebElement InputWithId(this IWebDriver webdriver, string idEndsWith)
        {
            return webdriver.FindElement(By.CssSelector("input[id$='" + idEndsWith + "']"));
        }

        /// <summary>
        /// Gets an element's value with the id ending with the provided value.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The element's value.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static string ElementValueWithId(this IWebDriver webdriver, string idEndsWith)
        {
            return webdriver.FindElement(By.CssSelector("input[id$='" + idEndsWith + "']")).GetAttribute("value");
        }

        /// <summary>
        /// Gets an element's value using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The element's value.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static string ElementValue(this IWebDriver webdriver, string selector)
        {
            return webdriver.FindElement(By.CssSelector(selector)).GetAttribute("value");
        }

        /// <summary>
        /// Gets an element's value using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="itemIndex">A zero-based index that determines which element to target from the CSS selector (assuming
        /// the CSS selector returns more than one element).</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The element's value.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static string ElementValue(this IWebDriver webdriver, string selector, int itemIndex)
        {
            return webdriver.FindElements(By.CssSelector(selector))[itemIndex].GetAttribute("value");
        }

        /// <summary>
        /// Gets an element's text using the given CSS selector.
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="itemIndex">A zero-based index that determines which element to target from the CSS selector (assuming
        /// the CSS selector returns more than one element).</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>The element's text.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static string ElementText(this IWebDriver webdriver, string selector, int itemIndex)
        {
            return webdriver.FindElements(By.CssSelector(selector))[itemIndex].Text;
        }

        /// <summary>
        /// Return true if the checkbox with the id ending with the provided value is checked/selected.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <returns>True if the checkbox is checked.</returns>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static bool IsCheckboxChecked(this IWebDriver webdriver, string idEndsWith)
        {
            return webdriver.FindElement(By.CssSelector("input[id$='" + idEndsWith + "']")).Selected;
        }

        /// <summary>
        /// Clicks the checkbox with the id ending with the provided value.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void ClickCheckbox(this IWebDriver webdriver, string idEndsWith)
        {
            webdriver.FindElement(By.CssSelector("input[id$='" + idEndsWith + "']")).Click();
        }

        /// <summary>
        /// Sets an element's (an input field) value to the provided text by using SendKeys().
        /// </summary>
        /// <param name="value">The text to type.</param>
        /// <param name="element">A <see cref="IWebElement"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void SetValue(this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        /// <summary>
        /// Sets an element's (an input field) value to the provided text, using the given CSS selector and using SendKeys().
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="value">The text to type.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void SetValue(this IWebDriver webdriver, string selector, string value)
        {
            webdriver.FindElement(By.CssSelector(selector)).Clear();
            webdriver.FindElement(By.CssSelector(selector)).SendKeys(value);
        }

        /// <summary>
        /// Sets an element's (an input field) value to the provided text, using the given CSS selector and using SendKeys().
        /// </summary>
        /// <param name="selector">A valid CSS selector.</param>
        /// <param name="value">The text to type.</param>
        /// <param name="itemIndex">A zero-based index that determines which element to target from the CSS selector (assuming
        /// the CSS selector returns more than one element).</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void SetValue(this IWebDriver webdriver, string selector, string value, int itemIndex)
        {
            webdriver.FindElements(By.CssSelector(selector))[itemIndex].Clear();
            webdriver.FindElements(By.CssSelector(selector))[itemIndex].SendKeys(value);
        }

        /// <summary>
        /// Sets the textbox with the given CSS id to the provided value.
        /// </summary>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="value">The text to type.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void FillTextBox(this IWebDriver webdriver, string idEndsWith, string value)
        {
            webdriver.SetValue("input[id$='" + idEndsWith + "']", value);
        }

        /// <summary>
        /// Sets the textarea with the given CSS id to the provided value.
        /// </summary>
        /// <param name="value">The text to set the value to.</param>
        /// <param name="idEndsWith">A CSS id.</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">No element was found.</exception>
        public static void FillTextArea(this IWebDriver webdriver, string idEndsWith, string value)
        {
            webdriver.SetValue("textarea[id$='" + idEndsWith + "']", value);
        }

        /// <summary>
        /// Waits the specified time in second (using a thread sleep)
        /// </summary>
        /// <param name="seconds">The number of seconds to wait (this uses TimeSpan.FromSeconds)</param>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        [Obsolete("Use WaitForElementDisplayed instead, as Wait uses Thread.Sleep")]
        public static void Wait(this IWebDriver webdriver, double seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Waits 2 seconds, which is *usually* the maximum time needed for all Javascript execution to finish on the page.
        /// </summary>
        /// <param name="webdriver">A <see cref="IWebDriver"/> instance.</param>
        [Obsolete("Use WaitForElementDisplayed instead, as WaitForPageLoad uses Thread.Sleep")]
        public static void WaitForPageLoad(this IWebDriver webdriver)
        {
            webdriver.Wait(2);
        }

        /// <summary>
        /// Waits for an elements to be displayed on the page for the time specified.
        /// </summary>
        /// <param name="driver">A <see cref="IWebDriver"/> instance.</param>
        /// <param name="by">The selector to find the element with.</param>
        /// <param name="timeoutInSeconds">The number of seconds to wait</param>
        /// <returns></returns>
        public static IWebElement WaitForElementDisplayed(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            if (wait.Until<bool>(x => x.FindElement(by).Displayed))
            {
                return driver.FindElement(by);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Determines whether the element provided by the selector is displayed or not, waiting a 
        /// certain amount of time for it to be displayed.
        /// </summary>
        /// <param name="driver">A <see cref="IWebDriver"/> instance.</param>
        /// <param name="by">The selector to find the element with.</param>
        /// <param name="timeoutInSeconds">The number of seconds to wait.</param>
        /// <returns>True if the element is displayed, false otherwise.</returns>
        public static bool IsElementDisplayed(this IWebDriver driver, By by, int timeoutInSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until<bool>(x => x.FindElement(by).Displayed);
        }
    }
}
