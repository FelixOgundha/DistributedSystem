using CommandsService.Models;

namespace CommandsService.SyncDataService.Grpc
{
    public interface IPlatfromDataClient
    {
        IEnumerable<Platform> ReturnAllPlatfroms();
     }
}
