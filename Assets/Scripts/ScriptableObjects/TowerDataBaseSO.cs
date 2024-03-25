using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerDataBaseSO : ScriptableObject
{
    [field: SerializeField] 
    public List<TowerSO> Towers { get; private set; }
}