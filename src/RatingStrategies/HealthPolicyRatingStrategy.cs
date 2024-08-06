using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewProject.src.Core.Interfaces;
using MyNewProject.src.Core.Models;

namespace MyNewProject.src.RatingStrategies
{
    public class HealthPolicyRatingStrategy : IRatingStrategy
    {
        private readonly ILogger _logger;

        public HealthPolicyRatingStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public decimal Rate(Policy policy)
        {
            _logger.Log("Rating HEALTH policy...");
            _logger.Log("Validating policy.");

            if (string.IsNullOrEmpty(policy.Gender))
            {
                _logger.Log("Health policy must specify Gender");
                return 0;
            }

            if (policy.Gender == "Male")
            {
                return policy.Deductible < 500 ? 1000m : 900m;
            }
            else
            {
                return policy.Deductible < 800 ? 1100m : 1000m;
            }
        }

    }
}
