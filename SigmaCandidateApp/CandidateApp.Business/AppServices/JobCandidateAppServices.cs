using AutoMapper;
using CandidateApp.Business.Contracts.IAppServices;
using CandidateApp.DataAccess.DataRepositories.EntityRepos;
using CandidateApp.Domain.AppEntities;
using CandidateApp.Domain.Shared.Utilities;
using CandidateApp.Dtos.Requests;
using CandidateApp.Dtos.Responeses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business.AppServices
{
    public class JobCandidateAppServices : IJobCandidateAppService
    {
        private readonly IJobCandidateRepo _jobCandidateRepo;
        private readonly IMapper _mapper;

        public JobCandidateAppServices(IJobCandidateRepo jobCandidateRepo, IMapper mapper)
        {
            _jobCandidateRepo = jobCandidateRepo;
            _mapper = mapper;

        }

        public async Task<JobCandidateItemResponseDto> CreateOrUpdateCandidateAsync(JobCandidateAddUpdateRequestDto item, CancellationToken cancellationToken)
        {
            var requestedCandidate = await _jobCandidateRepo.GetJobCandidateByEmail(item.Email, cancellationToken);

            if (requestedCandidate is null)
            {
                var candidate = _mapper.Map<JobCandidate>(item);
                candidate.CreatedDate = DateTimeHelper.GetCurrentUtcTime();
                // Create a new candidate
                await _jobCandidateRepo.CreateAsync(candidate, cancellationToken);


                return _mapper.Map<JobCandidateItemResponseDto>(candidate);
            }
            else
            {
                // Update the properties (example, you can customize as needed)
                requestedCandidate.FirstName = item.FirstName;
                requestedCandidate.LastName = item.LastName;
                requestedCandidate.PhoneNumber = item.PhoneNumber;
                requestedCandidate.TimeIntervalInSecond = item.TimeIntervalInSecond;
                requestedCandidate.LinkedInUrl = item.LinkedInUrl;
                requestedCandidate.GitHubUrl = item.GitHubUrl;
                requestedCandidate.Comment = item.Comment;
                requestedCandidate.LastUpdatedDate = DateTimeHelper.GetCurrentUtcTime();

                await _jobCandidateRepo.UpdateAsync(requestedCandidate, cancellationToken);

                return _mapper.Map<JobCandidateItemResponseDto>(requestedCandidate);
            }



        }

        public async Task DeleteCandidateAsync(string email, CancellationToken cancellationToken)
        {
            var requestedCandidate = await _jobCandidateRepo.GetJobCandidateByEmail(email, cancellationToken);

            if (requestedCandidate !=null)
            {
                // delete a candidate
                await _jobCandidateRepo.DeleteAsync(requestedCandidate.Id, cancellationToken);

            }



        }

        public async Task DeleteCandidateViaIdAsync(string encrytId, CancellationToken cancellationToken)
        {
            int id = AESUtil.DecryptStringToInt(encrytId) ;
            if (id > 0)
            {

                // delete a candidate
                await _jobCandidateRepo.DeleteAsync(id, cancellationToken);

            }



        }

        public async Task<IEnumerable<JobCandidateItemResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var items = await _jobCandidateRepo.ListAsync(cancellationToken);
            var results = _mapper.Map<List<JobCandidateItemResponseDto>>(items);

            return results;
        }

        public async Task<JobCandidateItemResponseDto> GetCandidateAsync(int id, CancellationToken cancellationToken)
        {
            var jobCandidate = await _jobCandidateRepo.ReadAsync(id, cancellationToken);
            if (jobCandidate != null)
            {
                return _mapper.Map<JobCandidateItemResponseDto>(jobCandidate);
            }
            return null;
        }

        public async Task<JobCandidateItemResponseDto> GetCandidateViaEmailAsync(string email, CancellationToken cancellationToken)
        {

            var jobCandidate = await _jobCandidateRepo.GetJobCandidateByEmail(email, cancellationToken);
            if (jobCandidate != null)
            {
                return _mapper.Map<JobCandidateItemResponseDto>(jobCandidate);
            }

            return null;
        }
    }
}
