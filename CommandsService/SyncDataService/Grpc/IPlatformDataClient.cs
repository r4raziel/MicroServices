using CommandsService.Models;

namespace CommandsService.SynDataServices.Grpc
{

    public interface IPlatformDataClient
    {
        IEnumerable<Platform> ReturnAllPlatforms();

    }
}