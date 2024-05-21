using System.Collections;
using Sxm.DependencyInjection.Descriptors;

namespace Sxm.DependencyInjection;

public sealed class ServiceCollection : IServiceCollection
{
    private readonly List<ServiceDescriptor> _descriptors = [];

    public int Count => _descriptors.Count;
    public bool IsReadOnly { get; private set; }

    public ServiceDescriptor this[int index]
    {
        get => _descriptors[index];
        set
        {
            CheckReadOnly();
            _descriptors[index] = value;
        }
    }
    
    void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
    {
        CheckReadOnly();
        _descriptors.Add(item);
    }

    public void Clear()
    {
        CheckReadOnly();
        _descriptors.Clear();
    }

    public bool Contains(ServiceDescriptor item)
    {
        return _descriptors.Contains(item);
    }

    public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
    {
        _descriptors.CopyTo(array, arrayIndex);
    }

    public bool Remove(ServiceDescriptor item)
    {
        CheckReadOnly();
        return _descriptors.Remove(item);
    }
    
    public IEnumerator<ServiceDescriptor> GetEnumerator()
    {
        return _descriptors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int IndexOf(ServiceDescriptor item)
    {
        return _descriptors.IndexOf(item);
    }

    public void Insert(int index, ServiceDescriptor item)
    {
        CheckReadOnly();
        _descriptors.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        CheckReadOnly();
        _descriptors.RemoveAt(index);
    }

    public void MakeReadOnly()
    {
        IsReadOnly = true;
    }

    private void CheckReadOnly()
    {
        if (!IsReadOnly) return;
        ThrowReadOnlyException();
    }

    private static void ThrowReadOnlyException() =>
        throw new InvalidOperationException("Service collection is read only.");
    
    public IReadOnlyList<ServiceDescriptor> GetServices() => _descriptors.ToList();
}