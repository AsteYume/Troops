using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private MapDataProvider _mapData;
    [SerializeField] private GameObject _tileSelecter;
    [SerializeField] private InputActionReference _mouseMove;
    private Camera _camera;
    
    private Tile _currentTile;
    private Vector2 _cursorVector;
    private float _rayLength = 50f;
    private int _tileLayer = 1 << 15;
    private bool _isMoveMode = false;
    
    private bool _isTileExist =>  _currentTile != null;

    private void Awake()
    {
        _camera = Camera.main;
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
    
    

    private void SelectorControl()
    {
        _tileSelecter.SetActive(_isTileExist);
    }
    
    private void CursorSelect()
    {
        if (!_isMoveMode) return;
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
        if (_mapData.TryGetTile(x, z, out Tile tile))
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
