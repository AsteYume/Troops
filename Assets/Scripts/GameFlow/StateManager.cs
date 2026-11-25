using System;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private RangeViewer _rangeViewer;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private SkillSelectUI _skillSelectUI;
    [SerializeField] private Button _endButton;
    
    private static StateManager _instance;
    private State _currentState;
    private StateContext _context;
    private PlayerPhase _playerPhase;
    
    private IdleState _idleState;
    private SelectMoveState _selectMoveState;
    private MoveState _moveState;
    private EnemySightState _enemySightState;
    private SelectSkillState _selectSkillState;
    private SelectRangeState _selectRangeState;
    private SkillState _skillState;
    
    // ============================================================
    
    public State CurrentState => _currentState;
    public MapDataProvider MapData => MapManager.Instance.MapDataProvider;
    public event Action<StateEnum> OnStateStart;
    
    // ============================================================
    
    public static StateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<StateManager>();
            }
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
        DontDestroyOnLoad(this);
    }
    
    // ============================================================

    private void Awake()
    {
        SetSingleTon();
        Init();
    }

    private void OnDestroy()
    {
        OnStateStart = null;
    }


    // ============================================================
    
    public void SetPhaseClass(PlayerPhase playerPhase)
    {
        _playerPhase = playerPhase;
        _playerPhase.PhaseStart += OnPhaseStart;
    }
    
    public void PhaseEnd()
    {
        EndState();
        _rangeViewer.ClearViewerObject();
        _context.ClearAll();
        _playerPhase.PlayerPhaseEnd();
    }
    
    // ============================================================

    private void SetEndButton(bool value)
    {
        _endButton.gameObject.SetActive(value);
    }

    private void OnPhaseStart()
    {
        StageData.Instance.NextTurn();
        EnterState(StateEnum.Idle);
    }

    private void OnStageEnd()
    {
        _playerPhase.PhaseStart -= OnPhaseStart;
        _playerPhase = null;
    }
    
    private void ChangeState(StateEnum newState)
    {
        SetEndButton(false);
        EndState();
        EnterState(newState);
    }

    private void EndState()
    {
        _currentState.StateEnd -= ChangeState;
        _currentState.OnExit();
    }

    private void EnterState(StateEnum newStateEnum)
    { 
        SetEndButton(newStateEnum == StateEnum.Idle);
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
            case StateEnum.SelectMove:
                return _selectMoveState;
            case StateEnum.EnemySight:
                return _enemySightState;
            case StateEnum.SelectSkill:
                return _selectSkillState;
            case StateEnum.SelectRange:
                return _selectRangeState;
            case StateEnum.Skill:
                return _skillState;
            default:
                Debug.Log("StateManager : 예외 경로 실행");
                return _idleState;
        }
    }

    private void Init()
    {
        _context = new StateContext();
        _idleState = new IdleState(_inputReader, _context);
        _enemySightState = new EnemySightState(_rangeViewer, _inputReader, _context);
        _selectMoveState = new SelectMoveState(_inputReader, _rangeViewer, _context);
        _moveState = new MoveState(_mover, _rangeViewer, _context);
        _selectSkillState = new SelectSkillState(_inputReader, _context, _skillSelectUI, _rangeViewer);
        _selectRangeState = new SelectRangeState(_inputReader, _rangeViewer, _context);
        _skillState = new SkillState(_context, _rangeViewer);
        _rangeViewer.SetStateContext(_context);
    }
}
