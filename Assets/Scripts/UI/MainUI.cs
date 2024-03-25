using System;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField] private Button _buildingMenuButton;
    public event Action OnBuildingMenuOpened;

    public static MainUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _buildingMenuButton.onClick.AddListener(() =>
        {
            OnBuildingMenuOpened?.Invoke();
            _buildingMenuButton.gameObject.SetActive(false);
        });
    }
}