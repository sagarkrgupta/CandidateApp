using CandidateApp.Business.AppServices;
using CandidateApp.Business.Contracts.IAppServices;
using CandidateApp.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace CandidateApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCandidateController : ControllerBase
    {
        private readonly IJobCandidateAppService _jobCandidateService;

        public JobCandidateController(IJobCandidateAppService jobCandidateService)
        {
            _jobCandidateService = jobCandidateService;
        }

        // GET: api/<JobCandidateController>

        /// <summary>
        /// Here you will get list of job candidates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCandidates(CancellationToken cancellationToken)
        {
            try
            {
                var candidates = await _jobCandidateService.GetAllAsync(cancellationToken);
                return Ok(candidates);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request was canceled.");
            }
        }

        [HttpGet("GetCandidateByEmail")]
        public async Task<IActionResult> GetCandidateByEmail([Required]  string email, CancellationToken cancellationToken)
        {
            // Check if email is null or empty, and handle accordingly (optional, based on your business logic)
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            var employee = await _jobCandidateService.GetCandidateViaEmailAsync(email, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST api/<JobCandidateController>
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCandidate(JobCandidateAddUpdateRequestDto value, CancellationToken cancellationToken)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // Return validation errors in the response
                return BadRequest(new { errors = GetErrors(ModelState) });
            }

            try
            {

                await _jobCandidateService.CreateOrUpdateCandidateAsync(value, cancellationToken);
                return Ok("Candidate successfully created/updated.");
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request was canceled.");
            }

            //Service

            // If the model is valid, process the request (for example, save to database)

        }


        // DELETE api/<JobCandidateController>/abc@yes.com
        [HttpDelete("DeleteViaEmail/{email}")]
        public async Task<IActionResult> DeleteViaEmail([Required] string email, CancellationToken cancellationToken)
        {
            // Check if email is null or empty, and handle accordingly (optional, based on your business logic)
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }


            try
            {

                await _jobCandidateService.DeleteCandidateAsync(email, cancellationToken);
                return Ok("Candidate successfully deleted.");
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request was canceled.");
            }
        }

        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync([Required] string id, CancellationToken cancellationToken)
        {
            // Check if email is null or empty, and handle accordingly (optional, based on your business logic)
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(id));
            }


            try
            {

                await _jobCandidateService.DeleteCandidateAsync(id, cancellationToken);
                return Ok("Candidate successfully deleted.");
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, "Request was canceled.");
            }
        }

        private IEnumerable<object> GetErrors(ModelStateDictionary modelState)
        {
            foreach (var state in modelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    yield return new
                    {
                        Field = state.Key,
                        ErrorMessage = error.ErrorMessage
                    };
                }
            }
        }

    }
}
