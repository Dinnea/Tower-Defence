using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyData
{
    public int id;
    public bool value;
    public float money;
    public MoneyData(int pId, bool pValue, float pMoney)
    {
        id = pId;
        value = pValue;
        money = pMoney;
    }
}
public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float _money = 1000;
    private GridManager _gridManager;
    private List<BuildingTypeSO> _buildings;
    public System.Action<MoneyData> onMoneyChanged;

    private void OnEnable()
    {
        _gridManager.onBuild += BuyTower;
    }

    private void OnDisable()
    {
        _gridManager.onBuild -= BuyTower;
    }

    private void Awake()
    {
        _gridManager = GetComponent<GridManager>();
        _buildings = _gridManager.GetBuildingTypes();
        checkIfCanAffordBuildings();
    }

    public void BuyTower(BuildData buildData)
    {
        _money -= buildData.cost;
        //if money is too low, disable building options
        checkIfCanAffordBuildings();
    }
    private void checkIfCanAffordBuildings()
    {
        for (int i = 0; i < _buildings.Count; i++)
        {
            onMoneyChanged?.Invoke(new MoneyData(i, canAffordBuild(_buildings[i].cost), _money));
        }
    }
    private bool canAffordBuild(float cost)
    {
        if (_money - cost >= 0) return true;
        return false;
    }
}
