using PlatformService.DTOs;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatfrom(PlatfromPublishedDto platformPublishedDto);
    }
}
