using AgentWorker.Repositories;
using Grpc.Net.Client;
using MainApp;

namespace AgentApp.Services;

public class AgentService : IAgentRepository
{
    public async Task<EmployeeMessage> Auth(AuthRequest request)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5174");
        var client = new Agent.AgentClient(channel);
        return await client.AuthAsync(request);
    }

    public async Task<NewResponse> AddNewLamp(NewRequest request)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5174");
        var client = new Agent.AgentClient(channel);
        return await client.AddNewLampAsync(request);
    }
}