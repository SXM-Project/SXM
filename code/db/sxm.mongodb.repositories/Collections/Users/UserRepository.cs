using Sxm.Core.Attributes;

namespace Sxm.MongoDB.Repositories.Collections.Users;

public sealed class UserRepository(SxmDb db) : RepositoryBase<User>(db)
{
    public override string CollectionName => "users";
}