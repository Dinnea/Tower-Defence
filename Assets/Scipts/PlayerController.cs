using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] float _money = 1000;
    StructureController _gridController;

    private void Awake()
    {
        _gridController = GetComponent<StructureController>();
    }

    private void Start()
    {
        _gridController.OnBuildingBuilt += BuyTower;
    }

    public void BuyTower(object sender, StructureController.OnBuildingBuiltEventArgs args)
    {
        Debug.Log("Money was taken: " + args.cost);
        _money -= args.cost;
        Debug.Log("Balance: " + _money);
    }

    public bool CanAffordBuild(float cost)
    {
        if (_money - cost >= 0) return true;
        return false;
    }
}
