using GRS.ApplicationCore.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRS.Infrastructure.Data
{
    public static class GRSContextSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                var context = (GRSContext)applicationBuilder
                    .ApplicationServices.GetService(typeof(GRSContext));
                
                if (!context.Skills.Any())
                {
                    context.Skills.AddRange(
                        GetPreconfiguredSkills());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger("GRS seed");
                    log.LogError(ex.Message);
                    await SeedAsync(applicationBuilder, loggerFactory, retryForAvailability);
                }
            }
        }

        static IEnumerable<Skill> GetPreconfiguredSkills()
        {
            return new List<Skill>()
            {
                new Skill() { Name = "Azure"},
                new Skill() { Name = ".NET" },
                new Skill() { Name = "Visual Studio" },
                new Skill() { Name = "SQL Server" },
                new Skill() { Name = "C#" },
                new Skill() { Name = "T-SQL" },
                new Skill() { Name = "JavaScript" },
                new Skill() { Name = "JQuery" }
            };
        }
    }
}
