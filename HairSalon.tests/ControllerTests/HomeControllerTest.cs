using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Controllers;

namespace HairSalon.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
      [TestMethod]
        public void LinksGoToCorrectView()
        {
          //Arrange
          HomeController controller = new HomeController();

          //Act
          ActionResult indexView = controller.Index();

          //Assert
          Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
    }
}
