using UnityEngine;

public interface IStorage
{
    public void Add(ResourceData resource, int amount);
    public bool Rmv(ResourceData resource, int amount);
    public bool Contains(ResourceData resource, int amount);
}
