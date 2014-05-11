using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PodcastAggregator.Services;

namespace PodcastAggregator.Tests.Services
{
    [TestClass]
    [Ignore]
    public class RssReaderTests
    {
        [TestMethod]
        public void CanReadFeed()
        {

            var rssService = new RssReaderService();

            var rss = rssService.ReadOne("http://feeds.feedburner.com/HanselminutesCompleteMP3?format=xml");

            Assert.IsNotNull(rss);

            Assert.IsTrue(rss.Items.Count > 0);
            Assert.IsTrue(! string.IsNullOrWhiteSpace( rss.Items[0].Mp3));

        }
    }
}
