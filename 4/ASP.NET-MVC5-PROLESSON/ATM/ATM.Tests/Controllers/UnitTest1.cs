using System;
using System.Web.Mvc;
using ATM.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATM.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FooActionReturnsAboutView()
        {
            var homeController = new HomeController();
            var result = homeController.Foo() as ViewResult;
            Assert.AreEqual("About", result.ViewName);
        }
    }
}
