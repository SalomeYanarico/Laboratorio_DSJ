using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    public Attack(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] waypoints)
        : base(npc, agent, anim, player, waypoints)
    {
        name = STATE.ATTACK;
    }



    public override void Enter()
    {
        agent.isStopped = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isShooting", true);
        base.Enter();
    }

    public override void Update()
    {
        float distance = Vector3.Distance(npc.transform.position, player.position);

        Vector3 direction = (player.position - npc.transform.position).normalized;
        direction.y = 0; 
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, lookRotation, Time.deltaTime * 5);
        }

        if (distance > 7f)
        {
            nextState = new PursueFixed(npc, agent, anim, player, waypoints);
            stage = EVENT.EXIT;
        }
        else if (distance >= 15f)
        {
            nextState = new Patrol(npc, agent, anim, player, waypoints);
            stage = EVENT.EXIT;
        }
    }


    public override void Exit()
    {
        anim.SetBool("isShooting", false);
        base.Exit();
    }
}
