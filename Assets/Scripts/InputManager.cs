using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    [SerializeField] private Camera _camera;
    [field: SerializeField] 
    public LayerMask RayCastLayerMask { get; private set; }
    private Vector3 _lastPosition;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        
    }
    
    public Vector2 GetCameraMovementDeltaNormalized() 
        => _playerInputActions.Player.MoveCamera.ReadValue<Vector2>().normalized;

    public Vector3 GetSelectedMapPosition()
    {
        var touchPos = _playerInputActions.Player.Point.ReadValue<Vector2>();
        if (Physics.Raycast(_camera.ScreenPointToRay(touchPos), out var hit, 100, RayCastLayerMask))
        {
            _lastPosition = hit.point;
        }
        return _lastPosition;
    }
}