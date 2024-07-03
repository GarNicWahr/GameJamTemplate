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
    private AudioSource _audioSource;


    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCAttackState:OnEnterState");
        var npcStateMachine = controller as NPCStateMachine;
        
        _playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        _audioSource = GameObject.Find("HitSound").GetComponent<AudioSource>();
        _leaveTime = Time.time + UnityEngine.Random.Range(MinHitTime, MaxHitTime);

        npcStateMachine.SetAgentSpeedMultiplier(0);

        // Play Hit-Sound
        _audioSource.PlayOneShot(_audioSource.clip);

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
