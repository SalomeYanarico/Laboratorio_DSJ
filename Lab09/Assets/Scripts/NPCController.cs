using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform player;
    public Transform[] waypoints;

    private State currentState;
    private Animator anim;
    private NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = new Patrol(gameObject, agent, anim, player, waypoints);
    }

    void Update()
    {
        currentState = currentState.Process();
    }
}
