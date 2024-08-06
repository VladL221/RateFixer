using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRating;

namespace MyNewProject
{
    public class TravelPolicyRatingStrategy : IRatingStrategy
    {
        private readonly ILogger _logger;

        public TravelPolicyRatingStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public decimal Rate(Policy policy)
        {
            _logger.Log("Rating TRAVEL policy...");
            _logger.Log("Validating policy.");

            if (policy.Days <= 0)
            {
                _logger.Log("Travel policy must specify Days.");
                return 0;
            }
            if (policy.Days > 180)
            {
                _logger.Log("Travel policy cannot be more than 180 Days.");
                return 0;
            }
            if (string.IsNullOrEmpty(policy.Country))
            {
                _logger.Log("Travel policy must specify country.");
                return 0;
            }

            decimal rating = policy.Days * 2.5m;
            if (policy.Country == "Italy")
            {
                rating *= 3;
            }
            return rating;
        }
    }
}
