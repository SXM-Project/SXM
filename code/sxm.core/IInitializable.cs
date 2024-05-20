using System.Reflection;

namespace Sxm.Core;

public interface IInitializable
{
    void Initialize(params Assembly[] assemblies);
}