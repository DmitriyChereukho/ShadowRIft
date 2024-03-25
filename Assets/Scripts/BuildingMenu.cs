using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour, IEnumerable<TowerSO>
{
    public event EventHandler<TowerSO> OnBuildingAdded;

    [SerializeField] private TowerDataBaseSO _towerDataBaseSo;
    [SerializeField] private int _maxBuildingsCount = 10;
    
    // Debug only
    private bool _allTowersAvailable = true;
    
    private List<TowerSO> _towerSos;

    private void Awake()
    {
        _towerSos = new List<TowerSO>();
        if (_allTowersAvailable)
        {
            foreach (var towerSo in _towerDataBaseSo.Towers)
            {
                AddBuilding(towerSo);
            }
        }
    }

    private void Start()
    {
        
    }

    public bool AddBuilding(TowerSO towerSo)
    {
        if (_maxBuildingsCount == _towerSos.Count || _towerSos.Contains(towerSo))
            return false;
        
        _towerSos.Add(towerSo);
        OnBuildingAdded?.Invoke(this, towerSo);
        return true;
    }

    public TowerSO this[int index]
    {
        get => _towerSos[index];
        set => _towerSos[index] = value;
    }

    public IEnumerator<TowerSO> GetEnumerator() 
        => _towerSos.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}