using LabsDB.Entity;

namespace ClientApp.Models;

public class RoomViewModel
{
    public IEnumerable<Room> Rooms { get; set; }

    public RoomViewModel()
    {
        Rooms = new List<Room>();
    }

    public RoomViewModel(IEnumerable<Room> rooms)
    {
        Rooms = rooms;
    }
}