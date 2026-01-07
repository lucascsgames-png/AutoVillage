using UnityEngine;

public class Storage : MonoBehaviour, IStorage
{
    public void Add(ResourceData resource, int amount) 
        => ResourceManager.Instance.Add(resource, amount);

    public bool Rmv(ResourceData resource, int amount) 
        => ResourceManager.Instance.Rmv(resource, amount);
    
    public bool Contains(ResourceData resource, int amount) 
        => ResourceManager.Instance.Contains(resource, amount);
}
