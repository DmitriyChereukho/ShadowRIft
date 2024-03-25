using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class TowerSO : ScriptableObject
{
    [field: SerializeField] 
    public string Name { get; private set; }
    [field: SerializeField] 
    public int Id { get; private set; }
    [field: SerializeField] 
    public Sprite Icon { get; private set; }
    [field: SerializeField] 
    public SizeInCells SizeInCells { get; private set; }
    [field: SerializeField] 
    public GameObject Prefab { get; private set; }
    [field: SerializeField] 
    public int Damage { get; private set; }
    [field: SerializeField] 
    public int Price { get; private set; }
    [field: SerializeField] 
    public int AttackSpeed { get; private set; }
    [field: SerializeField] 
    public int LifeTimeInSec { get; private set; }
}

[Serializable]
public struct SizeInCells
{
    public int Height { get; private set; }
    public int Width { get; private set; }
}