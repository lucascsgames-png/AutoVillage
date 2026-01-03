using System;
using UnityEngine;

public interface IFactory
{
    public event Action<IFactory> Updated;

    public UnitData UnitData { get; }
    public ResourceData ResourceData { get; }
    public float ProductionRate { get; }
    public float CurrentStorage { get; }
    public float CapacityStorage { get; }
    public bool Working { get; }
}