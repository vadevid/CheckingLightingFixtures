using System.Text.Json;
using ClientApp.Repositories;
using LabsDB.Entity;

namespace ClientApp.Services;

public class ClientService:IClientRepositories
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<IEnumerable<Room>> GetAllRooms()
    {
        var client = _httpClientFactory.CreateClient();
        try
        {
            var responseMessage = await client.GetAsync("http://localhost:5173/client/get");
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<IEnumerable<Room>>(await responseMessage.Content
                    .ReadAsStringAsync())??Enumerable.Empty<Room>();
            }
            return Enumerable.Empty<Room>();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return Enumerable.Empty<Room>();
        }
    }
}