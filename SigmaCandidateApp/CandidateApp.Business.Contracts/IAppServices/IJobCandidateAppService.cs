using CandidateApp.Dtos.Requests;
using CandidateApp.Dtos.Responeses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Business.Contracts.IAppServices
{
    public interface IJobCandidateAppService
    {
        Task<JobCandidateItemResponseDto> CreateOrUpdateCandidateAsync(JobCandidateAddUpdateRequestDto item, CancellationToken cancellationToken);
        Task DeleteCandidateAsync(string email, CancellationToken cancellationToken);
        Task DeleteCandidateViaIdAsync(string encrytId, CancellationToken cancellationToken);
        Task<IEnumerable<JobCandidateItemResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<JobCandidateItemResponseDto> GetCandidateViaEmailAsync(string email, CancellationToken cancellationToken);
        Task<JobCandidateItemResponseDto> GetCandidateAsync(int id, CancellationToken cancellationToken);
    }
}
