using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class NPCCatchState : BaseState
{
    public float CatchDistance;

    public float Damage;

    private PlayerStats _playerStats;
    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnEnterState");
        var npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1.5f);

        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;
        npcStateMachine.SetDestination(npcStateMachine.PlayerPosition);

        if (Vector3.Distance(npcStateMachine.NPCPosition, npcStateMachine.PlayerPosition) <= CatchDistance)
        {
            _playerStats.SetValues(0, _playerStats.StatValues(true) - Damage);
        }
        
        // Transitions
        // Can't see or hear player
        if (!npcStateMachine.CanHearPlayer && !npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }
        
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnExitState");
        var npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1f);
    }
}
