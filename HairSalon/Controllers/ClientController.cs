using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
       
        
    [HttpGet("/clients")]
    public ActionResult CreateForm()
    {

      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create()
    {
      Client newClient = new Client (Request.Form["newClient"]);
      newClient.Save();
      List<Client> allClient = Client.GetAll();
      return View("Index", allClient);
    }
        }
    }

