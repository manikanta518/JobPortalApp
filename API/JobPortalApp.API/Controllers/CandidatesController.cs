using System;
using System.Linq;
using System.Threading.Tasks;
using JobPortalApp.API.Model;
using JobPortalApp.Domain;
using JobPortalApp.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using JobPortalApp.Model;
using Microsoft.AspNetCore.Http;

namespace JobPortalApp.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly IFilter<Candidate> _filterCandidate;
        private readonly ICandidateService _candidateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidatesController"/> class.
        /// </summary>
        /// <param name="filterCandidate">The filter candidate.</param>
        /// <param name="candidateService">The candidate service.</param>
        public CandidatesController(IFilter<Candidate> filterCandidate, ICandidateService candidateService)
        {
            _filterCandidate = filterCandidate;
            _candidateService = candidateService;
        }


        /// <summary>
        /// Adds the specified candidate.
        /// </summary>
        /// <param name="candidateRequest">The candidate.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ApiResponse>> Add(CandidateRequest candidateRequest)
        {
            try
            {
                //Validation
                if (!ModelState.IsValid)
                    return BadRequest();

                //Add to DB
                await _candidateService.Add(candidateRequest.BuildRequest());

                //Return
                return Created("", new ApiResponse {Message = "Candidate has been added successfully"});
            }
            catch (Exception e)
            {
                //LOG The Exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        /// <summary>
        /// Searches the specified skills.
        /// </summary>
        /// <param name="skills">The skills with coma separator to add more skills</param>
        /// <returns></returns>
        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CandidateRequest>> Search([FromQuery] string skills)
        {
            try
            {
                //Validation
                if (string.IsNullOrWhiteSpace(skills))
                    return BadRequest();

                //Get filtered candidates by skills
                var candidates = await _filterCandidate.Filter(await _candidateService.GetCandidates(),
                    new SkillsSpecification(skills.Split(',').ToList()));

                //No data found
                if (candidates == null || !candidates.Any())
                    return NotFound();

                //Get Top matching record
                var result = candidates.FirstOrDefault();
                var candidateRequest = new CandidateRequest();
                candidateRequest.BuildResponse(result);
                return Ok(candidateRequest);

            }
            catch (Exception e)
            {
                //LOG The Exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}