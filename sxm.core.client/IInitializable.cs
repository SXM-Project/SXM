using System.Reflection;

namespace Sxm.Core.Client;

public interface IInitializable
{
    void Initialize(params Assembly[] assemblies);
}