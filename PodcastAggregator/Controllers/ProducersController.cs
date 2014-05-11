using PodcastAggregator.DB;
using PodcastAggregator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PodcastAggregator.Controllers
{
    public class ProducersController : Controller
    {
        DataContext _db = new DataContext();
        // GET: Producers
        public ActionResult Index()
        {
            return View(_db.Producers.FindAll());
        }

        public ActionResult Create() { return View(new Producer()); }

        [HttpPost]
        public ActionResult Create(Producer producer) {
            if (!ModelState.IsValid) return View(producer);

            _db.Producers.Save(producer);

            return RedirectToAction("Index");
        }
    }
}