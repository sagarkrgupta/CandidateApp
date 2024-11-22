using CandidateApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.DataAccess
{
    public static class RegisterServicesExtensions
    {
        public static void ApplyMigrationsAndCreateDatabase(this IServiceProvider  serviceProvider)
        {
            // Apply any pending migrations and create the database if necessary
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();  // This ensures the database is created if it doesn't exist
            }
        }
    }
}
