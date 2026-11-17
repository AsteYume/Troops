using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private GameObject _tileSelecter;
    [SerializeField] private InputActionReference _mouseMove;

    private StateManager _stateManager;
    private Camera _camera;
    private Tile _currentTile;
    private Vector2 _cursorVector;
    private float _rayLength = 50f;
    private int _tileLayer = 1 << 15;
    
    private MapDataProvider MapData => _stateManager.MapData;
    private StateEnum CurrentState => _stateManager.CurrentState;
    private bool IsTracking => CurrentState is StateEnum.Select or StateEnum.MoveTarget or StateEnum.SkillTarget;
    private bool _isTileExist =>  _currentTile != null;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        CursorSelect();
        SelectorControl();
    }

    private void OnEnable()
    {
        _mouseMove.action.Enable();
    }

    private void OnDisable()
    {
        _mouseMove.action.Disable();
    }


    private void Init()
    {
        _camera = Camera.main;
        _stateManager = GetComponent<StateManager>();
    }
    
    private void SelectorControl()
    {
        _tileSelecter.SetActive(_isTileExist);
    }
    
    private void CursorSelect()
    {
        if (!IsTracking) return;
        _cursorVector = _mouseMove.action.ReadValue<Vector2>();
        Ray ray = _camera.ScreenPointToRay(_cursorVector);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, _tileLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * _rayLength, Color.red);
            int x = Mathf.RoundToInt(hit.collider.gameObject.transform.position.x);
            int z = Mathf.RoundToInt(hit.collider.gameObject.transform.position.z);
            GetTile(x,z);
        }
    }

    private void GetTile(int x, int z)
    {
        if (MapData.TryGetTile(x, z, out Tile tile))
        {
            _currentTile = tile;
            switch (_stateManager.CurrentState)
            {
                case StateEnum.Select:
                    _tileSelecter.transform.position = new Vector3(x, 0, z);
                    break;
                default:
                    break;
            }
        }
        else
        {
            _currentTile = null;
        }
    }
}
