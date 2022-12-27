using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientApp.Models;
using ClientApp.Repositories;
using LabsDB.Entity;
using MainApp;

namespace ClientApp.Controllers;

public class HomeController : Controller
{
    private readonly IClientRepositories _clientRepositories;
    public HomeController(IClientRepositories clientRepository)
    {
        _clientRepositories = clientRepository;
    }
    public async Task<IActionResult>  Index()
    {
        return View(new RoomViewModel{Rooms = await _clientRepositories.GetAllRooms()});
    }
}