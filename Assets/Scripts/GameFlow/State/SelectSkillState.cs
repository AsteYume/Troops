using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkillState : State
{
    private const StateEnum Type = StateEnum.SelectSkill;
    private InputReader _inputReader;
    private StateContext _stateContext;
    private SkillSelectUI _skillSelectUI;
    private RangeViewer _rangeViewer;
    
    // ============================================================

    public override StateEnum StateType => Type;
    public override event Action<StateEnum> StateEnd;
    
    // ============================================================

    public SelectSkillState(InputReader inputReader, StateContext stateContext,  SkillSelectUI skillSelectUI, RangeViewer rangeViewer)
    {
        _rangeViewer = rangeViewer;
        _inputReader = inputReader;
        _stateContext = stateContext;
        _skillSelectUI = skillSelectUI;
    }
    
    // ============================================================
    
    public override void OnEnter()
    {
        _rangeViewer.ClearViewerObject();
        List<Vector2Int> tile = new List<Vector2Int>();
        tile.Add(_stateContext.SelectVector);
        _rangeViewer.ListView(tile,MarkerType.UnitGreen);
        
        _skillSelectUI.OnSkillSelected += OnSelect;
        _inputReader.Confirm += OnConfirm;
        _inputReader.Cancel += OnCancel;
        _skillSelectUI.OpenSkillUI(_stateContext.SelectUnit);
    }

    public override void OnExit()
    {
        _rangeViewer.ClearViewerObject();
        _skillSelectUI.OnSkillSelected -= OnSelect;
        _inputReader.Confirm -= OnConfirm;
        _inputReader.Cancel -= OnCancel;
        _skillSelectUI.CloseSkillUI();
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

    public void OnSelect(SkillSlotEnum slot)
    {
        if (!_stateContext.SelectUnit.CheckCost(slot)) return;
        _stateContext.SetSkill(_stateContext.SelectUnit.GetSkill(slot));
        StateEnd?.Invoke(StateEnum.SelectRange);
    }

    private void SetIdleState()
    {
        _stateContext.SetList(null);
        _stateContext.SetVector(default);
        _stateContext.SetUnit(null);
        StateEnd?.Invoke(StateEnum.Idle);
    }
}
