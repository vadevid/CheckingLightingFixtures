using System.Text.Json.Serialization;

namespace LabsDB.Entity;

public class Room
{
    public Room()
    {
        Lamps = new List<Lamp>();
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("lamps")] public List<Lamp> Lamps { get; set; }
}