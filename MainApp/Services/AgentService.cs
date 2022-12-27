using LabsDB.Entity;
using MainApp.Interfaces;

namespace MainApp.Services;

public class AgentService : IAgentService
{
    private ApplicationContext _context;

    public AgentService(ApplicationContext context)
    {
        _context = context;
    }
    public bool AddNewLamp(Lamp lamp)
    {
        if (lamp is null || !_context.Room.Any(h => h.Id == lamp.RoomId)) return false;
        try
        {
            _context.Add(lamp);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public Employee? AuthEmployee(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) return null;
        return _context.Employees.FirstOrDefault(e => e.Login == login && e.Password == password);
    }

    public Room? GetRoomById(int id)
    {
        if (id <= 0) return null;
        return _context.Room.FirstOrDefault(h => h.Id == id);
    }

    public Employee? GetEmployeeById(int id)
    {
        if (id <= 0) return null;
        return _context.Employees.FirstOrDefault(e => e.Id == id);
    }
}