using UnityEngine;

public class PreparePhase : Phase
{
    private PhaseEnum _phaseType = PhaseEnum.Prepare;
    
    // ============================================================
    public override PhaseEnum PhaseType => _phaseType;
    
    // ============================================================
    
    public override void OnPhaseStart(PhaseEnum phaseType)
    {
        Debug.Log(phaseType);
        if (phaseType != _phaseType) return;
        MapManager.Instance.StageStart(StageNumber);
    }
    
    public override void OnPhaseEnd(PhaseEnum phaseType)
    {
        
    }
    
    // ============================================================
}
