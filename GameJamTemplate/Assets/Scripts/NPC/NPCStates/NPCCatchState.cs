using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[Serializable]
public class NPCCatchState : BaseState
{
    public float CatchDistance;
    public float MinHitTime;
    public float MaxHitTime;

    private float _leaveTime;
    private bool _isStopped;

    private PlayerStats _playerStats;


    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnEnterState");
        var npcStateMachine = controller as NPCStateMachine;
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        _leaveTime = Time.time + UnityEngine.Random.Range(MinHitTime, MaxHitTime);

        npcStateMachine.SetAgentSpeedMultiplier(1f);

    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;
        npcStateMachine.SetDestination(npcStateMachine.PlayerPosition);

        if(_isStopped)
        {
            if(Time.time > _leaveTime)
            {
                npcStateMachine.agent.enabled = true;
                _isStopped = false;
            }
        }

        // Transitions
        // Can't see or hear player
        if (!npcStateMachine.CanHearPlayer && !npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }

        // When Player is hit > IdleState
        if (Vector3.Distance(npcStateMachine.NPCPosition, npcStateMachine.PlayerPosition) <= CatchDistance)
        {
            _playerStats.SetValues(0, _playerStats.StatValues(true) - npcStateMachine.Damage);

            npcStateMachine.agent.enabled = false;
            _isStopped = true;
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnExitState");
        var npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1f);
    }
}
