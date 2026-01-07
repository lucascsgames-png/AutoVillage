using System;

public class CarrierRoutine : Routine
{
    public People _people;
    public Storage _storage;

    public override event Action<RoutineStatus> End;

    public CarrierRoutine(People people, Storage storage)
    {
        _people = people;
        _storage = storage;
    }


    public override void Exit()
    {
        
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }
}
