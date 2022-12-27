using LabsDB.Entity;

namespace MainApp.Interfaces;

public interface IAgentService
{
    bool AddNewLamp(Lamp lamp);
    Employee? AuthEmployee(string login, string password);
    Room? GetRoomById(int id);
    Employee? GetEmployeeById(int id);
}