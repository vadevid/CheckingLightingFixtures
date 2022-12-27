using MainApp.Interfaces;
using Grpc.Core;
using LabsDB.Entity;

namespace MainApp.Controller;

public class AgentController : Agent.AgentBase
{
    private readonly IAgentService _agentService;

    public AgentController(IAgentService agentService)
    {
        _agentService = agentService;
    }

    public EmployeeMessage Auth(AuthRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Login) ||  string.IsNullOrWhiteSpace(request.Password))
        return new EmployeeMessage { Id = -1, Login = "", Password = "" };
        var agent = _agentService.AuthEmployee(request.Login, request.Password);
        return agent is null
            ? new EmployeeMessage {Id = -1, Login = "", Password = ""}
            : new EmployeeMessage() {Id = agent.Id, Login = agent.Login, Password = agent.Password};
    }

    public override Task<EmployeeMessage> Auth(AuthRequest request, ServerCallContext context)
    {
        return Task.FromResult(Auth(request));
    }
    public NewResponse AddNewLamp(NewRequest request)
    {
        if (request is null || request.Room <= 0  || request.Employee <= 0)
        return new NewResponse {Res = false};
        var room = _agentService.GetRoomById(request.Room);
        if (room is null)
            return new NewResponse {Res = false};
        var ind = new Lamp(request.Glows, room);
        var res = _agentService.AddNewLamp(ind);
        return new NewResponse {Res = res};
    }
    public override Task<NewResponse> AddNewLamp(NewRequest request, ServerCallContext context)
    {
        return Task.FromResult(AddNewLamp(request));
    }
    
}