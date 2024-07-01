using System;
using UnityEngine;

[Serializable]
public class NPCFleeState : BaseState
{
    public float fleeDistance;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnEnterState");
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;
        npcStateMachine.SetDestination((npcStateMachine.NPCPosition - npcStateMachine.PlayerPosition).normalized * fleeDistance + npcStateMachine.NPCPosition);

        // Transitions
        // Can see or hear player > switch to hide
        if (npcStateMachine.CanHearPlayer || npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.HideState);
        }

    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCFleeState:OnExitState");
    }
}
