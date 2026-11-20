using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private StateContext _context;
    private UnitMover _mover;
    private RangeViewer _rangeViewer;
    
    private bool _isMoving;
    private float _speed = 2f;
    private Vector3 _nextPoint;
    private Vector2Int _goal;
    
    // ============================================================

    private bool HasReached => _context.SelectUnit.Object.transform.position == _nextPoint;
    
    // ============================================================
    
    public override event Action<StateEnum> StateEnd;
    
    // ============================================================

    public MoveState(UnitMover mover, RangeViewer rangeViewer, StateContext context)
    {
        _context = context;
        _mover = mover;
        _rangeViewer = rangeViewer;
        mover.SetMoveState(this);
    }
    
    // ============================================================
    
    public override void OnEnter()
    {
        SetData();
    }

    public override void OnExit()
    {
    }
    
    // ============================================================
    
    public void Moving()
    {
        if (!_isMoving) return;
        if (HasReached && _context.TilesStack.Count == 0)
        {
            MoveDone();
            return;
        }
        if (HasReached) SetNextPoint();
        MoveToPoint();
    }
    
    // ============================================================

    private void SetData()
    {
        _isMoving = true;
        _rangeViewer.IsReachableTile(_context.SelectVector.x, _context.SelectVector.y,out Node node);
        _context.SetStack(LocateAlgorithm.ReadNodePath(node));
        _goal = _context.SelectVector;
        StageData.Instance.MoveUnit(_context.SelectUnit, _goal);
        SetNextPoint();
    }
    
    private void SetNextPoint()
    {
        Vector2Int next = _context.TilesStack.Pop();
        _nextPoint = new Vector3(next.x, _context.SelectUnit.Object.transform.position.y, next.y);
    }

    private void MoveDone()
    {
        _rangeViewer.ClearViewerObject();
        _context.ClearRange();
        _context.SelectUnit.MoveDone(_goal);
        _isMoving = false; 
        if (_context.SelectUnit.CanAction) NextIsSkill();
        else NextIsIdle();
    }

    private void NextIsIdle()
    {
        _context.SetUnit(null);
        StateEnd?.Invoke(StateEnum.Idle);
    }

    private void NextIsSkill()
    {
        StateEnd?.Invoke(StateEnum.SelectSkill);
    }
    
    private void MoveToPoint()
    {
        Transform trans = _context.SelectUnit.Object.transform;
        trans.position = Vector3.MoveTowards(trans.position, _nextPoint
            , _speed * Time.deltaTime);
    }
}
