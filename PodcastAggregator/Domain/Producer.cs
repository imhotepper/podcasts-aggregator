using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace PodcastAggregator.Domain
{
    public class Producer
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required][Url]
        public string Url { get; set; }
        [Required][Url]
        public string FeedUrl { get; set; }
        public bool Active { get; set; }

        public Producer()
        {
            Active = true;
        }
    }
}