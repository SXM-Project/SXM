using CitizenFX.Core;
using Sxm.Core;
using Sxm.Core.Attributes;
using Sxm.MongoDB.Repositories.Collections.Users;

namespace Sxm.Economy.Server;

public interface IPing;

public class Ping : IPing
{
    [Cmd("ping")]
    public void PingCmd(int count)
    {
        Debug.WriteLine("Pong: " + count);
    }
}