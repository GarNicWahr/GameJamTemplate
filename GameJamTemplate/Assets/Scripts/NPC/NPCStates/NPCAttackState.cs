using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[Serializable]
public class NPCAttackState : BaseState
{
    public float MinHitTime;
    public float MaxHitTime;

    private float _leaveTime;
    private bool _isStopped;

    private PlayerStats _playerStats;


    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCAttackState:OnEnterState");
        var npcStateMachine = controller as NPCStateMachine;
        
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        _leaveTime = Time.time + UnityEngine.Random.Range(MinHitTime, MaxHitTime);

        npcStateMachine.SetAgentSpeedMultiplier(0);

        // Do damage to player
        _playerStats.SetValues(0, _playerStats.StatValues(true) - npcStateMachine.Damage);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCAttackState:OnUpdateState");
        var npcStateMachine = controller as NPCStateMachine;
        


        if (Time.time > _leaveTime)
        {
            npcStateMachine.SwitchToState(npcStateMachine.CatchState);
        }
        
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCAttackState:OnExitState");
        var npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1f);

    }
}
