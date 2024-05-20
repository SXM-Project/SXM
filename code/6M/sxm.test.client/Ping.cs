using CitizenFX.Core;
using Sxm.Core;
using Sxm.DependencyInjection;

namespace Sxm.Test.Client;

public interface IPing;

public class Ping : IPing
{
    public Ping(IServiceProvider provider)
    {
    }
    
    [Cmd("age")]
    public void AgeCmd(int age)
    {
        Debug.WriteLine("Age command called with age: " + age);
    }
}