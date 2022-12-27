using System.Text.Json.Serialization;

namespace LabsDB.Entity;

public class Lamp
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("glows")] public bool Glows { get; set; }
    [JsonPropertyName("timeStamp")] public DateTime TimeStamp { get; set; }
    public Room Room { get; set; }
    [JsonPropertyName("roomId")] public int RoomId { get; set; }
    
    public Lamp()
    {
        Glows = false;
        TimeStamp = DateTime.Now;
        Room = new Room();
    }

    public Lamp(bool glows, Room room)
    {
        Glows = glows;
        TimeStamp = DateTime.Now;
        Room = room;
        RoomId = room.Id;
    }
}