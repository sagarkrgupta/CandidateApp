using CandidateApp.DataAccess.Data;
using CandidateApp.Domain.AppEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.DataAccess.DataRepositories.EntityRepos
{
    public class JobCandidateRepo : SqlRepository<JobCandidate>, IJobCandidateRepo
    {
        public JobCandidateRepo(ApplicationDbContext context) : base(context)
        {
        }



        public async Task<JobCandidate> GetJobCandidateByEmail(string email, CancellationToken cancellationToken)
        {
            var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);
            return result;
        }
    }

    public interface IJobCandidateRepo : IDataRepository<JobCandidate>
    {
        Task<JobCandidate> GetJobCandidateByEmail(string email, CancellationToken cancellationToken);
    }
}
