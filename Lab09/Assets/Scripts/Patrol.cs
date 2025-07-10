using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    private int currentWaypoint = 0;

    public Patrol(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] waypoints)
        : base(npc, agent, anim, player, waypoints)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        agent.isStopped = false;
        anim.SetBool("isRunning", true);
        anim.SetBool("isShooting", false);
        

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[currentWaypoint].position);

        base.Enter();
    }

    public override void Update()
    {
    
        if (Vector3.Distance(npc.transform.position, player.position) < 10f)
        {
            nextState = new PursueFixed(npc, agent, anim, player, waypoints);
            stage = EVENT.EXIT;
            return;
        }


        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
