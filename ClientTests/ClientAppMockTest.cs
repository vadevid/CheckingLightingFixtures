using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.Controllers;
using ClientApp.Models;
using ClientApp.Repositories;
using LabsDB.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ClientTests;

public class ClientAppMockTest
{
    private readonly IEnumerable<Room> _testRooms;

    public ClientAppMockTest()
    {
        var testHouses = new List<Room>();
        var a = new Employee {Id = 1, Login = "123", Password = "123"};
        for (var i = 1; i < 3; i++)
        {
            var wh = new Room() {Id = i};
            var item0 = new Lamp(true, wh);
            var item1 = new Lamp(true, wh);
            wh.Lamps.Add(item0);
            wh.Lamps.Add(item1);
            testHouses.Add(wh);
        }

        _testRooms = testHouses;
    }

    [Test]
    public async Task GetRoomsSuccess()
    {
        var mock = new Mock<IClientRepositories>();
        mock.Setup(s => s.GetAllRooms()).Returns(Task.FromResult(_testRooms));
        var controller = new HomeController(mock.Object);
        var actionResult = await controller.Index();
        Assert.That(actionResult, Is.TypeOf<ViewResult>());
        var viewResult = (ViewResult) actionResult;
        Assert.That(viewResult.ViewData.Model, Is.TypeOf<RoomViewModel>());
        var model = (RoomViewModel) viewResult.ViewData.Model!;
        Assert.That(model.Rooms.Count(), Is.EqualTo(_testRooms.Count()));
    }
    [Test]
    public async Task GetRoomsServerError()
    {
        var mock = new Mock<IClientRepositories>();
        mock.Setup(s => s.GetAllRooms()).Returns(Task.FromResult(Enumerable.Empty<Room>()));
        var controller = new HomeController(mock.Object);
        var actionResult = await controller.Index();
        Assert.That(actionResult, Is.TypeOf<ViewResult>());
        var viewResult = (ViewResult) actionResult;
        Assert.That(viewResult.ViewData.Model, Is.TypeOf<RoomViewModel>());
        var model = (RoomViewModel) viewResult.ViewData.Model!;
        Assert.That(model.Rooms.Count(), Is.EqualTo(0));
    }
}