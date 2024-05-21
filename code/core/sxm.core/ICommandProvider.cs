using Sxm.Core.Descriptors;

namespace Sxm.Core;

public interface ICommandProvider
{
    CommandDescriptor? GetCommand(string name);
}