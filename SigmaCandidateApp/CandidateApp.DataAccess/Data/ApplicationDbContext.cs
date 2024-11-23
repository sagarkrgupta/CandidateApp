using CandidateApp.Domain.AppEntities;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public virtual DbSet<JobCandidate>  JobCandidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                      
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
           // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("CandidateApp.DataAccess"));
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)));

            //base.OnModelCreating(modelBuilder);



        }
    }
}
