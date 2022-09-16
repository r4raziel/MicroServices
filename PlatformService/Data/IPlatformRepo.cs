using PaltformService.Models;

namespace PaltformService.Data
{
    public interface IPlatformRepo
    {

        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatfroms();
    }


}