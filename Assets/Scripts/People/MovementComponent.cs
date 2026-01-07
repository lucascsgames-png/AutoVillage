using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] float distanceToStop;

    Vector3 destiny;

    bool isStoped => agent.isStopped;
    public bool InDestiny => agent.remainingDistance < distanceToStop;

    void Awake()  => 
        agent = GetComponent<NavMeshAgent>();

    void Update() {
        if(InDestiny && !isStoped) agent.isStopped = true;
    }

    public void Stop()
    {
        SetDestiny(transform.position);
    }
    public void SetDestiny(Vector3 destiny)
    {
        if (this.destiny == destiny) return;

        this.destiny = destiny;
        agent.isStopped = false;
        agent.SetDestination(destiny);
    }
}
