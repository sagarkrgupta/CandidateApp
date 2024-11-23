using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Domain.AppEntities
{
    public class JobCandidate : BasedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedInUrl{ get; set; }
        public string GitHubUrl { get; set; }
        public string Comment { get; set; }
        public int? TimeIntervalInSecond { get; set; }
    }
}
