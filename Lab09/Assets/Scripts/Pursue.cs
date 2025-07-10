using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    public Pursue(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] waypoints)
        : base(npc, agent, anim, player, waypoints)
    {
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        agent.isStopped = false;
        anim.SetBool("isRunning", true);
        anim.SetBool("isShooting", false);
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(npc.transform.position, player.position);

        if (distance < 6f)
        {
            nextState = new Attack(npc, agent, anim, player, waypoints);
            stage = EVENT.EXIT;
        }
        else if (distance > 15f)
        {
            nextState = new Patrol(npc, agent, anim, player, waypoints);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("isRunning", false);
        base.Exit();
    }
}
