using PodcastAggregator.DB;
using PodcastAggregator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Linq;

namespace PodcastAggregator.Services
{
    public class FeedsUpdaterService
    {
        RssReaderService _rssReader;
        DataContext _db;
        public FeedsUpdaterService(RssReaderService rssReader, DataContext db)
        {
            _rssReader = rssReader;
            _db = db;
        }

        
        public void Update(Producer producer)
        {
            try
            {
                UpdateItems(producer);                
            }
            catch (Exception exp) {
                LoggerService.Exception(exp);
            }
           

        }

        private void UpdateItems(Producer producer)
        {
            var shows = _rssReader.ReadOne(producer.FeedUrl);
            var allLinks = shows.Items.Select(x => x.Link).ToArray();
            var existingLinks = _db.Shows.AsQueryable()
                .Where(x => allLinks.Contains(x.Link))
                .Select(x => x.Link)
                .Distinct()
                .ToArray();

            LoggerService.Info("Received to be updated: " + allLinks);
            shows.Items
                .Where(x => !existingLinks.Contains(x.Link))
                .ToList()
                .ForEach(x =>
                {
                    var show = new Show
                    {
                        Title = x.Title,
                        Description = x.Description,
                        Mp3 = x.Mp3,
                        PublicationDate = x.PublicationDate,
                        Link = x.Link,
                        ProducerName = producer.Name,
                        ProducerUrl = producer.Url
                    };

                    if (x.PublicationDate > DateTime.Now.AddYears(-10)) 
                        _db.Shows.Save(show);
                });
        }
    }
}