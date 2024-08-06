using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRating;

namespace MyNewProject
{
    public class JsonPolicySource : IPolicySource
    {
        private readonly string _filePath;
        private readonly ILogger _logger;

        public JsonPolicySource(string filePath, ILogger logger)
        {
            _filePath = filePath;
            _logger = logger;
        }

        public Policy GetPolicy()
        {
            _logger.Log($"Reading policy fromm {_filePath}");
            string policyJson = File.ReadAllText(_filePath);
            _logger.Log($"Policey JSON content: {policyJson}");

            var settings = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }
            };
            var policy = JsonConvert.DeserializeObject<Policy>(policyJson, settings);

            if (policy == null)
            {
                throw new InvalidOperationException("Failed to deserialize policy from JSON.");
            }

            _logger.Log($"Deserialized policy. Type: {policy.Type}");
            return policy;
        }
    }
}
