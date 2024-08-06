using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNewProject.src.Core.Interfaces;
using MyNewProject.src.Core.Models;

namespace MyNewProject.src.Core.Strategies.RatingStrategies
{
    public class LifePolicyRatingStrategy : IRatingStrategy
    {
        private readonly ILogger _logger;

        public LifePolicyRatingStrategy(ILogger logger)
        {
            _logger = logger;
        }

        public decimal Rate(Policy policy)
        {
            _logger.Log("Rating life policy...");
            _logger.Log("Validating policy.");

            if (policy.DateOfBirth == DateTime.MinValue)
            {
                _logger.Log("Life policy must include Date of Birth.");
                return 0;
            }
            if (policy.DateOfBirth < DateTime.Today.AddYears(-100))
            {
                _logger.Log("Max eligible age for coverage is 100 years.");
                return 0;
            }
            if (policy.Amount == 0)
            {
                _logger.Log("Life policy must include an Amount.");
                return 0;
            }

            int age = DateTime.Today.Year - policy.DateOfBirth.Year;
            if (policy.DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < policy.DateOfBirth.Day ||
                DateTime.Today.Month < policy.DateOfBirth.Month)
            {
                age--;
            }
            decimal baseRate = policy.Amount * age / 200;
            return policy.IsSmoker ? baseRate * 2 : baseRate;
        }
    }
}
