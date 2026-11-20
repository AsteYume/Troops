using System;
using UnityEngine;

public class SelectSkillState : State
{
    private InputReader _inputReader;
    private StateContext _stateContext;
    
    // ============================================================

    public override event Action<StateEnum> StateEnd;
    
    // ============================================================

    public SelectSkillState(InputReader inputReader, StateContext stateContext)
    {
        _inputReader = inputReader;
    }
    
    // ============================================================
    
    public override void OnEnter()
    {
        _inputReader.Confirm += OnConfirm;
        _inputReader.Cancel += OnCancel;
    }

    public override void OnExit()
    {
        _inputReader.Confirm -= OnConfirm;
        _inputReader.Cancel -= OnCancel;
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

    public void OnSkillSelect(int slot)
    {
        if (!_stateContext.SelectUnit.CheckCost(slot)) return;
        SkillSO skill = _stateContext.SelectUnit.GetSkill(slot);
        switch (skill.SkillType)
        {
            case SkillsEnum.Attack:
                _stateContext.SetWeapon((WeaponData)skill);
                break;
            case SkillsEnum.Utility:
                _stateContext.SetUtility((UtilityData)skill);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    
    
    

    private void SetIdleState()
    {
        _stateContext.SetList(null);
        _stateContext.SetVector(default);
        _stateContext.SetUnit(null);
        StateEnd?.Invoke(StateEnum.Idle);
    }
}
