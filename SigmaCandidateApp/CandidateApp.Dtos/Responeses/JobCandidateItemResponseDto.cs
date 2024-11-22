using AutoMapper;
using CandidateApp.Domain.AppEntities;
using CandidateApp.Dtos.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Dtos.Responeses
{
    public class JobCandidateItemResponseDto : IMapFrom<JobCandidate>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string Comment { get; set; }
        public int? TimeIntervalInSecond { get; set; }

        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        public void Mapping(Profile profile)
        {
            var memberNames = new[] { nameof(PhoneNumber), nameof(LinkedInUrl), nameof(GitHubUrl), nameof(Comment) };

            profile.CreateMap<JobCandidate, JobCandidateItemResponseDto>()
                .ForMember(d => d.InsertedDate, opt => opt.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.UpdatedDate, opt => opt.Ignore())
                .ForAllMembers(opt =>
                {
                    // Apply NullSubstitute only to specific properties
                    if (memberNames.Contains(opt.DestinationMember.Name))
                    {
                        opt.NullSubstitute("N/A");
                    }
                });                
        }
    }
}
