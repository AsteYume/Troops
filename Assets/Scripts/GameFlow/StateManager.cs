using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    private static StateManager _instance;
    private State _currentState;
    private StateContext _context;
    private IdleState _idleState;
    private MoveState _moveState;
    
    // ============================================================
    
    public State CurrentState => _currentState;
    public MapDataProvider MapData => MapManager.Instance.MapDataProvider;
    public event Action<StateEnum> OnStateStart;
    
    // ============================================================
    
    public static StateManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindFirstObjectByType<StateManager>();
            if (_instance != null) return _instance;
            GameObject go = new GameObject();
            go.AddComponent<StateManager>();
            return _instance;
        }
    }
    
    private void SetSingleTon()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    
    // ============================================================

    private void Awake()
    {
        SetSingleTon();
        Init();
    }
    
    // ============================================================
    
    private void ChangeState(StateEnum newState)
    {
        _currentState.StateEnd -= ChangeState;
        _currentState.OnExit();
        EnterState(newState);
    }

    private void EnterState(StateEnum newStateEnum)
    {
        _currentState = GetState(newStateEnum);
        _currentState.StateEnd += ChangeState;
        _currentState.OnEnter();
        OnStateStart?.Invoke(newStateEnum);
    }
    
    private State GetState(StateEnum stateEnum)
    {
        switch (stateEnum)
        {
            case StateEnum.Idle:
                return _idleState;
            case StateEnum.Move:
                return _moveState;
            default:
                return _idleState; // 실행되면 안됨
        }
    }

    private void Init()
    {
        _context = new StateContext();
        _idleState = new IdleState(_inputReader, _context);
        _moveState = new MoveState(this, _context);
        EnterState(StateEnum.Idle);
    }
    
    
    
}
