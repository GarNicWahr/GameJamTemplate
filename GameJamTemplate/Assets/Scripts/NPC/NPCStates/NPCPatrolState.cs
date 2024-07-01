using System;
using UnityEngine;

[Serializable]
public class NPCPatrolState : BaseState
{
    public Transform[] Waypoints;

    private int currentWaypointIndex;

    private Vector3 targetPosition;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnEnterState");
        var npcStateMachine = controller as NPCStateMachine;

        if(targetPosition == Vector3.zero)
        {
            targetPosition = Waypoints[0].position;
        }

        // StateMachine sagen das der NavAgent bewegt wird
        npcStateMachine.SetDestination(targetPosition);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;

        float sqrDistance = (npcStateMachine.transform.position - targetPosition).sqrMagnitude;

        //Transitions
        //NPC reached Waypoint? > Switch to Idle
        if(sqrDistance < 0.1f)
        {
            targetPosition = GetNextWaypoint();
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }

        //Can see or hear player and the NPC should flee > Switch to flee
        if (npcStateMachine.isFleeing)
        {
            if (npcStateMachine.CanHearPlayer || npcStateMachine.CanSeePlayer)
            {
                npcStateMachine.SwitchToState(npcStateMachine.FleeState);
            }
        }
        //Can see or hear player and the NPC should not flee  > Switch to catch
        if (!npcStateMachine.isFleeing)
        {
            if (npcStateMachine.CanHearPlayer || npcStateMachine.CanSeePlayer)
            {
                npcStateMachine.SwitchToState(npcStateMachine.CatchState);
            }
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnExitState");
    }

    public Vector3 GetNextWaypoint()
    {
        currentWaypointIndex = ++currentWaypointIndex % Waypoints.Length;
        return Waypoints[currentWaypointIndex].position;
    }
}
