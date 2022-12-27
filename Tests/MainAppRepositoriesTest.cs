using System;
using System.Collections.Generic;
using System.Linq;
using LabsDB.Entity;
using MainApp;
using MainApp.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests.MainApp;

public class MainAppRepositoriesTest
{
    private ApplicationContext _context;

    public MainAppRepositoriesTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase($"ContextDb_{DateTime.Now.ToFileTimeUtc()}").Options;
        _context = new ApplicationContext(options, true);
        FillDb();
    }

    private void FillDb()
    {
        var testRoom = new List<Room>();
        var employee = new Employee {Id = 1, Login = "123", Password = "123"};
        _context.Employees.Add(employee);
        for (var i = 1; i < 3; i++)
        {
            var room = new Room {Id = i};
            var ind0 = new Lamp(true, room);
            room.Lamps.Add(ind0);
            testRoom.Add(room);
        }

        _context.AddRange(testRoom);
        _context.SaveChanges();
    }

    ~MainAppRepositoriesTest()
    {
        _context.Dispose();
    }

    [Test]
    public void GetAllRooms()
    {
        var service = new ClientService(_context);
        Assert.That(service.GetAllRooms(), Is.InstanceOf<IEnumerable<Room>>());
    }

    [Test]
    public void AddNewLampSuccess()
    {
        var service = new AgentService(_context);
        var room = _context.Room.FirstOrDefault();
        Assert.That(service.AddNewLamp(new Lamp(true, room)), Is.True);
    }

    [TestCase("123", "123", ExpectedResult = false)]
    [TestCase("1233", "123", ExpectedResult = true)]
    [TestCase("   ", "123", ExpectedResult = true)]
    [TestCase("123", "    ", ExpectedResult = true)]
    [TestCase("   ", "    ", ExpectedResult = true)]
    [TestCase(null, "123", ExpectedResult = true)]
    [TestCase("123", null, ExpectedResult = true)]
    [TestCase(null, null, ExpectedResult = true)]
    public bool AuthWithErrorData(string login, string password)
    {
        var service = new AgentService(_context);
        return service.AuthEmployee(login, password) is null;
    }
    
    [TestCase(1, ExpectedResult = false)]
    [TestCase(15, ExpectedResult = true)]
    public bool GetRoomById(int id)
    {
        var service = new AgentService(_context);
        return service.GetRoomById(id) is null;
    }

    [TestCase(1, ExpectedResult = false)]
    [TestCase(15, ExpectedResult = true)]
    public bool GetEmployeeById(int id)
    {
        var service = new AgentService(_context);
        return service.GetEmployeeById(id) is null;
    }
}