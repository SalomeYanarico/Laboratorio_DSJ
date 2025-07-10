using UnityEngine;
using UnityEngine.AI;

public class PursueFixed : State
{
    public PursueFixed(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] waypoints)
        : base(npc, agent, anim, player, waypoints)
    {
        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        // Asegúrate de que el agente esté habilitado y no detenido
        if (agent != null)
        {
            agent.isStopped = false;
            agent.enabled = true;
        }

        anim.SetBool("isRunning", true);
        anim.SetBool("isShooting", false);
        base.Enter();
    }

    public override void Update()
    {
        float distance = Vector3.Distance(npc.transform.position, player.position);

        // Asegura que el agente esté activo y funcionando
        if (!agent.enabled || agent.isStopped)
        {
            agent.enabled = true;
            agent.isStopped = false;
        }

        // Asigna el destino correctamente
        if (agent.isOnNavMesh && agent.destination != player.position)
        {
            agent.SetDestination(player.position);
        }

        // Rotación hacia el jugador (opcional, ya que NavMeshAgent maneja esto)
        Vector3 direction = (player.position - npc.transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, lookRotation, Time.deltaTime * 5);
        }

        // Transición a Attack cuando esté cerca
        if (distance <= 7f)
        {
            nextState = new Attack(npc, agent, anim, player, waypoints);
            stage = EVENT.EXIT;
        }
        // Transición a Patrol si el jugador está muy lejos
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