using System;
using UnityEngine;

public abstract class Routine
{
    public abstract event Action<RoutineStatus> End;

    public abstract void Start();
    public abstract void Update();
    public abstract void Exit();
}

public enum RoutineStatus
{
    COMPLETED, FAIL
}
