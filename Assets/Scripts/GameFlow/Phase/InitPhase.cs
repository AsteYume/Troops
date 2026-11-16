using UnityEngine;

public class InitPhase : Phase
{
    private PhaseEnum _phaseType = PhaseEnum.Init;
    
    
    public override PhaseEnum PhaseType => _phaseType;
    
    public override void OnPhaseStart()
    {
        throw new System.NotImplementedException();
    }

    public override void OnPhaseEnd()
    {
        throw new System.NotImplementedException();
    }
}
