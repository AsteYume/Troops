using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private StateManager _stateManager;

    private MouseTracker _tracker;
    private Stack<Vector2Int> _stack;
    private Unit _currentUnit ;
    private Vector3 _goal;
    private bool _signal;
    private float _speed = 2f;
    
    private bool HasData => _currentUnit != null;
    private bool IsMoving => _stack.Count > 0 || !HasReached;
    private bool HasReached => _currentUnit.Object.transform.position == _goal;

    private void Awake()
    {
        _tracker = GetComponent<MouseTracker>();
        _stateManager = GetComponent<StateManager>();
    }
    
    private void Update()
    {
        MoveUnit();
    }

    public void SetData(Unit unit, Stack<Vector2Int> stack)
    {
        _stateManager.SetState(StateEnum.Move);
        _stack = stack;
        _currentUnit = unit;
        _goal = unit.Object.transform.position;
        _signal = true;
    }

    private void MoveUnit()
    {
        if (HasData && !IsMoving) MoveDone();
        if (!_signal) return;
        if (HasReached) SetNextPoint();
        MoveToPoint();
    }

    private void MoveDone()
    {
        _stateManager.SetState(StateEnum.MoveResult);
        _signal = false;
        _currentUnit = null;
        _tracker.OnMoveResult();
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
}
