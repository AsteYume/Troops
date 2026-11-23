using System;
using UnityEngine;

public class SkillState : State
{
    StateContext _context;
    RangeViewer _viewer;

    public SkillState(StateContext context, RangeViewer viewer)
    {
        _context = context;
        _viewer = viewer;
    }
    
    public override event Action<StateEnum> StateEnd;
    public override void OnEnter()
    {
        _viewer.ClearViewerObject();
        _context.SelectUnit.SkillUsed(_context.SelectSkill);
        SkillCalcurator.CalSkillResult(_context.SelectSkill, _context.SelectVector, _context.SelectUnit);
        if (_context.SelectUnit.CanAction) NextIsSelect();
        else NextIsIdle();
    }

    private void NextIsSelect()
    {
        StateEnd.Invoke(StateEnum.SelectSkill);
    }

    private void NextIsIdle()
    {
        _context.SetUnit(null);
        _context.ClearRange();
        StateEnd.Invoke(StateEnum.Idle);
    }


    public override void OnExit()
    {
        _context.SkillResult();
    }
}
