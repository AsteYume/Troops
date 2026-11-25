using System;
using UnityEngine;

public class EnemySightState : State
{
    private const StateEnum Type = StateEnum.EnemySight;
    private RangeViewer _rangeViewer;
    private InputReader _inputReader;
    private StateContext _stateContext;
    
    // ============================================================

    public EnemySightState(RangeViewer rangeViewer, InputReader inputReader, StateContext stateContext)
    {
        _rangeViewer = rangeViewer;
        _inputReader = inputReader;
        _stateContext = stateContext;
    }
    
    // ============================================================
    
    public override StateEnum StateType => Type;
    public override event Action<StateEnum> StateEnd;
    
    // ============================================================
    
    public override void OnEnter()
    {
        _inputReader.Confirm += OnConfirm;
        _inputReader.Cancel += OnCancel;
        _rangeViewer.BfsView(_stateContext.SelectVector
            , _stateContext.SelectUnit.Sight, 0, UnitTeam.None, MarkerType.TileRed);
    }

    public override void OnExit()
    {
        _inputReader.Confirm -= OnConfirm;
        _inputReader.Cancel -= OnCancel;
        _rangeViewer.ClearViewerObject();
    }
    // ============================================================

    private void OnCancel()
    {
        SetIdleState();
    }
    
    private void OnConfirm()
    {
        if (!_inputReader.TryGetVector(out Vector2Int vec)) return;
        if (vec == _stateContext.SelectVector) return;
        if (!StageData.Instance.TryGetUnit(vec.x, vec.y, out var unit))
        {
            SetIdleState();
            return;
        }
        StateEnum nextState;
        switch (unit.Team)
        {
            case UnitTeam.Blue:
                if (!unit.CanAction) return;
                nextState = unit.HasMoved ? StateEnum.SelectSkill : StateEnum.SelectMove;
                break;
            case UnitTeam.Red:
                nextState = StateEnum.EnemySight;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _stateContext.SetVector(vec);
        _stateContext.SetUnit(unit);
        StateEnd?.Invoke(nextState);
    }

    private void SetIdleState()
    {
        _rangeViewer.ClearViewerObject();
        _stateContext.SetList(null);
        _stateContext.SetVector(default);
        _stateContext.SetUnit(null);
        StateEnd?.Invoke(StateEnum.Idle);
    }
    
    // ============================================================
}
