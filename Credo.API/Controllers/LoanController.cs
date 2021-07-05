using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Credo.API.Extensions;
using Credo.Domain.Dtos.Commands;
using Credo.Domain.Dtos.Queries;
using Credo.Domain.Factories;
using Credo.Domain.Interfaces;
using Credo.Domain.Request;
using Credo.Domain.Response;
using Microsoft.AspNetCore.Authorization;


namespace Credo.API.Controllers
{
    [Route("api/loan")]
    [Authorize]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoanController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<LoanReadDto>>> GetLoansByUserId()
        {
            var userId = HttpContext.GetActiveUserId();

            if (userId == 0)
            {
                return Unauthorized("Authorization needed (or some other text from resources)");
            }

            var loans = await _unitOfWork.LoanRepository.LoansByUserId(userId);

            if (loans.Any())
            {
                var successResponse = new Response<IEnumerable<LoanReadDto>>(loans, true);
                return Ok(successResponse);
            }

            var failResponse = new Response<IEnumerable<LoanReadDto>>(null, false);
            return NotFound(failResponse);
        }

        [HttpGet("single/{loanId:int}")]
        public async Task<ActionResult<LoanReadDto>> GetLoanById(int loanId)
        {
            var userId = HttpContext.GetActiveUserId();

            if (userId == 0)
            {
                return Unauthorized("Authorization needed (or some other text from resources)");
            }

            var loan = await _unitOfWork.LoanRepository.GetLoanById(loanId, userId);

            if (loan != null)
            {
                var successResponse = new Response<LoanReadDto>(loan, true);
                return Ok(successResponse);
            }

            var failResponse = new Response<LoanReadDto>(null, false);
            return NotFound(failResponse);
        }

        [HttpPost("demand")]
        public async Task<IActionResult> DemandLoan([FromBody] LoanDemandRequest model)
        {
            var userId = HttpContext.GetActiveUserId();
            var loanToCreate = LoanFactory.InitialLoanModel(model, userId);

            var loanId = await _unitOfWork.LoanRepository.DemandLoan(loanToCreate, userId);
            var loan = await _unitOfWork.LoanRepository.GetLoanById(loanId, userId);
            
            if (loanId > 0)
            {
                return CreatedAtAction(nameof(GetLoanById), new {loanId}, loan);
            }

            return BadRequest("Error processing request");
        }

        [HttpPut("update/{loanId:int}")]
        public async Task<IActionResult> UpdateLoan(int loanId, [FromBody] LoanUpdateRequest model)
        {
            var userId = HttpContext.GetActiveUserId();
            var loanToUpdate = await _unitOfWork.LoanRepository.GetLoanById(loanId, userId);
            if (loanToUpdate == null)
            {
                return NotFound("No such loan found");
            }

            var updatedDto = LoanFactory.UpdateLoanModel(model);
            var updateResult = await _unitOfWork.LoanRepository.EditLoan(loanId, updatedDto);
            if (updateResult > 0)
            {
                return NoContent();
            }

            return BadRequest("Error processing Request");
        }
    }
}
