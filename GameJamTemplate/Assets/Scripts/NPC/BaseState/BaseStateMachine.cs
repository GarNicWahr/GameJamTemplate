using UnityEngine;

/// <summary>
/// Base class for all state machines
/// </summary>
public abstract class BaseStateMachine : MonoBehaviour
{
    // State that is currently being executed
    public BaseState CurrentState { get; protected set; }

    void Start()
    {
        Initialize();    
    }

    void Update()
    {        
        CurrentState?.OnUpdateState(this);
        Tick();
    }

    /// <summary>
    /// Change to a given state
    /// </summary>
    /// <param name="nextState">Next state</param>
    public void SwitchToState(BaseState nextState) 
    {
        CurrentState?.OnExitState(this);
        CurrentState = nextState;
        CurrentState.OnEnterState(this);
    }

    /// <summary>
    /// Called when statemachine started
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Called every frame 
    /// </summary>
    public abstract void Tick();
}
