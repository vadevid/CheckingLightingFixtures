using LabsDB.Entity;
using MainApp.Interfaces;
using MainApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controller;

[ApiController]
[Route("/client")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("get")]
    public IEnumerable<Room> GetRoom()
    {
        return _clientService.GetAllRooms();
    }
}