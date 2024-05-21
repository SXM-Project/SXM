using CitizenFX.Core;
using Sxm.Core;

namespace Sxm.Economy.Client;

public interface IPing;

public class Ping : IPing
{
    [Cmd("ping")]
    public void PingCmd(int count)
    {
        Debug.WriteLine("Pong: " + count);
    }
}