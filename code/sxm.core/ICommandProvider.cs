namespace Sxm.Core;

public interface ICommandProvider
{
    CommandDescriptor? GetCommand(string name);
}