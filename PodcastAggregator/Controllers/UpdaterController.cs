using PodcastAggregator.DB;
using PodcastAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


namespace PodcastAggregator.Controllers
{
    public class UpdaterController : Controller
    {
        // GET: Updater
        public ActionResult Run()
        {
            var db = new DataContext();
            var rssReaderService = new RssReaderService();
            var updater = new FeedsUpdaterService(rssReaderService, db);
            db.DeleteShows();

            db.ActiveProducers()
                .AsQueryable()
                .ToList()
                .ForEach(p =>
                {
                    updater.Update(p);
                });


            return RedirectToAction("index","home");
        }
    }
}