using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;



  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public SpecialtyTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=nick_rogers_test;";
    }
    public void Dispose()
    {
      Specialty.DeleteAll();
    }
    [TestMethod]
    public void Save_Test()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Potato");
      testSpecialty.Save();
      //Act
      List<Specialty> testList = new List<Specialty>{testSpecialty};
      List<Specialty> result = Specialty.GetAll();
      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Pizza");
      testSpecialty.Save();
      //Act
      Specialty result = Specialty.Find(testSpecialty.GetSpecialtyId());
      //Assert
      Assert.AreEqual(testSpecialty, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      string  testName = "yolo";
      Specialty testSpecialty = new Specialty(testName);
      testSpecialty.Save();
      string updateName = "wolololo";
      //Act
      testSpecialty.Edit(updateName);
      string result = Specialty.Find(testSpecialty.GetSpecialtyId()).GetSpecialtyName();
      //Assert
      Assert.AreEqual(updateName, result);
    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Specialty testSpecialty = new Specialty("Gyro");
      testSpecialty.Save();
      Specialty newTestSpecialty = new Specialty("Sub");
      newTestSpecialty.Save();

      List<Specialty> afterDeleteList = new List<Specialty>{newTestSpecialty};

      //Act
      Specialty.Find(testSpecialty.GetSpecialtyId()).Delete();
      List<Specialty> result = Specialty.GetAll();
      //Assert
      CollectionAssert.AreEqual(afterDeleteList, result);
    }
    [TestMethod]
    public void GetStylists_Test()
    {
      //Arrange
      Specialty newSpecialty = new Specialty("Sandwich");
      newSpecialty.Save();
      Stylist newStylist = new Stylist("Barista");
      newStylist.Save();
      Stylist newStylist1 = new Stylist("chef");
      newStylist1.Save();

      //Act
      newSpecialty.AddStylist(newStylist);
      newSpecialty.AddStylist(newStylist1);

      List<Stylist> expectedResult = new List<Stylist>{newStylist, newStylist1};
      List<Stylist> result = newSpecialty.GetStylists();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
  }
