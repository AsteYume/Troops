using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private GameObject _tileSelecter;
    
    [SerializeField] private InputActionReference _mouseMove;
    [SerializeField] private InputActionReference _mouseClick;

    private UnitMover _unitMover;
    private RangeViewer _rangeViewer;
    private StateManager _stateManager;
    private Camera _camera;
    private Tile _currentTile;
    private Vector2 _cursorVector;
    private float _rayLength = 50f;
    private int _tileLayer = 1 << 15;
    private Unit _currentUnit;
    private Vector2Int _targetVector;
    
    private MapDataProvider MapData => _stateManager.MapData;
    private StateEnum CurrentState => _stateManager.CurrentState;
    private bool IsTracking => CurrentState is not (StateEnum.None or StateEnum.MoveResult or StateEnum.SkillResult);
    private bool _isTileExist =>  _currentTile != null;
    private bool _isUnitExist => _currentUnit != null;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        ReadMouseMove();
    }

    private void OnEnable()
    {
        _mouseMove.action.Enable();
        _mouseClick.action.Enable();
        _mouseClick.action.performed += OnClick;
    }

    private void OnDisable()
    {
        _mouseMove.action.Disable();
        _mouseClick.action.Disable();
        _mouseClick.action.performed -= OnClick;
    }


    private void Init()
    {
        _unitMover = GetComponent<UnitMover>();
        _camera = Camera.main;
        _stateManager = GetComponent<StateManager>();
        _rangeViewer = GetComponent<RangeViewer>();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log(CurrentState);
        switch (CurrentState)
        {
            case StateEnum.Idle:
                OnIdle();
                break;
            case StateEnum.Select:
                OnSelect();
                break;
            default:
                break;
        }
        Debug.Log(_currentUnit);
    }

    public void OnMoveResult()
    {
        StageData.MoveUnit(_currentUnit, _targetVector);
        _currentUnit.MoveDone(_targetVector);
        _rangeViewer.ClearData();
        _currentUnit = null;
        _stateManager.SetState(StateEnum.Idle);
    }

    private void OnSelect()
    {
        Vector2Int unitVec = new Vector2Int(_currentUnit.X, _currentUnit.Z);
        if (!CursorSelect(out Vector2Int vec))
        {
            _stateManager.SetState(StateEnum.Idle);
            _rangeViewer.ClearData();
            return;
        }
        if (vec == unitVec) return;
        if (StageData.UnitsPosition.ContainsKey((vec.x,vec.y))) TrySelectUnit();
        else if(_rangeViewer.IsReachableTile(vec.x, vec.y,out Node node))
        {
            _targetVector = vec;
            Stack<Vector2Int> stack = LocateAlgorithm.ReadNodePath(node);
            _unitMover.SetData(_currentUnit, stack);
        }
        else
        {
            _stateManager.SetState(StateEnum.Idle);
            _rangeViewer.ClearData();
        }
    }

    private void OnIdle()
    {
        TrySelectUnit();
    }

    private void TrySelectUnit()
    {
        _rangeViewer.ClearData();
        if (!CursorSelect(out Vector2Int vec)) return;
        _currentUnit = _rangeViewer.TryGetUnit(vec, out var unit) ? unit : null;
        if (!_isUnitExist) return;
        switch (unit.Team)
        {
            case UnitTeam.Blue:
                if (!unit.CanAction) return;
                _stateManager.SetState(StateEnum.Select);
                _rangeViewer.CalculateMovement(vec, unit.MoveRange);
                break;
            case UnitTeam.Red:
                _rangeViewer.BfsView(vec, unit.Sight, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private bool CursorSelect(out Vector2Int vec)
    {
        vec = new Vector2Int();
        if (!IsTracking) return false;
        _cursorVector = _mouseMove.action.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(_cursorVector);
        if (!Physics.Raycast(ray, out RaycastHit hit, _rayLength, _tileLayer)) return false;
        Debug.DrawRay(ray.origin, ray.direction * _rayLength, Color.red);
        vec.x = Mathf.RoundToInt(hit.collider.gameObject.transform.position.x);
        vec.y = Mathf.RoundToInt(hit.collider.gameObject.transform.position.z);
        GetTile(vec.x,vec.y);
        return true;
    }

    private void ReadMouseMove()
    {
        Tracking();
        _tileSelecter.SetActive(IsTracking && _isTileExist);
    }

    private void Tracking()
    {
        if (!IsTracking) return;
        if (!CursorSelect(out Vector2Int vec)) return;
        _tileSelecter.transform.position = new Vector3(vec.x, 0, vec.y);
    }

    private void GetTile(int x, int z)
    {
        if (MapData.TryGetTile(x, z, out Tile tile))
        {
            _currentTile = tile;
            _tileSelecter.transform.position = new Vector3(x, 0, z);
        }
        else
        {
            _currentTile = null;
        }
    }
}
