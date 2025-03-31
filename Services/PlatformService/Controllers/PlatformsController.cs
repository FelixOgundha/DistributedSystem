﻿using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _context;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandData;

        public PlatformsController(IPlatformRepository context, IMapper mapper,ICommandDataClient commandData)
        {
            _context = context;
            _mapper = mapper;
            _commandData = commandData;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatformDto()
        {
            var result = _context.GetAllPlatforms();
            var platformReadDtos = _mapper.Map<IEnumerable<PlatformReadDto>>(result);
            return Ok(platformReadDtos); 
        }

        [HttpGet("{id}",Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        { 
            Platform result = _context.GetPlatformById(id);
            if (result == null)
            {
                return NotFound();  
            }
            var platformReadDto = _mapper.Map<PlatformReadDto>(result);
            return Ok(platformReadDto); 
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlatform(PlatformCreateDto plat)
        {
            var newPlatform = _mapper.Map<Platform>(plat);
            _context.CreatePlatform(newPlatform);
            _context.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(newPlatform);

            try
            {
                await _commandData.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex) { 
              Console.WriteLine(ex);
            }

            return CreatedAtRoute(nameof(GetPlatformById),new { Id = platformReadDto.Id},platformReadDto);
        }

    }
}
