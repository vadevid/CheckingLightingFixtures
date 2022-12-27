using System.Text.Json.Serialization;

namespace LabsDB.Entity;

public class Employee
{
    public Employee()
    {
        Login = string.Empty;
        Password = string.Empty;
    }

    public Employee(string login, string password)
    {
        Login = login;
        Password = password;
    }

    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("login")] public string Login { get; set; }
    [JsonPropertyName("password")] public string Password { get; set; }
}