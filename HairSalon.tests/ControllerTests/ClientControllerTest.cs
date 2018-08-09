
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientControllerTest
  {
    [TestMethod]
    public void Index_ReturnsCorrectView()
    {
      //Arrange
      ClientController controller = new ClientController();

      //Act
      ActionResult indexView = controller.Index();

      //Assert
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }

    [TestMethod]
    public void ClientList_GoesToCorrectPage()
    {
      //Arrange
      ClientController controller = new ClientController();

      //Act
      ActionResult createFormView = controller.CreateForm();

      //Assert
      Assert.IsInstanceOfType(createFormView, typeof(ViewResult));
    }

  }
}
