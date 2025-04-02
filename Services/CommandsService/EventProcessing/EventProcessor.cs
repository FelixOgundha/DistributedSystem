using CommandsService.Data;
using MapsterMapper;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly AppDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(
            AppDbContext context,
            IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _context = context;
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            throw new NotImplementedException();
        }



    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }


}
