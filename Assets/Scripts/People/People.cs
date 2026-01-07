using UnityEngine;

public class People : MonoBehaviour
{
    [SerializeField] public GameObject home;
    [SerializeField] public GameObject model;

    PeopleStatus status = new PeopleStatus
    {
        happyness = 100,
        hunger = 0,
        tiredness = 0,
    };

    MovementComponent movement;

    CarrierRoutine _carrierRoutine = null;
    RestRoutine _restRoutine = null; // ir descansar

    Routine _currentRoutine = null;

    public bool InDestiny => movement.InDestiny;

    private void Start()
    {
        movement = GetComponent<MovementComponent>();

        _restRoutine = new(this, home);

        SetRoutine(_restRoutine);
    }
    private void Update()
    {
        UpdateStatus(Time.deltaTime);

        if (_currentRoutine != null) _currentRoutine.Update();
        else
        {
            if (status.hunger == 0)
            {
                SetRoutine(_restRoutine);
            }
            else if (status.tiredness == 100)
            {
                SetRoutine(_restRoutine);
            }
            else if (status.happyness == 0)
            {
                //Se Divertir
            }
            else
            {
                //Trabalhar
            }
        }
    }


    public void SetRoutine(Routine routine)
    {
        if (_currentRoutine != null)
        {
            _currentRoutine.End -= RoutineCompleted;
            _currentRoutine.Exit();
        }

        _currentRoutine = routine;
        _currentRoutine.End += RoutineCompleted;
        _currentRoutine.Start();
    }



    private void RoutineCompleted(RoutineStatus routineStatus)
    {
        if(routineStatus == RoutineStatus.COMPLETED)
        {
            if (_currentRoutine == _restRoutine)
            {
                this.status.tiredness = 0;
                this.status.hunger = 0;
            }
        }
    }

    public void Hide() => model.SetActive(false);
    public void Show() => model.SetActive(true);
    
    public void StopMovement() => movement.Stop();
    public void MoveTo(Vector3 destiny) => movement.SetDestiny(destiny);

    private void UpdateStatus(float delta, float multiply = 1f)
    {
        status.happyness -= delta * multiply;
        status.tiredness += delta * multiply;
        status.hunger += delta * multiply;
    }

    private struct PeopleStatus
    {
        public float hunger;
        public float happyness;
        public float tiredness;
    }
}