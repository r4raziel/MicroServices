using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {

        bool SaveChanges();

        // Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExist(int PlatformId);



        //Commands
        IEnumerable<Command> GetCommandsForPlatform(int PlatformId);
        Command GetCommand(int PlatformId, int commandId);
        void CreateCommand(int PlatformId, Command command);

    }


}