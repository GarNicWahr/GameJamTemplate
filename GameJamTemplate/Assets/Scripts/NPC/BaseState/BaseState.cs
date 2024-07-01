using UnityEngine;

/// <summary>
/// Base class for all states used in our state machines
/// </summary>
public abstract class BaseState
{
    /// <summary>
    /// Called once when state entered 
    /// </summary>
    /// <param name="controller">Controlling state machine</param>
    public virtual void OnEnterState(BaseStateMachine controller) { }

    /// <summary>
    /// Called every frame till the state is active
    /// </summary>
    /// <param name="controller">Controlling state machine</param>
    public virtual void OnExitState(BaseStateMachine controller) { }

    /// <summary>
    /// Called once when state left
    /// </summary>
    /// <param name="controller">Controlling state machine</param>
    public abstract void OnUpdateState(BaseStateMachine controller);
}
