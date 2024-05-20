using CitizenFX.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Sxm.Server;

public class Main : BaseScript
{
    private readonly List<string> _arguments = [];
    
    public Main()
    {
        // var services = new ServiceCollection();
        // services.AddSingleton<IMyService, MyService>();

        var myService = new MyService();
        
        _arguments.Add("enculer");
        _arguments.Add("enculer2");
        
        Debug.WriteLine("sxm.server!");

        foreach (var item in _arguments)
        {
            Debug.WriteLine("Argument: " + item);
        }
    }
}

public interface IMyService
{
}

public class MyService : IMyService
{
    public MyService()
    {
        
    }
}