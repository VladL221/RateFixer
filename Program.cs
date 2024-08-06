using Microsoft.Extensions.DependencyInjection;
using MyNewProject.src.Core;
using MyNewProject.src.Core.Interfaces;
using MyNewProject.src.Core.Models;
using MyNewProject.src.Core.Strategies.RatingStrategies;
using MyNewProject.src.Infrastructure.Logging;
using MyNewProject.src.Infrastructure.PolicySources;
using System;

namespace TestRating
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("Insurance Rating System Starting...");

            // Setup logger and policy source for easier loggin and json configuration
            var logger = new ConsoleLogger();
            var policySource = new JsonPolicySource("policy.json", logger);

            // Setup dependency injection everything as singleton or can change to transient or scoped depends if need for future
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILogger>(logger)
                .AddSingleton<IPolicySource>(policySource)
                .AddSingleton<IRatingStrategy, HealthPolicyRatingStrategy>()
                .AddSingleton<IRatingStrategy, TravelPolicyRatingStrategy>()
                .AddSingleton<IRatingStrategy, LifePolicyRatingStrategy>()
                .AddSingleton<RatingEngine>()  // Registered RatingEngine
                .BuildServiceProvider();

            // Create rating strategies dictionary as a design pattern to support cleaner code and easier extension
            var ratingStrategies = new Dictionary<PolicyType, IRatingStrategy>
            {
                { PolicyType.Health, serviceProvider.GetServices<IRatingStrategy>().First(s => s is HealthPolicyRatingStrategy) },
                { PolicyType.Travel, serviceProvider.GetServices<IRatingStrategy>().First(s => s is TravelPolicyRatingStrategy) },
                { PolicyType.Life, serviceProvider.GetServices<IRatingStrategy>().First(s => s is LifePolicyRatingStrategy) }
            };

            // Get RatingEngine instance
            var engine = ActivatorUtilities.CreateInstance<RatingEngine>(
                serviceProvider,
                serviceProvider.GetRequiredService<IPolicySource>(),
                serviceProvider.GetRequiredService<ILogger>(),
                ratingStrategies
            );

            // Perform rating
            engine.Rate();

            // Output result
            if (engine.Rating > 0)
            {
                Console.WriteLine($"Rating: {engine.Rating}");
            }
            else
            {
                Console.WriteLine("No rating produced.");
            }

        }
    }
}
