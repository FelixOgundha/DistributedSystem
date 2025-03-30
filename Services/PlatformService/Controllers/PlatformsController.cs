using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _context;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatformDto()
        {
            var result = _context.GetAllPlatforms();
            // Use Mapster to map the collection correctly
            var platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(result);
            return Ok(platformReadDtos); // Return a successful response with the mapped data
        }

        [HttpGet("{id}")]
        public ActionResult<PlatformReadDto> GetPlatformDto(int id)
        {
            // Retrieve the platform from the repository
            Platform result = _context.GetPlatformById(id);

            // If no platform is found, return 404 Not Found
            if (result == null)
            {
                return NotFound();  // 404 Not Found response
            }

            // Map the result to PlatformReadDto
            var platformReadDto = _mapper.Map<PlatformReadDto>(result);

            // Return the mapped DTO with a 200 OK response
            return Ok(platformReadDto);  // 200 OK response with the data
        }

        [HttpPost]
        public ActionResult CreatePlatform(PlatformCreateDto plat)
        {
            var newPlatform = _mapper.Map<Platform>(plat);
            _context.CreatePlatform(newPlatform);
            _context.SaveChanges();

            return Created();
        }

    }
}
