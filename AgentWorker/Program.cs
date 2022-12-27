using AgentApp;
using AgentApp.Services;
using AgentWorker;
using AgentWorker.Repositories;
using Google.Protobuf.WellKnownTypes;
using MainApp;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static void Main(string[] args)
    {
        bool numberCheck = true;
        var service = new ServiceCollection().AddTransient<IAgentRepository,AgentService>();

        using var serviceProvider = service.BuildServiceProvider();
        var worker = new Worker(serviceProvider.GetService<IAgentRepository>()!);
        Console.WriteLine("Введите логин");
        var login = Console.ReadLine();
        Console.WriteLine("Введите пароль");
        var pass = Console.ReadLine();
        var agent = worker.Auth(new AuthRequest {Login = login, Password = pass});
        bool isGlows = false;
        int roomNumber = 0;
        while (numberCheck)
        {
            Console.WriteLine("Введите номер аудитории");
            try
            {
                roomNumber = int.Parse(Console.ReadLine());
                numberCheck = false;
            }
            catch
            {
                Console.WriteLine("Вы ввели не число");
            }
        }

        bool check = true;
        while (check)
        {
            Console.WriteLine("Введите true или false");
            try
            {
                isGlows = bool.Parse(Console.ReadLine());
                check = false;
            }
            catch
            {
                Console.WriteLine("Вы ввели некорректные данные");
            }
        }
        

        var response = worker.AddNewLamp(new NewRequest {Room = roomNumber , Employee = agent.Id, Glows = isGlows}).Result;
        if (response.Res) Console.WriteLine("Лампочка успешно добавлена");
        else Console.WriteLine("Лампочка не была добавлена. Попробуйте снова");
        Console.WriteLine("Для выхода нажмите любую клавишу");
        Console.ReadKey();


    }
}