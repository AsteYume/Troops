using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectMoveState : State
{
    private InputReader _inputReader;
    private RangeViewer _rangeViewer;
    private StateContext _stateContext;
    
    // ============================================================

    public SelectMoveState(InputReader inputReader, RangeViewer rangeViewer,  StateContext context)
    {
        _inputReader = inputReader;
        _rangeViewer = rangeViewer;
        _stateContext = context;
    }
    
    // ============================================================
    
    public override event Action<StateEnum> StateEnd;
    
    // ============================================================
    public override void OnEnter()
    {
        _rangeViewer.CalculateMovement(_stateContext.SelectVector, _stateContext.SelectUnit.MoveRange, out var list);
        _stateContext.SetList(list);
        _inputReader.Confirm += OnConfirm;
        _inputReader.Cancel += OnCancel;
    }

    public override void OnExit()
    {
        _inputReader.Confirm -= OnConfirm;
        _inputReader.Cancel -= OnCancel;
    }
    
    // ============================================================
    
    // 우클릭시엔 - IdleState로
    private void OnCancel()
    {
        SetIdleState();
    }
    
    private void OnConfirm()
    {
        if (!_inputReader.TryGetVector(out Vector2Int vec)) return;
        if (_stateContext.SelectVector == vec) // 클릭한곳이 자기 자신 위치면 - 스킬준비
        {
            ClearView();
            StateEnd?.Invoke(StateEnum.SelectSkill);
            return;
        }
        if (_stateContext.TilesInRange.Contains(vec)) // 클릭한곳이 이동 범위 이내면 - 이동실행
        {
            _stateContext.SetVector(vec);
            StateEnd?.Invoke(StateEnum.Move);
            return;
        }
        if (StageData.Instance.TryGetUnit(vec.x, vec.y, out var unit)) // 클릭한곳이 다른 유닛이면 - IdleState과 똑같이 처리
        {
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
            ClearView();
            _stateContext.SetVector(vec);
            _stateContext.SetUnit(unit);
            StateEnd?.Invoke(nextState);
            return;
        }
        SetIdleState(); // 그 외엔 - IdleState로
    }

    private void SetIdleState()
    {
        ClearView();
        _stateContext.SetVector(default);
        _stateContext.SetUnit(null);
        StateEnd?.Invoke(StateEnum.Idle);
    }
    

    private void ClearView()
    {
        _rangeViewer.ClearViewerObject();
        _stateContext.SetList(null);
    }
    
    // ============================================================
    
    
}
