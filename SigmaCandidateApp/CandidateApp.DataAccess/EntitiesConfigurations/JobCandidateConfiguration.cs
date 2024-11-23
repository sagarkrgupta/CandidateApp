using CandidateApp.Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.DataAccess.EntitiesConfigurations
{
    public class JobCandidateConfiguration : IEntityTypeConfiguration<JobCandidate>
    {
        public void Configure(EntityTypeBuilder<JobCandidate> builder)
        {
            builder.ToTable("JobCandidates");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).HasMaxLength(256).IsRequired(true);
            builder.Property(c => c.LastName).HasMaxLength(256).IsRequired(true);
            builder.Property(c => c.PhoneNumber).HasMaxLength(20).IsRequired(false);
            builder.Property(c => c.Email).HasMaxLength(256).IsRequired(true);            
            builder.Property(c => c.LinkedInUrl).IsRequired(false);
            builder.Property(c => c.GitHubUrl).IsRequired(false);
            builder.Property(c => c.Comment).IsRequired(true);
            builder.Property(c => c.TimeIntervalInSecond).IsRequired(false);

            builder.HasIndex(c => c.Email).IsUnique(); // Unique index on Email
            builder.HasIndex(c => c.PhoneNumber);
            builder.HasIndex(c => c.LinkedInUrl);
            builder.HasIndex(c => c.GitHubUrl);
        }
    }
}
