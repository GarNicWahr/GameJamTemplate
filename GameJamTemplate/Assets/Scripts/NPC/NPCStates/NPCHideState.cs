using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class NPCHideState : BaseState
{
    public Transform[] HidingSpots;
    public float MinHideTime;
    public float MaxHideTime;

    private Vector3 targetPosition;
    private float _leaveHiding;

    public override void OnEnterState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnEnterState");

        NPCStateMachine npcStateMachine = controller as NPCStateMachine;
        _leaveHiding = Time.time + UnityEngine.Random.Range(MinHideTime, MaxHideTime);

        targetPosition = GetNearestHidingSpot(npcStateMachine.transform.position);
        npcStateMachine.SetDestination(targetPosition);
        npcStateMachine.SetAgentSpeedMultiplier(2);
    }

    public override void OnUpdateState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnUpdateState");

        NPCStateMachine npcStateMachine = controller as NPCStateMachine;

        if (Vector3.Distance(npcStateMachine.NPCPosition, npcStateMachine.PlayerPosition) <= 1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Transitions
        // NPC reached waypoint? > Switch to idle
        float sqrtDistance = (npcStateMachine.transform.position - targetPosition).sqrMagnitude;
        //Debug.Log("dist: " + sqrtDistance);
        if (sqrtDistance < 3f) 
        {
            npcStateMachine.SetHideAnimation(true);
        }

        // NPC done hiding? Switch to Patrol
        if (Time.time > _leaveHiding)
        {
            npcStateMachine.SwitchToState(npcStateMachine.PatrolState);
        }
    }

    public override void OnExitState(BaseStateMachine controller)
    {
        Debug.Log("NPCPatrolState:OnExitState");
        NPCStateMachine npcStateMachine = controller as NPCStateMachine;

        npcStateMachine.SetAgentSpeedMultiplier(1f);

        npcStateMachine.SetHideAnimation(false);
    }

    public Vector3 GetNearestHidingSpot(Vector3 position) 
    {
        if (HidingSpots.Length < 2)
            return Vector3.zero;

        int shortestSqrtDistanceIndex = 0;
        float shortestSqrtDistance = (HidingSpots[0].position - position).sqrMagnitude;
        for (int i = 1; i < HidingSpots.Length; i++)
        {
            float sqrtDistance = (HidingSpots[i].position - position).sqrMagnitude;
            if (sqrtDistance < shortestSqrtDistance)
            {
                shortestSqrtDistance = sqrtDistance;
                shortestSqrtDistanceIndex = i;
            }
        }
        
        return HidingSpots[shortestSqrtDistanceIndex].position;
    }
}
