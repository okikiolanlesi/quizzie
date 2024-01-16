using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzie.Repositories;
using System;
using System.Threading.Tasks;

namespace Quizzie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOptionRepository _optionRepository;

        public OptionController(IMapper mapper, IOptionRepository optionRepository)
        {
            _mapper = mapper;
            _optionRepository = optionRepository;
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteOption(Guid id)
        {
            var option = await _optionRepository.GetById(id);
            if (option == null)
            {
                return NotFound(new { message = "Option does not exist" });
            }
            if(option.IsDeleted == true)
            {
                return Ok("This option has already been deleted");
            }
            option.IsDeleted = true;
            _optionRepository.MarkAsModified(option);
            var result = await _optionRepository.SaveChangesAsync();
            if (!result)
            {
                return Problem("Something went wrong with deleting the option");
            }
            return Ok(new {message = "Option deleted successfully" });
        }

    }
}
