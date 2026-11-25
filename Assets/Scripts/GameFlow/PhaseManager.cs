using System;
using System.Data;
using Unity.AppUI.UI;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] StateManager _stateManager;
    
    private int _stageNumber;
    private Phase _currentPhase;
    private PhaseEnum _currentPhaseEnum;
    private PreparePhase _preparePhase;
    private PlayerPhase _playerPhase;
    // private EnemyPhase _enemyPhase;
    // private ResultPhase _resultPhase;
    

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

    private void Update()
    {
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
        _currentPhase.PhaseChange -= OnChange;
        PhaseEnd?.Invoke();
    }
    
    private void StartPhase(PhaseEnum phase)
    {
        _currentPhaseEnum = phase;
        _currentPhase = GetPhase(phase);
        _currentPhase.PhaseChange += OnChange;
        PhaseStart?.Invoke(_currentPhaseEnum);
    }

    private Phase GetPhase(PhaseEnum phase)
    {
        switch (phase)
        {
            case PhaseEnum.Prepare:
                return _preparePhase;
            case PhaseEnum.Player:
                return _playerPhase;
            default:
            case PhaseEnum.Enemy:
            case PhaseEnum.Result:
            case PhaseEnum.None: 
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Init()
    {
        _preparePhase = new PreparePhase(this);
        _playerPhase = new PlayerPhase(this);
        _stateManager.SetPhaseClass(_playerPhase);
    }


}
