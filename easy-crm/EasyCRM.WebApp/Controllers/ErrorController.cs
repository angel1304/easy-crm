﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace EasyCRM.WebApp.Controllers
{


    public class ErrorController : Controller
    {

        [HttpGet]
        public ViewResult Unknown()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Unknown");
        }



        [HttpGet]
        public ViewResult NotFound(string path)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View("NotFound", path);

        }

    }

}
