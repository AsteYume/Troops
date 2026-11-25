using System;
using System.Data;
using Unity.AppUI.UI;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private int _stageNumber;
    private Phase _currentPhase;
    private PhaseEnum _currentPhaseEnum;
    private PreparePhase _preparePhase;
    // private PlayerPhase _player;
    // private EnemyPhase _enemy;
    // private ResultPhase _result;
    

    // ============================================================
    public event Action<PhaseEnum> PhaseStart;
    public event Action PhaseEnd;
    public int StageNumber => _stageNumber;

    // ============================================================

    private void Awake() => Init();

    private void OnDestroy()
    {
        PhaseEnd = null;
        PhaseStart = null;
    }

    // ============================================================

    public void OnStart(int stage)
    {
        _stageNumber = stage;
        StartPhase(PhaseEnum.Prepare);
    }
    
    // ============================================================
    
    private void OnChange(PhaseEnum phase)
    {
        EndPhase();
        StartPhase(phase);
    }

    private void EndPhase()
    {
        PhaseEnd?.Invoke();
        _currentPhase.PhaseChange -= OnChange;
    }
    
    private void StartPhase(PhaseEnum phase)
    {
        _currentPhaseEnum = phase;
        _currentPhase = GetPhase(phase);
        PhaseStart?.Invoke(_currentPhaseEnum);
        _currentPhase.PhaseChange += OnChange;
    }

    private Phase GetPhase(PhaseEnum phase)
    {
        switch (phase)
        {
            case PhaseEnum.Prepare:
                return _preparePhase;
            default:
            case PhaseEnum.Player:
            case PhaseEnum.Enemy:
            case PhaseEnum.Result:
            case PhaseEnum.None: 
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Init()
    {
        _preparePhase = new PreparePhase(this);
    }


}
