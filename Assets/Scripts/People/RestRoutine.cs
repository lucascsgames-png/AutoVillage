using System;
using System.Collections;
using UnityEngine;

public class RestRoutine : Routine
{
    public override event Action<RoutineStatus> End;

    private People _people;
    private GameObject _home;
    private Coroutine sleep;

    [SerializeField]
    private bool sleeping = false;

    public RestRoutine(People people,GameObject home)
    {
        _people = people;
        _home = home;
    }

    public override void Exit()
    {
        Debug.Log("Saindo de RestRoutine.");
        _people.Show();

        if(sleeping) sleeping = false;

         _people.StopMovement();
    }

    public override void Start()
    {
        Debug.Log("Entrando em RestRoutine.");
        Debug.Log("Voltando para casa...");
        _people.MoveTo(_home.transform.position);
    }

    public override void Update()
    {
        if (!_people.InDestiny || sleeping) return;
        
        _people.Hide();
        sleep = _people.StartCoroutine(Sleep());
    }

    private IEnumerator Sleep()
    {
        sleeping = true;
        float timer = 0;
        
        Debug.Log("Indo dormir");
        
        while(timer < 30 && sleeping)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        
        Debug.Log("Acordando");
        sleeping = false;

        End?.Invoke(RoutineStatus.COMPLETED);
    }

}
