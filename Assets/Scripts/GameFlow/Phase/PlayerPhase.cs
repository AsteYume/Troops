using System;
using UnityEngine;

public class PlayerPhase : Phase
{
    private const PhaseEnum ThisPhase = PhaseEnum.Player;

    public override PhaseEnum PhaseType => ThisPhase;
    
    public PlayerPhase(PhaseManager manager) : base(manager) {}
    
    public override event Action<PhaseEnum> PhaseChange;

    public event Action PhaseStart; 
    
    protected override void OnStart(PhaseEnum phaseType)
    {
        if (phaseType != ThisPhase) return;
        PhaseStart?.Invoke();
    }

    protected override void OnEnd()
    {
        PhaseChange?.Invoke(PhaseEnum.Player);
    }

    public void PlayerPhaseEnd()
    {
        PhaseChange?.Invoke(PhaseEnum.Player);
    }
    
    
}
