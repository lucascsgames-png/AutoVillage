using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData Data => unitData;
    public IFactory Factory => factory;
    public IStorage Storage => storage;

    [SerializeField] private UnitData unitData;
    private IFactory factory;
    private IStorage storage;


    private void Start()
    {
        factory = GetComponent<IFactory>();
        storage = GetComponent<IStorage>();
    }

}
