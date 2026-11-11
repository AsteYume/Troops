using UnityEngine;
using UnityEngine.InputSystem;

public class InGameCameraMove : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private float cameraSpeed = 5f;

    private float _cameraMin = -2f;
    private float _cameraMax = 14f;
    
    
    private Vector2 _inputMovement;
    private Vector3 _moveDirection;
    
    private void OnEnable()
    {
        if (movement != null)
        {
            movement.action.Enable();
            movement.action.performed += OnMovePress;
            movement.action.canceled += OnMoveRelease;
        }
    }
    
    private void OnDisable()  
    {  
        if (movement != null)
        {
            movement.action.Disable();
            movement.action.performed -= OnMovePress;
            movement.action.canceled -= OnMoveRelease;
        }
    }

    private void Update()
    {
        if (_inputMovement != Vector2.zero)
        {
            transform.Translate(_moveDirection * (Time.deltaTime * cameraSpeed));
            Vector3 nextPosition = transform.position;
            nextPosition.x = Mathf.Clamp(nextPosition.x, _cameraMin, _cameraMax);
            nextPosition.z = Mathf.Clamp(nextPosition.z, _cameraMin, _cameraMax);
            transform.position = nextPosition;
        }
    }

    private void OnMovePress(InputAction.CallbackContext ctx)
    {
        _inputMovement = ctx.ReadValue<Vector2>();
        _moveDirection = new Vector3(_inputMovement.x, 0f, _inputMovement.y);
    }

    private void OnMoveRelease(InputAction.CallbackContext ctx)
    {
        _inputMovement = Vector2.zero;
    }
    
}
