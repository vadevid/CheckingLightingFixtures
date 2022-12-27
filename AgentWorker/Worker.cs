using AgentWorker.Repositories;
using MainApp;

namespace AgentApp;

public class Worker
{
    private readonly IAgentRepository _agentRepository;

    public Worker(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }
    public async Task<EmployeeMessage> Auth(AuthRequest request)
    {
        if (request is null|| string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
        {
            return new EmployeeMessage {Id = -1, Login = "", Password = ""};
        }

        return await _agentRepository.Auth(request);
    }
    public async Task<NewResponse> AddNewLamp(NewRequest request)
    {
        if (request is null ||request.Room<=0||request.Employee<=0) return new NewResponse {Res = false};
        return await _agentRepository.AddNewLamp(request);
    }
}