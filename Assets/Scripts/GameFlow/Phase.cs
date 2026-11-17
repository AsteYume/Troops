public abstract class Phase
{
    protected PhaseManager Manager = PhaseManager.Instance;
    protected int StageNumber => Manager.StageNumber;
    
    // ============================================================
    public abstract PhaseEnum PhaseType { get; }
    
    // ============================================================
    public abstract void OnPhaseStart(PhaseEnum phaseType);
    public abstract void OnPhaseEnd(PhaseEnum phaseType);
    
    // ============================================================

    protected Phase()
    {
        Manager.PhaseStart += OnPhaseStart;
    }

}
