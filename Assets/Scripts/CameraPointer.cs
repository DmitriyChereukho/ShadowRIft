using System;
using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed = 0.5f;
    [SerializeField] private float _smoothTime = 0.1f;
    private Vector3 _velocity = Vector3.zero;

    private Transform _targetToFollow;
    private Transform _transformCashed;

    private void Awake()
    {
        _transformCashed = transform;
    }

    private void Start()
    {
        PlacementSystem.Instance.OnPlacing += (_, towerTransform) => _targetToFollow = towerTransform;
        PlacementSystem.Instance.OnPlaced += (_, _) => _targetToFollow = null;
    }

    private void Update()
    {
        if (_targetToFollow is null)
        {
            HandleCameraMovementWhenItIsNotLockedToTarget();
        }
        else
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = _targetToFollow.position;
        _transformCashed.position = Vector3.SmoothDamp(
            _transformCashed.position, 
            targetPosition, 
            ref _velocity, 
            _smoothTime);
    }

    private void HandleCameraMovementWhenItIsNotLockedToTarget()
    {
        var cameraMovementDelta2D = InputManager.Instance.GetCameraMovementDeltaNormalized();
        var moveDir = _transformCashed.forward * cameraMovementDelta2D.y +
                      _transformCashed.right * cameraMovementDelta2D.x;
        _transformCashed.position -= _cameraSpeed * Time.deltaTime * moveDir;
    }
}