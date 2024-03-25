using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuUI : MonoBehaviour
{
    public event EventHandler<DragAndDrop> OnBuildingSelected;
    
    [SerializeField] private BuildingMenu _buildingMenu;
    [SerializeField] private Transform _buildingTemplate;
    [SerializeField] private Transform _container;

    private void Awake()
    {
        _buildingTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        Hide();
        MainUI.Instance.OnBuildingMenuOpened += () =>
        {
            Show();
            UpdateVisual();
        };

        _buildingMenu.OnBuildingAdded += (_, _) => UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in _container)
        {
            if (child == _buildingTemplate)
                continue;
            var dragAndDrop = child.GetComponent<DragAndDrop>();
            dragAndDrop.OnStartDrag -= OnDragAndDropOnStartDrag;
            Destroy(child.gameObject);
        }

        foreach (var towerSo in _buildingMenu)
        {
            var building = Instantiate(_buildingTemplate, _container);
            building.gameObject.SetActive(true);
            building.TryGetComponent<SingleBuildingUI>(out var buildingUI);
            building.TryGetComponent<DragAndDrop>(out var dragAndDrop);
            buildingUI.SetTowerSO(towerSo);
            dragAndDrop.TowerSo = towerSo;

            dragAndDrop.OnStartDrag += OnDragAndDropOnStartDrag;
        }
    }
    
    private void OnDragAndDropOnStartDrag(object sender, EventArgs eventArgs)
    {
        var dragAndDrop = (DragAndDrop) sender;
        OnBuildingSelected?.Invoke(this, dragAndDrop);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}