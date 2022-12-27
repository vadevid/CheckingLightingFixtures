using LabsDB.Entity;

namespace ClientApp.Repositories;

public interface IClientRepositories
{
    Task<IEnumerable<Room>> GetAllRooms();
}