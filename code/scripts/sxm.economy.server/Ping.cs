using CitizenFX.Core;
using Sxm.Core;
using Sxm.MongoDB.Repositories.Collections.Users;

namespace Sxm.Economy.Server;

public interface IPing;

public class Ping(UserRepository users) : IPing
{
    [Cmd("ping")]
    public void PingCmd(int count)
    {
        Debug.WriteLine("Pong: " + count);

        var user = new User
        {
            Id = 27
        };

        users.Add(user);
    }
}