
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyControllerTest
  {

    [TestMethod]
    public void SpecialtyList_Test()
    {
      //Arrange
      SpecialtyController controller = new SpecialtyController();
      IActionResult actionResult = controller.Index();
      ViewResult indexView = controller.Index() as ViewResult;

      //Act
      var result = indexView.ViewData.Model;

      //Assert
      Assert.IsInstanceOfType(result, typeof(List<Specialty>));
    }
    [TestMethod]
    public void CreateForm_Test()
    {
      //Arrange
      SpecialtyController controller = new SpecialtyController();

      //Act
      ActionResult createFormView = controller.CreateForm();

      //Assert
      Assert.IsInstanceOfType(createFormView, typeof(ViewResult));
    }
    [TestMethod]
    public void Details_LinksToCorrect_View()
    {
      //Arrange
      SpecialtyController controller = new SpecialtyController();

      //Act
      ActionResult detailsView = controller.Details(0);

      //Assert
      Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
    }
    [TestMethod]
    public void Details_ReturnsWhatItShould()
    {
      //Arrange
      SpecialtyController controller = new SpecialtyController();
      IActionResult actionResult = controller.Details(0);
      ViewResult detailsView = controller.Details(0) as ViewResult;

      //Act
      var result = detailsView.ViewData.Model;

      //Assert
      Assert.IsInstanceOfType(result, typeof(List<Client>));
    }
  }
}
