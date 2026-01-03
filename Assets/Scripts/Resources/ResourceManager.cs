using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static event Action Updated;

    public static ResourceManager Instance { private set; get; }


    private Dictionary<string, ResourcePack> resources = new();

    public ResourcePack[] AllResources
    {
        get => resources.Values.ToArray();
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        
        Instance = this;
    }

    public bool Contains(ResourceData resource, int amount)
    {
        return resources.ContainsKey(resource.resourceName) && resources[resource.resourceName].amount >= amount;
    }

    public void Add(ResourceData resource, int amount)
    {
        if (!resources.ContainsKey(resource.resourceName))
        {
            resources.Add(
                resource.resourceName, 
                new ResourcePack( 
                    amount: amount, 
                    resource: resource)
            );

            Debug.Log(resources.ToString());
            Updated?.Invoke();
            return;
        }


        resources[resource.resourceName].amount += amount;
        Updated?.Invoke();
    }

    public bool Rmv(ResourceData resource, int amount)
    {
        if (!resources.ContainsKey(resource.resourceName)) return false;

        if (resources[resource.resourceName].amount >= amount)
        {
            resources[resource.resourceName].amount -= amount;
            if (resources[resource.resourceName].amount == 0)
            {
                resources.Remove(resource.resourceName);
            }
            Updated?.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }


 
    [Serializable]
    public class ResourcePack
    {
        public ResourcePack(int amount, ResourceData resource)
        {
            this.amount = amount;
            this.resource = resource;
        }

        public ResourceData resource;
        public int amount;
    }
}
