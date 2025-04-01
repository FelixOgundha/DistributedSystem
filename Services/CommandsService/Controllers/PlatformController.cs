using CommandsService.Data;
using CommandsService.Dto;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

       
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var result = _repository.GetAllPlatforms(); // Sync call, no await needed
            IEnumerable<PlatformReadDto> response = _mapper.Map<IEnumerable<PlatformReadDto>>(result);

            return Ok(response);
        }


    }
}
