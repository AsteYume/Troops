using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputActionReference _mouseMove;
    [SerializeField] private InputActionReference _confirm;
    [SerializeField] private InputActionReference _cancel;
    [SerializeField] private InputActionReference _option;
    
    public event Action Confirm;
    public event Action Cancel;
    public event Action Option;
    
    private const float RayLength = 50f;
    private const int TileLayer = 1 << 15;
    private Camera _cam;
    
    // ============================================================
    
    private void Awake() => Init();
    
    private void OnEnable()
    {
        _mouseMove.action.Enable();
        
        _confirm.action.Enable();
        _cancel.action.Enable();
        _option.action.Enable();

        _confirm.action.performed += OnConfirm;
        _cancel.action.canceled += OnCancel;
        _option.action.performed += OnOption;
    }

    private void OnDisable()
    {
        _mouseMove.action.Disable();
        
        _confirm.action.Disable();
        _cancel.action.Disable();
        _option.action.Disable();
        
        _confirm.action.performed -= OnConfirm;
        _cancel.action.performed -= OnCancel;
        _option.action.performed -= OnOption;
    }
    
    // ============================================================

    private void Init()
    {
        _cam = Camera.main;
    }
    
    // ============================================================

    private void OnConfirm(InputAction.CallbackContext context)
    {
        Debug.Log("Confirm");
        Confirm?.Invoke();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel");
        Cancel?.Invoke();
    }
    
    private void OnOption(InputAction.CallbackContext context)
    {
        Option?.Invoke();
    }
    
    // ============================================================
    
    public bool TryGetVector(out Vector2Int vec)
    {
        vec = new Vector2Int();
        Ray ray = _cam.ScreenPointToRay(_mouseMove.action.ReadValue<Vector2>());
        if (!Physics.Raycast(ray, out RaycastHit hit, RayLength, TileLayer)) return false;
        Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.red);
        vec.x = Mathf.RoundToInt(hit.collider.gameObject.transform.position.x);
        vec.y = Mathf.RoundToInt(hit.collider.gameObject.transform.position.z);
        return true;
    }
    
}
