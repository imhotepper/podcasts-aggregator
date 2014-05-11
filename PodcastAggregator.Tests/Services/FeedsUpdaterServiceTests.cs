using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PodcastAggregator.DB;
using PodcastAggregator.Services;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System.Linq;

namespace PodcastAggregator.Tests.Services
{
    [TestClass]
    public class FeedsUpdaterServiceTests
    {
        [TestMethod]
        [Ignore]
        public void WillUpdateDB()
        {
            var db = new DataContext();
            var rssReaderService = new RssReaderService();
            var updater = new FeedsUpdaterService(rssReaderService, db);
            db.DeleteShows();

            db.Producers.AsQueryable()
                .ToList()
                .ForEach(p =>
                {
                    updater.Update(p);
                });


            Assert.IsTrue(db.Shows.Count() > 0);

            Console.WriteLine("Shows count: " + db.Shows.Count());
            

            Assert.IsTrue(db.Shows.Count() > 0);

        }
    }
}
