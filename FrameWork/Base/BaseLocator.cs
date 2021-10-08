using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.BrowserDriver;
using FrameWork.Helper;
using static OpenQA.Selenium.Support.PageObjects.PageFactory;

namespace FrameWork.Base
{
    public class BaseLocator : BaseFrameWork
    {
        public BaseLocator()
        {
            InitElements(DriverContext.Driver,this );
            LogHelper.Write("PageFactory Initialized");
        }

        public static partial class PageInit
        {
            #region Reg PAGE OBJECT

            private static BaseLocator _baseLocator;
            public static BaseLocator BaseLocator
            {
                get
                {
                    if (_baseLocator == null)
                    {
                        _baseLocator = new BaseLocator();
                    }

                    return _baseLocator;
                }
                set => _baseLocator = value;
            }


            #endregion
        }

    }
}
