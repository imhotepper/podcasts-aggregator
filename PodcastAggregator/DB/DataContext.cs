using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PodcastAggregator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace PodcastAggregator.DB
{
    public class DataContext
    {
        public MongoDatabase Database;

        public MongoCollection<Producer> Producers { get { return Database.GetCollection<Producer>("producers"); } }

        public MongoCollection<Show> Shows { get { return Database.GetCollection<Show>("shows"); } }

        public DataContext()
        {
            var connectionString = WebConfigurationManager.AppSettings["MongoDBConnection"];
            connectionString = WebConfigurationManager.ConnectionStrings["MongoDBConnection"].ConnectionString;
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            Database = server.GetDatabase(WebConfigurationManager.AppSettings["MongoDBDatabase"]);
           
        }


        public IEnumerable<Producer> ActiveProducers
        {
            get
            {
                return Producers.FindAll().Where(x => x.Active == true).OrderBy(x => x.Name);

            }
        }

        public void DeleteShows()
        {
            this.Shows.RemoveAll();
        }

        internal IQueryable<Show> ActiveShows(string search)
        {
            var shows = Shows.AsQueryable()
                        .Where(x => x.Active);

            if (!string.IsNullOrWhiteSpace(search))
                shows = shows.Where(x => x.Title.ToLower().Contains(search.ToLower()) || x.Description.ToLower().Contains(search.ToLower())).AsQueryable();

            return shows.OrderByDescending(x => x.PublicationDate);

        }
    }
}