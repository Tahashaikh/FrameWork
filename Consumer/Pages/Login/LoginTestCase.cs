using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FrameWork.Base;
using FrameWork.Helper;
using NUnit.Framework;

namespace Consumer.Pages.Login
{
    [TestFixture]
    public class LoginTestCase : TestCase
    {   



        [Test, Category("First")]
        public void LaunchAndLogin()
        {
            LoginPage.Login();
        }


    }
}
