using System;
using UnityEngine;

public class IdleState : State
{
    private InputReader _inputReader;
    private StateContext _stateContext;
    
    // ============================================================

    public override event Action<StateEnum> StateEnd;
    
    // ============================================================

    public IdleState (InputReader inputReader, StateContext stateContext)
    {
        _inputReader = inputReader;
        _stateContext = stateContext;
    }
    
    // ============================================================
    
    public override void OnEnter()
    {
        _inputReader.Confirm += OnClick;
    }

    public override void OnExit()
    {
        _inputReader.Confirm -= OnClick;
    }
    
    // ============================================================


    private void OnClick()
    {
        if (!_inputReader.TryGetVector(out Vector2Int vec)) return;
        if (!StageData.Instance.TryGetUnit(vec.x, vec.y, out var unit)) return;
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
        _stateContext.SetSelectVector(vec);
        _stateContext.SetSelectUnit(unit);
        StateEnd?.Invoke(nextState);
    }
    
}
