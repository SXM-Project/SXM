using CitizenFX.Core;
using Microsoft.Extensions.DependencyInjection;
using Sxm.Core.Server;

namespace Sxm.Server;

public sealed class Main : BaseScript
{
    public Main()
    {
        var services = new ServiceCollection();

        services.AddCore(options =>
        {
            options.Assemblies =
            [
                typeof(Main).Assembly
            ];
        });
    }
}

// public class Main : BaseScript
// {
//     private readonly List<string> _arguments = [];
//     
//     public Main()
//     {
//         // var services = new ServiceCollection();
//         // services.AddSingleton<IMyService, MyService>();
//
//         var myService = new MyService();
//         
//         _arguments.Add("enculer");
//         _arguments.Add("enculer2");
//         
//         Debug.WriteLine("sxm.server!");
//
//         foreach (var item in _arguments)
//         {
//             Debug.WriteLine("Argument: " + item);
//         }
//     }
// }
//
// public interface IMyService
// {
// }
//
// public class MyService : IMyService
// {
//     public MyService()
//     {
//         
//     }
// }