using LabsDB.Entity;
using MainApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MainApp.Services;

public class ClientService : IClientService
{
    private ApplicationContext _context;

    public ClientService(ApplicationContext context)
    {
        _context = context;
    }
    public IEnumerable<Room> GetAllRooms()
    {
        var rooms = _context.Room.Include(h => h.Lamps).ToList();
        rooms = rooms.Select(h =>
        {
            h.Lamps = h.Lamps.Select(i =>
            {
                i.Room = null;
                return i;
            }).ToList();
            return h;
        }).ToList();
        return rooms;
    }
}