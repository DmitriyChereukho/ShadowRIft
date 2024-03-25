using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleBuildingUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;

    public void SetTowerSO(TowerSO towerSo)
    {
        _icon.sprite = towerSo.Icon;
        _name.text = towerSo.Name;
    }
}