using CommandsService.Models;
using MapsterMapper;

namespace CommandsService.SyncDataService.Grpc
{
    public class PlatformDataClient : IPlatfromDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Platform> ReturnAllPlatfroms()
        {
            throw new NotImplementedException();
        }
    }
}
