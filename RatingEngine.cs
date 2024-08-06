using MyNewProject;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;

namespace TestRating
{
    /// <summary>D
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        private readonly IPolicySource _policySource;
        private readonly ILogger _logger;
        private readonly Dictionary<PolicyType, IRatingStrategy> _ratingStrategies;

        public decimal Rating { get; private set; }

        public RatingEngine(IPolicySource policySource, ILogger logger, Dictionary<PolicyType, IRatingStrategy> ratingStrategies)
        {
            _policySource = policySource;
            _logger = logger;
            _ratingStrategies = ratingStrategies;
        }

        public void Rate()
        {
            _logger.Log("Starting rate.");
            _logger.Log("Loading policy.");

            try
            {
                var policy = _policySource.GetPolicy();
                _logger.Log($"Policy loaded. Type: {policy.Type}");

                if (!_ratingStrategies.TryGetValue(policy.Type, out var ratingStrategy))
                {
                    _logger.Log($"Unknown policy type: {policy.Type}");
                    return;
                }

                Rating = ratingStrategy.Rate(policy);
                _logger.Log($"Rating calculated: {Rating}");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error during rating: {ex.Message}");
                _logger.Log($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}
