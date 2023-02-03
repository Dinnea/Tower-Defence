using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object placed on the StructureController grid. Contains structures (or an empty structure slot), can be marked as a buildzone (default is non buildzone).
/// </summary>
public class Cell
{
    private GridXZ<Cell> _grid;
    private int _x, _z;
    private Transform _objectOnTile;
    public bool isBuildZone = false;

    public Cell(GridXZ<Cell> grid, int x, int z)
    {
        _grid = grid;
        _x = x;
        _z = z;
    }

    public void SetObjectOnTile(Transform objectPlaced)
    {
        if (isBuildZone){
            this._objectOnTile = objectPlaced;
            _grid.TriggerGridObjectChanged(_x, _z);
        }        
    }

    public void ClearObjectOnTile()
    {
        this._objectOnTile = null;
    }

    public override string ToString()
    {
        return _x + ", " + _z + "\n" + _objectOnTile;

    }

    public bool isSlotFree()
    {
        return _objectOnTile == null;
    }
}
