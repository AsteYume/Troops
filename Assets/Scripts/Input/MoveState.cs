using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private StateContext _context;
    
    private Stack<Vector2Int> _stack;
    private Unit _currentUnit ;
    private Vector3 _goal;
    private float _speed = 2f;
    
    private bool HasData => _currentUnit != null;
    private bool IsMoving => _stack.Count > 0 || !HasReached;
    private bool HasReached => _currentUnit.Object.transform.position == _goal;

    public MoveState(StateContext context)
    {
        _context = context;
    }
    
    private void Update()
    {
        MoveUnit();
    }

    private void MoveUnit()
    {
        if (HasData && !IsMoving) MoveDone();
        if (HasReached) SetNextPoint();
        MoveToPoint();
    }

    private void MoveDone()
    {
        _currentUnit = null;
        _tracker.OnMoveResult();
        StageData.Instance.MoveUnit(_currentUnit, _targetVector);
        _currentUnit.MoveDone(_targetVector);
    }

    private void SetNextPoint()
    {
        Vector2Int next = _stack.Pop();
        _goal = new Vector3(next.x, _currentUnit.Object.transform.position.y, next.y);
    }

    private void MoveToPoint()
    {
        Transform trans = _currentUnit.Object.transform;
        trans.position = Vector3.MoveTowards(trans.position, _goal
            , _speed * Time.deltaTime);
    }
    public override void OnEnter()
    {
        // 들어올때 
        _stack = _context.TilesStack;
        _currentUnit = _context.CurrentUnit;
        _goal = _currentUnit.Object.transform.position;
    }

    public override void OnExit()
    {
        
    }
}
