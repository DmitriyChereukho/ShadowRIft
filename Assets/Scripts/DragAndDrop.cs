using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 LastWorldPosition { get; private set; }
    public event EventHandler OnStartDrag;
    public event EventHandler<Vector3> OnDragging;
    public event EventHandler OnDrop;

    public TowerSO TowerSo { get; set; }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnStartDrag?.Invoke(this, EventArgs.Empty);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        CalculateDragWorldPos(eventData.position);
        OnDragging?.Invoke(this, LastWorldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnDrop?.Invoke(this, EventArgs.Empty);
    }

    private void CalculateDragWorldPos(Vector2 screenPos)
    {
        var posVec3 = new Vector3(screenPos.x, 0, screenPos.y);

        var camera = Camera.main;
        posVec3.z = camera.nearClipPlane;
        if (Physics.Raycast(camera.ScreenPointToRay(screenPos), out var hit, 100, InputManager.Instance.RayCastLayerMask))
        {
            LastWorldPosition = hit.point;
        }
    }
}