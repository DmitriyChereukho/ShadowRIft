using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem Instance { get; private set; }

    public event EventHandler<Transform> OnPlacing;
    public event EventHandler OnPlaced;

    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _cellIndicator;
    [SerializeField] private float _hoveringOffset = 0.5f;
    [SerializeField] private BuildingMenuUI _buildingMenuUI;

    private GameObject _selectedTower;
    private Dictionary<Vector3Int, Building> _buildingsCoords;
    private bool _isPlacing;
    private MeshRenderer _meshInd;

    private void Awake()
    {
        Instance = this;
        _buildingsCoords = new Dictionary<Vector3Int, Building>();
    }

    private void Start()
    {
        _meshInd = _cellIndicator.GetComponentInChildren<MeshRenderer>();
        _buildingMenuUI.OnBuildingSelected += OnBuildingMenuUIOnOnBuildingSelected;
    }

    private void OnDestroy()
    {
        _buildingMenuUI.OnBuildingSelected -= OnBuildingMenuUIOnOnBuildingSelected;
    }

    private void OnBuildingMenuUIOnOnBuildingSelected(object _, DragAndDrop dragAndDrop)
    {
        var selectedTowerSo = dragAndDrop.TowerSo;
        _selectedTower = Instantiate(selectedTowerSo.Prefab);
        OnPlacing?.Invoke(this, _selectedTower.transform);
        dragAndDrop.OnStartDrag -= OnStartDrag;
        dragAndDrop.OnStartDrag += OnStartDrag;
        
        dragAndDrop.OnDragging -= OnDragging;
        dragAndDrop.OnDragging += OnDragging;
        
        dragAndDrop.OnDrop -= OnDrop;
        dragAndDrop.OnDrop += OnDrop;
    }

    private void OnStartDrag(object o, EventArgs eventArgs)
    {
        _isPlacing = true;
        _meshInd.material.color = Color.green;
    }

    private void OnDrop(object sender, EventArgs eventArgs)
    {
        var buildingDragAndDrop = (DragAndDrop) sender;
        _selectedTower.TryGetComponent<Building>(out var tower);
        PlaceBuilding(_grid.WorldToCell(buildingDragAndDrop.LastWorldPosition), tower);
    }

    private void Update()
    {
        if (_isPlacing)
            return;

        var mousePos = InputManager.Instance.GetSelectedMapPosition();
        var cellPos = CalculateWorldPositionOfTheCell(mousePos);
        _cellIndicator.transform.position = cellPos;
    }

    private Vector3 CalculateWorldPositionOfTheCell(Vector3 worldPosition)
    {
        var gridPos = _grid.WorldToCell(worldPosition);
        return _grid.CellToWorld(gridPos);
    }

    private void OnDragging(object sender, Vector3 position)
    {
        var cellPos = CalculateWorldPositionOfTheCell(position);
        _selectedTower.transform.position = cellPos + Vector3.up * _hoveringOffset;
        _cellIndicator.transform.position = cellPos;
    }

    private void PlaceBuilding(Vector3Int cellPos, Building tower)
    {
        _isPlacing = false;
        _meshInd.material.color = Color.white;
        OnPlaced?.Invoke(this, EventArgs.Empty);
        if (_buildingsCoords.ContainsKey(cellPos))
        {
            Destroy(_selectedTower);
            return;
        }

        tower.transform.position = cellPos;
        _buildingsCoords[cellPos] = tower;
    }
}