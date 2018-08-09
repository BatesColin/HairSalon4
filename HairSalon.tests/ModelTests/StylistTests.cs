using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=nick_rogers_test;";
    }
    //create
    [TestMethod]
   public void Save_Test()
   {
     //Arrange
     Stylist testStylist = new Stylist("Sally");
     testStylist.Save();
     //Act
     List<Stylist> testList = new List<Stylist>{testStylist};
     List<Stylist> result = Stylist.GetAll();
     //Assert
     Console.WriteLine(testList.Count);
     Console.WriteLine(result.Count);
     CollectionAssert.AreEqual(testList, result);
   }

   [TestMethod]
   public void Save_DatabaseAssignsIdToStylist_Id()
   {
     //Arrange
     Stylist testStylist = new Stylist("John");
     testStylist.Save();

     //Act
     Stylist savedStylist = Stylist.GetAll()[0];

     int result = savedStylist.GetId();
     int testId = testStylist.GetId();

     //Assert
     Assert.AreEqual(testId, result);
     }

     [TestMethod]
   public void Find_FindsStylistInDatabase_Stylist()
   {
     //Arrange
     Stylist testStylist = new Stylist("Randy");
     testStylist.Save();

     //Act
     Stylist foundStylist = Stylist.Find(testStylist.GetId());

     //Assert
     Assert.AreEqual(testStylist, foundStylist);
   }
   [TestMethod]
   public void Delete_DeletesStylistAssociationsFromDatabase_StylistList()
   {
     //Arrange
     Specialty testSpecialty = new Specialty("Mens Hair");
     testSpecialty.Save();

     string testName = "test";
     Stylist testStylist = new Stylist(testName);
     testStylist.Save();

     //Act
     testStylist.AddSpecialty(testSpecialty);
     testStylist.Delete();

     List<Stylist> resultSpecialtyCategories = testSpecialty.GetCategories();
     List<Stylist> testSpecialtyCategories = new List<Stylist> {};

     //Assert
     CollectionAssert.AreEqual(testSpecialtyCategories, resultSpecialtyCategories);
   }


}
}
