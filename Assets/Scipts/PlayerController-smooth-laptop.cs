using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float _money = 1000;
   private StructureController _structureController;

    private void Awake()
    {
        _structureController = GetComponent<StructureController>();
    }

    private void Start()
    {
        _structureController.OnBuildingBuilt += BuyTower;
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
