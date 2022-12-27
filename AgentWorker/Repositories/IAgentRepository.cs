using MainApp;

namespace AgentWorker.Repositories;

public interface IAgentRepository
{
    public Task<EmployeeMessage> Auth(AuthRequest request);

    public Task<NewResponse> AddNewLamp(NewRequest request);
}