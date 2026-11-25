using System;
using UnityEngine;

public class PreparePhase : Phase
{
    private const PhaseEnum ThisPhase = PhaseEnum.Prepare;
    
    // ============================================================

    public override PhaseEnum PhaseType => ThisPhase;
    private int StageNumber => Manager.StageNumber;
    
    // ============================================================

    public PreparePhase(PhaseManager manager) : base(manager) {}
    
    // ============================================================

    public override event Action<PhaseEnum> PhaseChange;

    protected override void OnStart(PhaseEnum phaseType)
    {
        if (phaseType != ThisPhase) return;
        InitStage();
        PhaseChange?.Invoke(PhaseEnum.Player);
    }

    protected override void OnEnd()
    {
        PhaseChange?.Invoke(PhaseEnum.Player);
    }

    private void InitStage()
    {
        StageData.Instance.SetStage(StageNumber);
        MapManager.Instance.StageStart(StageNumber);
        UnitManager.Instance.StageStart(StageNumber);
    }
    
    // ============================================================
}
