using RestApi.Models.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestApi.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error(string type)
        {
            ErrorModel error = new ErrorModel(type);
            return View(error);
        }
    }
}