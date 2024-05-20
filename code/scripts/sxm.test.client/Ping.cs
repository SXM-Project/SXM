using CitizenFX.Core;
using Sxm.Core;

namespace Sxm.Test.Client;

public interface IPing;

public class Ping : IPing
{
    [Cmd("age")]
    public void AgeCmd(int age)
    {
        Debug.WriteLine("Age command called with age: " + age);
    }
}