public abstract class Phase
{
    public abstract PhaseEnum PhaseType { get; }
    public abstract void OnPhaseStart();
    public abstract void OnPhaseEnd();

}
