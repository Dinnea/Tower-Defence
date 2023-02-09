using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object placed on the StructureController grid. Contains structures (or an empty structure slot), can be marked as a buildzone (default is non buildzone).
/// </summary>
public class Tile
{
    private GridXZ<Tile> _grid;
    private int _x, _z;
    private BuildingTypeSO _templateObjectOnTile = null;
    private Transform _objectOnTile = null;
    public bool isBuildZone = false;

    public Tile(GridXZ<Tile> grid, int x, int z)
    {
        _grid = grid;
        _x = x;
        _z = z;
    }

    private void setObjectTemplate(BuildingTypeSO template)
    {
        _templateObjectOnTile = template;
    }
    public void SetObjectOnTile(Transform objectPlaced, BuildingTypeSO template)
    {
        SetObjectOnTile(objectPlaced);
        setObjectTemplate(template);
    }
    public void SetObjectOnTile(Transform objectPlaced)
    {
        if (isBuildZone){
            _objectOnTile = objectPlaced;
            _grid.TriggerGridObjectChanged(_x, _z);
        }        
    }

    public void ClearObjectOnTile()
    {
        this._objectOnTile = null;
    }

    public override string ToString()
    {
        return _x + ", " + _z + "\n" + _objectOnTile.name;

    }

    public Transform GetObjectOnTile()
    {
        return _objectOnTile;
    }

    public float GetObjectCost()
    {
        return _templateObjectOnTile.cost;
    }

    public bool IsCellFree()
    {
        return _objectOnTile == null;
    }
}
