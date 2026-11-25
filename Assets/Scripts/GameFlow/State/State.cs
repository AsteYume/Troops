using System;

public abstract class State
{
    public abstract event Action<StateEnum> StateEnd;
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract StateEnum StateType { get; }
}
