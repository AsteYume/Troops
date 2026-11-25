using System;

public abstract class Phase
{
    private PhaseManager _manager;
    
    // ============================================================
    public abstract PhaseEnum PhaseType { get; }
    protected PhaseManager Manager => _manager;
    
    // ============================================================
    public abstract event Action<PhaseEnum> PhaseChange;
    protected abstract void OnStart(PhaseEnum phaseType);
    protected abstract void OnEnd();
    
    // ============================================================
    
    public Phase (PhaseManager manager)
    {
        _manager = manager;
        _manager.PhaseStart += OnStart;
        _manager.PhaseEnd += OnEnd;
    }

}
