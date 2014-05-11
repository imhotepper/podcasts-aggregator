using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastAggregator.Domain
{
    public class Show
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Mp3 { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool  Active { get; set; }
        public int PlayCount { get; set; }

        public string ProducerName { get; set; }
        public string ProducerUrl { get; set; }

        public Show()
        {
            Active = true;
            PlayCount = 0;
        }

        internal void RegisterPlay()
        {
            PlayCount += 1;
        }
    }
}