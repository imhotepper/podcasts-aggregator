using PodcastAggregator.DB;
using PodcastAggregator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using PodcastAggregator.Services;

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
        public ActionResult Create(Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            _db.Producers.Save(producer);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var producer = _db.Producers.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                LoggerService.Exception(new ApplicationException("No producer found for id:" + id));
                return RedirectToAction("Index");
            }
            return View(producer);
        }

        [HttpPost]
        public ActionResult Edit(string id, Producer model)
        {
            if (!ModelState.IsValid) return View(model);
            var producer = _db.Producers.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                LoggerService.Exception(
                    new ApplicationException( 
                        string.Format( "No producer found to update for id:{0} \n with the following data to post: \n {1}", id, model.ToJson())
                        ));
                return RedirectToAction("Index");
            }

            _db.Producers.Save(model);
                return RedirectToAction("Index");
        }


    }




}