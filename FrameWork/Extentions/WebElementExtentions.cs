using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Pattern;
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
        private By getByFromElement(IWebElementWebElement element)
        {
            By by = null;
            //[[ChromeDriver: chrome on XP (d85e7e220b2ec51b7faf42210816285e)] -> xpath: //input[@title='Search']]
            String[] pathVariables = (element.toString().split("->")[1].replaceFirst("(?s)(.*)\\]", "$1" + "")).split(":");

            String selector = pathVariables[0].Trim();
            String value = pathVariables[1].Trim();

            switch (selector)
            {
                case "id":
                    by = By.Id(value);
                    break;
                case "className":
                    by = By.ClassName(value);
                    break;
                case "tagName":
                    by = By.TagName(value);
                    break;
                case "xpath":
                    by = By.XPath(value);
                    break;
                case "cssSelector":
                    by = By.CssSelector(value);
                    break;
                case "linkText":
                    by = By.LinkText(value);
                    break;
                case "name":
                    by = By.Name(value);
                    break;
                case "partialLinkText":
                    by = By.PartialLinkText(value);
                    break;
                default:
                    throw new IllegalStateException("locator : " + selector + " not found!!!");
            }
            return by;
        }



    }
}
