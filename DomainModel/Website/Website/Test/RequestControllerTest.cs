using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using System.Web.Mvc;

namespace Website.Test
{
    [TestFixture]
    public class RequestControllerTest
    {
        [Test]
        public void TestRequestControllerCreation()
        {
            Website.Controllers.RequestController controller = new Website.Controllers.RequestController();
            ViewResult result = controller.Create();
            Assert.AreEqual(result.ViewName, "Create");

        }
    }
}