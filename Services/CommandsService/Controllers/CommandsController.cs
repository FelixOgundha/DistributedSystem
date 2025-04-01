using CommandsService.Data;
using CommandsService.Dto;
using CommandsService.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            if (!_repository.ExternalPlatformExists(platformId)) {
                return NotFound();
            }

            var commands = _repository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId,int commandId)
        {
            if (!_repository.ExternalPlatformExists(platformId))
            {
                return NotFound();
            }

            var platformCommands = _repository.GetCommand(platformId, commandId);

            if (platformCommands == null) 
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDto>(platformCommands));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CommandCreateForPlatform(int platformId, CommandCreateDto commandDto)
        {
            if (!_repository.ExternalPlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDto);

            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform),
                new { platfromId = platformId, commandId = commandReadDto.Id, commandReadDto
                });
        }

}
