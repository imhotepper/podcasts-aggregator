using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PodcastAggregator.Services
{
    public class LoggerService
    {
        static Logger _logger = LogManager.GetLogger("PodcastsAggregator");
        public static void Info(string message) {
            _logger.Info(message);
        }

        public static void Exception(Exception exp)
        {
            _logger.Error(exp);
        }

    }
}