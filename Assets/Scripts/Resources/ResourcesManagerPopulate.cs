using System;
using UnityEngine;

public class ResourcesManagerPopulate : MonoBehaviour
{
    [SerializeField] public ResourcePack[] initialPacks;

    private void Start()
    {
        foreach(var item in initialPacks)
        {
            ResourceManager.Instance.Add(item.resource, item.amount);
        }
    }


    [Serializable] public class ResourcePack
    {
        public ResourceData resource;
        public int amount;
    }
}
