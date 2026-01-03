using System;
using System.Collections;
using UnityEngine;

public class ResourceFactory : MonoBehaviour, IFactory
{
    //IFactory properties  
    public UnitData UnitData => unit?.Data;
    public ResourceData ResourceData => resource;
    public float ProductionRate => productionRate;
    public float CurrentStorage => currentStorage;
    public float CapacityStorage => capacityStorage;

    public bool Working => inWork && production != null;

    public event Action<IFactory> Updated;


    [SerializeField] private Unit unit;

    [SerializeField] private ResourceData resource;
    [SerializeField] private float productionRate; //per minute

    [SerializeField] private float currentStorage;
    [SerializeField] private float capacityStorage;

    [SerializeField] private bool inWork = true;
    [SerializeField] Coroutine production;


    private void Start()
    {
        unit = GetComponent<Unit>();
        TryStartProduction();
    }

    
    public ResourceManager.ResourcePack TryGetResource(int requestAmount)
    {
        if (currentStorage == 0) return null;

        int amount = currentStorage >= requestAmount ? requestAmount : (int) currentStorage;
        currentStorage -= amount;

        TryStartProduction();

        Updated?.Invoke(this);
        return new ResourceManager.ResourcePack(resource: resource, amount: amount);
    }


    private void TryStartProduction()
    {
        if (!inWork && production == null && isAvaliableProduction())
        {
            inWork = true;
            production = StartCoroutine(Production(resource));
        }
    }

    private void AddResource(float amount)
    {
        currentStorage += amount;
        currentStorage = Mathf.Clamp(currentStorage, 0, capacityStorage);
        Updated?.Invoke(this);

    }

    private bool isAvaliableProduction()
    {
        //Estoque cheio
        if (currentStorage >= capacityStorage) return false;

        return true;
    }


    private IEnumerator Production(ResourceData data)
    {
        Updated?.Invoke(this);
        Debug.Log($"{unit.Data.unitName} : Produção iniciada.");
        
        while (inWork)
        {
            yield return new WaitForSeconds(1f);
            AddResource(amount: productionRate / 60f);
            inWork = isAvaliableProduction();
        }

        production = null;
        Updated?.Invoke(this);
        Debug.Log($"{unit.Data.unitName} : Produção finalizada.");
    }
}