using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    public enum STATE { IDLE, PATROL, PURSUE, ATTACK }
    public enum EVENT { ENTER, UPDATE, EXIT }

    public STATE name;
    protected EVENT stage;

    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Transform player;
    protected Transform[] waypoints;

    protected State nextState;

    public State(GameObject npc, NavMeshAgent agent, Animator anim, Transform player, Transform[] waypoints = null)
    {
        this.npc = npc;
        this.agent = agent;
        this.anim = anim;
        this.player = player;
        this.waypoints = waypoints;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() => stage = EVENT.UPDATE;
    public virtual void Update() => stage = EVENT.UPDATE;
    public virtual void Exit() => stage = EVENT.EXIT;

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
