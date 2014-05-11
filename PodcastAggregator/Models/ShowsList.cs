using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PodcastAggregator.Domain;
using PagedList;


namespace PodcastAggregator.Models
{
    public class ShowsList
    {
        public IPagedList<Show> Shows { get; set; }

        public string Search{ get; set; }
    }
}