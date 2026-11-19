using UnityEngine;

public class PreparePhase : Phase
{
    private PhaseEnum _phaseType = PhaseEnum.Prepare;
    
    // ============================================================
    public override PhaseEnum PhaseType => _phaseType;
    
    // ============================================================
    
    public override void OnPhaseStart(PhaseEnum phaseType)
    {
        if (phaseType != _phaseType) return;
        MapManager.Instance.StageStart(StageNumber);
        UnitManager.Instance.StageStart(StageNumber);
    }
    
    public override void OnPhaseEnd(PhaseEnum phaseType)
    {
        
    }
    
    // ============================================================
}
