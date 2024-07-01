using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateMachine : BaseStateMachine
{
    public Vector3 PlayerPosition { get => _player.position; }

    public Vector3 NPCPosition { get => _npc.position; }

    public bool CanSeePlayer { get => _eyes.IsDetecting; }
    public bool CanHearPlayer { get => _ears.IsDetecting; }

    public bool isFleeing;

    public NPCIdleState IdleState;
    public NPCFleeState FleeState;
    public NPCHideState HideState;
    public NPCPatrolState PatrolState;
    public NPCCatchState CatchState;


    private Eyes _eyes;
    private Ears _ears;

    private Transform _player;
    private Transform _npc;
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _initialAgentSpeed;
    private int _isHidingParameterHash;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        WaypointGizmos.DrawWayPoints(PatrolState.Waypoints);
    }

#endif
    public override void Initialize()
    {
        _eyes = GetComponentInChildren<Eyes>();
        _ears = GetComponentInChildren<Ears>();

        _player = GameObject.FindWithTag("Player").transform;
        _npc = GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _isHidingParameterHash = Animator.StringToHash("isHiding");
        _initialAgentSpeed = _agent.speed;

        CurrentState = IdleState;
        CurrentState.OnEnterState(this);
    }

    public override void Tick()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude);
    }

    public void SetDestination (Vector3 destination)
    {
        _agent.SetDestination(destination);
    }

    public void SetAgentSpeedMultiplier(float multiplier)
    {
        _agent.speed = _initialAgentSpeed * multiplier;
    }

    public void SetHideAnimation(bool isHiding)
    {
        _animator.SetBool(_isHidingParameterHash, isHiding);
    }
}


