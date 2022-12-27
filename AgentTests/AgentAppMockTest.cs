using AgentApp;
using AgentWorker.Repositories;
using MainApp;
using Moq;
using NUnit.Framework;

namespace AgentTests;

public class AgentAppMockTest
{
    [Test]
    public async Task AuthSuccess()
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s => s.Auth(It.IsNotNull<AuthRequest>()))
            .Returns(Task.FromResult(new EmployeeMessage { Id = 1, Login = "123", Password = "123" }));
        var worker = new Worker(mock.Object);
        var res = await worker.Auth(new AuthRequest { Login = "123", Password = "123" });
        Assert.That(res.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task AuthWithNull()
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s => s.Auth(It.IsNotNull<AuthRequest>()))
            .Returns(Task.FromResult(new EmployeeMessage { Id = 1, Login = "", Password = "" }));
        var worker = new Worker(mock.Object);
        var res = await worker.Auth(null);
        Assert.That(res.Id, Is.EqualTo(-1));
    }

    [TestCase("  ", "123")]
    [TestCase("123", "  ")]
    public async Task AuthWithEmptyData(string login, string password)
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s =>
            s.Auth(It.Is<AuthRequest>(
                r => string.IsNullOrWhiteSpace(r.Login) || !string.IsNullOrWhiteSpace(r.Password)))).Returns(
            Task.FromResult(new EmployeeMessage()
                {Id = -1, Login = "", Password = ""}));
        var worker = new Worker(mock.Object);
        var res = await worker.Auth(new AuthRequest{Login = login, Password = password});
        Assert.That(res.Id, Is.EqualTo(-1));
    }
    [TestCase(null, "123")]
    [TestCase("123", null)]
    [TestCase(null, null)]
    public async Task AuthWithNullData(string login, string password)
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s =>
            s.Auth(It.Is<AuthRequest>(
                r => string.IsNullOrWhiteSpace(r.Login) || !string.IsNullOrWhiteSpace(r.Password)))).Returns(
            Task.FromResult(new EmployeeMessage()
                {Id = -1, Login = "", Password = ""}));
        var worker = new Worker(mock.Object);
        AuthRequest? req = null;
        Assert.Catch<ArgumentNullException>( () => req = new AuthRequest {Login = login, Password = password});
    }
    [Test]
    public async Task AddNewLampSuccess()
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s => s.AddNewLamp(It.IsNotNull<NewRequest>()))
            .Returns(Task.FromResult(new NewResponse { Res = true }));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLamp(new NewRequest
            { Glows = true, Room = 1, Employee = 1});
        Assert.That(res.Res, Is.True);
    }

    [Test]
    public async Task AddNewLampWithNull()
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s => s.AddNewLamp(It.Is<NewRequest>(r => r == null)))
            .Returns(Task.FromResult(new NewResponse { Res = false }));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLamp(null);
        Assert.That(res.Res, Is.False);
    }

    [Test]
    public async Task AddNewLampWithErrorRoom()
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s => s.AddNewLamp(It.Is<NewRequest>(r => r.Room > 0)))
            .Returns(Task.FromResult(new NewResponse { Res = true }));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLamp(new NewRequest
            { Glows = true, Room = -1, Employee = 1});
        Assert.That(res.Res, Is.False);
    }

    [Test]
    public async Task AddNewLampWithErrorEmployee()
    {
        var mock = new Mock<IAgentRepository>();
        mock.Setup(s => s.AddNewLamp(It.Is<NewRequest>(r => r.Employee > 0)))
            .Returns(Task.FromResult(new NewResponse { Res = true }));
        var worker = new Worker(mock.Object);
        var res = await worker.AddNewLamp(new NewRequest
            { Glows = true, Room = 1, Employee = -1});
        Assert.That(res.Res, Is.False);
    }
}