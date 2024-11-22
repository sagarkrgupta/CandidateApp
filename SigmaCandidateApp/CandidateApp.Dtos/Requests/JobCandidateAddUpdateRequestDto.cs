using AutoMapper;
using CandidateApp.Domain.AppEntities;
using CandidateApp.Dtos.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Dtos.Requests
{
    public class JobCandidateAddUpdateRequestDto : IMapFrom<JobCandidate>
    {

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobCandidateAddUpdateRequestDto, JobCandidate>()
                //.ForMember(d => d.InsertedDate, opt => opt.MapFrom(s => s.CreatedDate))
                .ForMember(d => d.LastUpdatedDate, opt => opt.Ignore())
                //.ForMember(d => d.Description, opt => opt.NullSubstitute("N/A"))
                .ReverseMap();
        }
    }
}
