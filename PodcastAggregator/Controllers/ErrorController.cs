using PodcastAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PodcastAggregator.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string aspxerrorpath)
        {
            LoggerService.Exception(new ApplicationException("The following path was not found: " + aspxerrorpath));
            return View("Error.cshtml");
        }
    }
}