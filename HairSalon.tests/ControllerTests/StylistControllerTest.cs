
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistControllerTest
  {

    [TestMethod]
    public void CreateForm_BringsYouToCorrectView()
    {
      //Arrange
      StylistController controller = new StylistController();

      //Act
      ActionResult createFormView = controller.CreateForm();

      //Assert
      Assert.IsInstanceOfType(createFormView, typeof(ViewResult));
    }
    [TestMethod]
    public void Details_BringsYouToCorrectView()
    {
      //Arrange
      StylistController controller = new StylistController();

      //Act
      ActionResult detailsView = controller.Details(0);

      //Assert
      Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
    }

  }
}
