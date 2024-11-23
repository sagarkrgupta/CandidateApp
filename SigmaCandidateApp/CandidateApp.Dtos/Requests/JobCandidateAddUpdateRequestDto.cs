using AutoMapper;
using CandidateApp.Domain.AppEntities;
using CandidateApp.Dtos.AutoMapper;
using CandidateApp.Dtos.Infrasctructures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CandidateApp.Dtos.Requests
{
    public class JobCandidateAddUpdateRequestDto : IMapFrom<JobCandidate>
    {

        [Required(ErrorMessage = "FirstName is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The first name can only contain letters and spaces.")]
        [MaxLength(256, ErrorMessage = "The first name  cannot exceed 256 characters.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The last name can only contain letters and spaces.")]
        [MaxLength(256, ErrorMessage = "The last name  cannot exceed 256 characters.")]
        public string LastName { get; set; }

        [RegularExpression(@"^\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{4}$",
        ErrorMessage = "Phone number is not in a valid format. The valid format is (XXX) XXX-XXXX or XXX-XXX-XXXX.")]
        [MaxLength(20, ErrorMessage = "The phone number cannot exceed 256 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(256, ErrorMessage = "The email  cannot exceed 256 characters.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "The URL is not valid.")]
        public string LinkedInUrl { get; set; }

        [Url(ErrorMessage = "The URL is not valid.")]
        public string GitHubUrl { get; set; }

        [Required(ErrorMessage = "Free text comment is required.")]
        // Custom validation to check for unwanted words
        [NoUnwantedText(ErrorMessage = "The comment contains unwanted text.")]
        public string Comment { get; set; }

        public int? TimeIntervalInSecond { get; set; }

        public DateTime InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Implementing IValidatableObject interface
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Create a list to hold validation results
            var validationResults = new List<ValidationResult>();


            // Check if the string contains invalid characters (e.g., anything other than letters and spaces)
            if (!Regex.IsMatch(FirstName, @"^[a-zA-Z\s]+$"))
            {
                validationResults.Add(new ValidationResult("The string can only contain letters and spaces.", new[] { nameof(FirstName) }));
            }


            // Validate the email
            if (string.IsNullOrEmpty(Email))
            {
                validationResults.Add(new ValidationResult("Email cannot be empty.", new[] { nameof(Email) }));
            }
            else
            {
                // Validate the email format using regex
                var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                if (!emailRegex.IsMatch(Email))
                {
                    validationResults.Add(new ValidationResult("The email format is invalid.", new[] { nameof(Email) }));
                }
            }

            // If the interval value is negative, add a validation error
            if (TimeIntervalInSecond.HasValue && TimeIntervalInSecond < 0)
            {
                // Returning a single ValidationResult indicating the error
                //yield return new ValidationResult("Time Interval cannot be negative.", new[] { "Time Interval" });
                validationResults.Add(new ValidationResult("Time Interval in second cannot be negative.", new[] { nameof(TimeIntervalInSecond) }));
            }

            // Return the list of validation results
            return validationResults;
        }


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
