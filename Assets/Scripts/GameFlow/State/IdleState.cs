using System;
using UnityEngine;

public class IdleState : State
{
    private const StateEnum Type =  StateEnum.Idle;
    private InputReader _inputReader;
    private StateContext _stateContext;
    private Vector2Int _vec;
    private Unit _unit;
    
    // ============================================================

    public override event Action<StateEnum> StateEnd;
    public override StateEnum StateType => Type;

    // ============================================================

    public IdleState (InputReader inputReader, StateContext stateContext)
    {
        _inputReader = inputReader;
        _stateContext = stateContext;
    }
    
    // ============================================================
    
    public override void OnEnter()
    {
        Debug.Log("Entering IdleState");
        _inputReader.Confirm += OnConfirm;
    }

    public override void OnExit()
    {
        if (_vec.x != -1) _stateContext.SetVector(_vec);
        if (_unit != null) _stateContext.SetUnit(_unit);
        ClearData();
        _inputReader.Confirm -= OnConfirm;
    }
    
    // ============================================================
    

    private void OnConfirm()
    {
        if (!_inputReader.TryGetVector(out Vector2Int vec)) return;
        if (!StageData.Instance.TryGetUnit(vec.x, vec.y, out var unit)) return;
        if (unit.Team == UnitTeam.Blue && !unit.CanAction) return;
        StateEnum nextState;
        _vec = vec;
        _unit = unit;
        switch (unit.Team)
        {
            case UnitTeam.Blue:
                nextState = unit.HasMoved ? StateEnum.SelectSkill : StateEnum.SelectMove;
                break;
            case UnitTeam.Red:
                nextState = StateEnum.EnemySight;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        StateEnd?.Invoke(nextState);
    }

    private void ClearData()
    {
        _vec = new Vector2Int(-1, -1);
        _unit = null;
    }
    
}
