using Argotic.Syndication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastAggregator.Services
{
    public class RssSource { public string FeedUrl { get; set; } public List<RssSourceItem> Items = new List<RssSourceItem>(); }
    public class RssSourceItem {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Mp3 { get; set; }
        public DateTime PublicationDate { get; set; }
    }

    public class RssReaderService
    {
        public RssSource ReadOne(string feedUrl)
        {
            var result = new RssSource { FeedUrl =  feedUrl};
            var feed =  new RssFeed();
            feed.Load(new Uri(feedUrl), null);
            feed.Channel.Items
                .ToList()
                .ForEach(x => {
                    if (x.Enclosures != null && x.Enclosures.Any(m => m.ContentType.ToLower().Contains("mp3"))) {
                        var item = new RssSourceItem {
                            Title =  x.Title,
                            Description = x.Description,
                            Link =  x.Link.ToString(),
                            Mp3 = x.Enclosures.First(mp3 => mp3.ContentType.ToLower().Contains("mp3")).Url.ToString()
                        };

                        item.PublicationDate = x.PublicationDate;
                        result.Items.Add(item);
                    }
                });

            return result;
        }
    }
}