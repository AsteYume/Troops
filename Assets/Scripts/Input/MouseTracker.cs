using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    
    
    
    private void PlayerCursor()
            {
    
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                if (plane.Raycast(ray, out float rayLength))
                {
                    Vector3 targetPosition = ray.GetPoint(rayLength);
                }
            }
}
