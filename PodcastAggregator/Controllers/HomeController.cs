using PodcastAggregator.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using PodcastAggregator.Services;
using PagedList;
using PodcastAggregator.Models;

namespace PodcastAggregator.Controllers
{
    public class HomeController : Controller
    {
        DataContext _db = new DataContext();

        public ActionResult Index(string search, int? page = 1)
        {
            var model = new ShowsList { 
                Search = search ,
                Shows =  _db.ActiveShows(search).ToPagedList(page.Value, 20)
            };
            return View( model);
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("~/{id:length(24)}")]
        public ActionResult Show(string id) {
            var show = _db.Shows.AsQueryable().FirstOrDefault(x => x.Id == id);

            if (show == null) { 
                LoggerService.Info(string.Format( "Show with id: {0} was not found!", id));
                return RedirectToAction("Index");
            }

            show.RegisterPlay();
            _db.Shows.Save(show);

            return View(show);
        }
    }
}