using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=nick_rogers_test;";
    }
    public void Dispose()
    {
      Client.DeleteAll();
    }
    [TestMethod]
    public void Save_Test()
    {
      //Arrange
      Client testClient = new Client("Scorpion", 2);
      testClient.Save();
      //Act
      List<Client> testList = new List<Client>{testClient};
      List<Client> result = Client.GetAll();
      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Client testClient = new Client("SubZero", 3);
      testClient.Save();
      //Act
      Client result = Client.Find(testClient.GetClientId());
      //Assert
      Assert.AreEqual(testClient, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      Client testClient = new Client("Kano");
      testClient.Save();
      Client oldClient = new Client("Sonya", testClient.GetClientId());
      //Act
      testClient.Edit("Smoke");

      Client result = Client.Find(testClient.GetClientId());
      //Assert

      Assert.AreEqual(testClient, result);
    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Client testClient = new Client("Raiden", 3);
      testClient.Save();
      Client newTestClient = new Client("Luke", 1);
      newTestClient.Save();
      List<Client> beforeDeleteList = new List<Client>{testClient, newTestClient}; //Use to make test fail
      List<Client> afterDeleteList = new List<Client>{newTestClient}; //Use to make test pass

      //Act
      Client.Find(testClient.GetClientId()).Delete();
      List<Client> result = Client.GetAll();
      //Assert
      CollectionAssert.AreEqual(afterDeleteList, result);
    }
  }
}
