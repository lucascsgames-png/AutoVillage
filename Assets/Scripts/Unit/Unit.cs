using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData Data => unitData;

    [SerializeField] private UnitData unitData;
}
