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

        npcStateMachine.SetAgentSpeedMultiplier(1.25f);

    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        var npcStateMachine = controller as NPCStateMachine;
        float distance = Vector3.Distance(npcStateMachine.transform.position + new Vector3(0, 0.5f, 0), (npcStateMachine.PlayerPosition + new Vector3(0, 0.5f, 0)));

        //Debug.Log("Distanz ist:" + distance);
        //Debug.Log("NPCCatchState:OnUpdateState>>"+distance+ ", CatchDistance:"+ CatchDistance);

        /* if (npcStateMachine.CanHearPlayer || npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SetDestination(npcStateMachine.PlayerPosition);
        }
        */
        npcStateMachine.SetDestination(npcStateMachine.PlayerPosition);
        

        // When Player is hit > IdleState
        if (distance <= CatchDistance)
        {
            npcStateMachine.SwitchToState(npcStateMachine.AttackState);
        }
        
        /*
        // Can't see or hear player
        if (!npcStateMachine.CanHearPlayer && !npcStateMachine.CanSeePlayer)
        {
            npcStateMachine.SwitchToState(npcStateMachine.IdleState);
        }
        */
        
        
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCCatchState:OnExitState");
        var npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1f);
    }
}
