using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using FrameWork.Extentions;
using FrameWork.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using static OpenQA.Selenium.Support.PageObjects.PageFactory;

namespace FrameWork.Base
{
    public class Locator : Base
    {


        public Locator()
        {
            InitElements(DriverContext.Driver,this);
            Thread.Sleep(2000);
            LogHelper.Write("PageFactory Initialized");
        }
        public static partial class PageInit
        {
            #region Reg PAGE OBJECT

            private static Locator _baseLocator;
            public static Locator BaseLocator
            {
                get
                {
                    if (_baseLocator == null)
                    {
                        _baseLocator = new Locator();
                    }

                    return _baseLocator;
                }
                set => _baseLocator = value;
            }


            #endregion
        }
    }
}
