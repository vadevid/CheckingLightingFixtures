using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ClientApp.Services;
using LabsDB.Entity;
using Moq;
using NUnit.Framework;

namespace ClientTests;

public class ClientAppRepositoryTest
{
    private readonly IEnumerable<Room> _testRoom;

    public ClientAppRepositoryTest()
    {
        var testHouses = new List<Room>();
        var agent = new Employee {Id = 1, Login = "123", Password = "123"};
        for (var i = 1; i < 3; i++)
        {
            var wh = new Room() {Id = i};
            var item0 = new Lamp(true, wh);
            var item1 = new Lamp(true, wh);
            item0.Room = null;
            item1.Room = null;
            wh.Lamps.Add(item0);
            wh.Lamps.Add(item1);
            testHouses.Add(wh);

            
        }

        _testRoom = testHouses;
    }
    
    public class DelegatingHandlerStub : DelegatingHandler {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStub()
        {
            
            _handlerFunc = (request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.RequestMessage = request;
                var res = JsonSerializer.Serialize(new List<Room>());
                response.Content = new StringContent(res);
                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(IEnumerable<Room> testRooms)
        {
            _handlerFunc = (request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.RequestMessage = request;
                var res = JsonSerializer.Serialize(testRooms);
                response.Content = new StringContent(res);
                return Task.FromResult(response);
            };
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc) {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            return _handlerFunc(request, cancellationToken);
        }
    }
    
    [Test]
    public async Task GetRoomHttpSuccess()
    {
        var clientHandlerStub = new DelegatingHandlerStub(_testRoom);
        var client = new HttpClient(clientHandlerStub);
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(r => r.CreateClient(It.IsAny<string>())).Returns(client);
        var service = new ClientService(mock.Object);
        var res = await service.GetAllRooms();
        Assert.That(res.Count(), Is.EqualTo(_testRoom.Count()));
    }
    
    [Test]
    public async Task GetHouseHttpError()
    {
        var client = new HttpClient();
        var mock = new Mock<IHttpClientFactory>();
        mock.Setup(r => r.CreateClient(It.IsAny<string>())).Returns(client);
        var service = new ClientService(mock.Object);
        var res = await service.GetAllRooms();
        Assert.That(res.Count(), Is.EqualTo(0));
    }
}