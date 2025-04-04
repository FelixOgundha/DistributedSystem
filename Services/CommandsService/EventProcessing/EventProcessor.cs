using CommandsService.Data;
using CommandsService.Dto;
using CommandsService.Models;
using MapsterMapper;
using System.Text.Json;

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
            var eventType = DetermineEvent(message);

            switch (eventType) 
            { 
                case EventType.PlatformPublished:
                    break;
                default;
                    break;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope()) { 
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);

                    if (!repo.ExternalPlatformExists(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("--> Platfrom already exists...");
                    }
                }
                catch(Exception ex) {
                {
                        Console.WriteLine("Could not add Platfrom to DB", ex.Message.ToString());
                }
            }
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform Published":
                    Console.WriteLine("Platform Published Event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type ");
                    return EventType.Undetermined;
            }
        }



    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }


}
