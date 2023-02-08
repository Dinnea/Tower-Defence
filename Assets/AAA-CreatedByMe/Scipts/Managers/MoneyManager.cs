using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAfford
{
    public int id;
    public bool value;
    public CanAfford(int pId, bool pValue)
    {
        id = pId;
        value = pValue;
    }
}
public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float _money = 1000;
    private GridManager _gridManager;
    public System.Action<CanAfford> canAfford;

    private void OnEnable()
    {
        _gridManager.onBuildAttempt += BuyTower;
    }

    private void OnDisable()
    {
        _gridManager.onBuildAttempt -= BuyTower;
    }

    private void Awake()
    {
        _gridManager = GetComponent<GridManager>();
    }

    public void BuyTower(BuildData buildData)
    {
        Debug.Log("Money was taken: " + buildData.cost);
        _money -= buildData.cost;
        Debug.Log("Balance: " + _money);
        //if money is too low, disable building options
        List<BuildingTypeSO> buildings = _gridManager.GetBuildingTypes();
        for (int i = 0; i< buildings.Count; i++)
        {   
            canAfford?.Invoke(new CanAfford(i, canAffordBuild(buildings[i].cost))); 
        }
    }

    private bool canAffordBuild(float cost)
    {
        if (_money - cost >= 0) return true;
        return false;
    }
}
