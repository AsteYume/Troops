using System;
using UnityEngine;

public class TrackerMover : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    [SerializeField] GameObject _tracker;
    
    private bool _isNeedTracking;
    private Camera _camera;
    
    // ============================================================

    private void Awake() => Init();

    private void Update() => MoveTracker();

    private void OnEnable() => StateManager.Instance.OnStateStart += OnStateChange;
    
    private void OnDisable() => StateManager.Instance.OnStateStart -= OnStateChange;

    // ============================================================
    
    private void Init()
    {
        _camera = Camera.main;
    }

    private void MoveTracker()
    {
        if (!_tracker.activeSelf) return;
        if (!InputReader.TryGetVector(_camera, _inputReader.MouseVector2(), out Vector2Int vec)) return;
        _tracker.gameObject.transform.position = new Vector3(vec.x, 0, vec.y);

    }

    private void OnStateChange(StateEnum state)
    {
        _isNeedTracking = state == StateEnum.None;
        if (_isNeedTracking != _tracker.activeSelf) 
            _tracker.gameObject.SetActive(_isNeedTracking);
    }
    
    
}
