using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;
using UnityEngine.Events;


public class BuildData
{
    public float cost;
    public BuildData(float pCost)
    {
        cost = pCost;
    }
}

public class SaleData
{
    public float income;
    public SaleData(float pIncome)
    {
        income = pIncome;
    }
}

public class StructureChanged
{
    public BuildingTypeSO test;
    public GameObject structureModel;

    public StructureChanged(BuildingTypeSO structure)
    {
        test = structure;
    }
}

/// <summary>
/// Grid template implementation. Can build towers and other structures. Keeps track of how many objects have been built.
/// </summary>
public class GridManager : MonoBehaviour
{
    
    [SerializeField] private List<BuildingTypeSO> _structureChoices;
    [SerializeField] private LayerMask _layer;
    
    //Those variables shouldn't be changed during runtime.
    [Space]
    [SerializeField] private int _columns = 20;
    [SerializeField] private int _rows = 15;
    [SerializeField] private int _cellSize = 5;
    [SerializeField] private Vector3 origin = Vector3.zero;
    [Space]
    [SerializeField] private bool sell = false;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float valueOnSale = 0.75f;

    private BuildingTypeSO _objectToBuild;
   // private MoneyManager _playerController;
    private int _objectsBuilt = 0;
    public GridXZ<Tile> grid;

    public System.Action<BuildData> onBuild;
    public System.Action<SaleData> onSale;
    public System.Action<StructureChanged> onStructureChanged;

    public List<BuildingTypeSO> GetBuildingTypes()
    {
        return _structureChoices;
    }

    public BuildingTypeSO GetBuildingType(int i)
    {
        return _structureChoices[i];
    }

    private void Awake()
    {
        //_playerController = GetComponent<MoneyManager>();
        grid = new GridXZ<Tile>(_columns, _rows, _cellSize, origin, (GridXZ<Tile> g, int x, int z) => new Tile(g, x, z));
        SetStructure(0);

    }
    /// <summary>
    /// Chooses a structure to build from the list. Defaults to [0] = empty structure that cannot be placed.
    /// </summary>
    /// <param name="number"></param>
    public void SetStructure(int number)
    {
        if (number >= 0 && number <= _structureChoices.Count)
        {
            sell = false;
            _objectToBuild = _structureChoices[number];
        }
        else
        {
            _objectToBuild = _structureChoices[0];
        }
        onStructureChanged?.Invoke(new StructureChanged(_objectToBuild));
    }

    public void EnableSell()
    {
        sell = true;
    }

    /// <summary>
    /// BuildOnTile that does not trigger the OnBuildingBuilt event. Can build objects larger than one tile.
    /// Should only be used at level setup. X, Z are the gridCoordinates.
    /// </summary>
    /// <param name="gridPositionList"></param>
    /// <param name="gridX"></param>
    /// <param name="gridZ"></param>
    /// <param name="objectToBuild"></param>
    public void BuildOnTileSetup(List<Vector2Int> gridPositionList, int gridX, int gridZ, BuildingTypeSO objectToBuild)
    {
        Transform built = Instantiate(objectToBuild.prefab.transform, grid.GetCellPositionInWorld(gridX, gridZ), Quaternion.identity);
        //Instantiate(objectToBuild.visual, grid.GetCellPositionInWorld(gridX, gridZ), Quaternion.identity, built);

        foreach (Vector2Int gridPosition in gridPositionList)
        {
            grid.GetGridObject(gridPosition.x, gridPosition.y).SetObjectOnTile(built);
        }
        //OnBuildingBuilt?.Invoke(this, new OnBuildingBuiltEventArgs { cost = objectToBuild.cost}); 
        //_objectsBuilt++;
    }

    /// <summary>
    /// Build on tile, only if the tile is buildable. Can build objects larger than one tile.
    /// </summary>
    /// <param name="location"></param>
    /// <param name="objectToBuild"></param>

    public void BuildOnTile(Vector3 location)
    {
        Vector2Int gridCoords = grid.GetCellOnWorldPosition(location);

        BuildOnTile(gridCoords);
    }

    public void BuildOnTile(Vector2Int gridCoords)
    {       
        List<Vector2Int> gridPositionList = _objectToBuild.GetGridPositionList(new Vector2Int(gridCoords.x, gridCoords.y)); //list of tiles taken up by object
        Transform built = Instantiate(_objectToBuild.prefab.transform, grid.GetCellPositionInWorld(gridCoords.x, gridCoords.y), Quaternion.identity);
        //Instantiate(_objectToBuild.visual, grid.GetCellPositionInWorld(gridCoords.x, gridCoords.y), Quaternion.identity, built);

        foreach (Vector2Int gridPosition in gridPositionList)
        {
            grid.GetGridObject(gridPosition.x, gridPosition.y).SetObjectOnTile(built, _objectToBuild);
        }
        onBuild?.Invoke(new BuildData(_objectToBuild.cost));
        SetStructure(-1);
         _objectsBuilt++;
       // _objectToBuild = null;
    }

    public void SellFromTile(Vector2Int gridCoords)
    {
        if (!outOfBounds(gridCoords))
        {
            Tile target = grid.GetGridObject(gridCoords.x, gridCoords.y);
            if (!target.IsCellFree())
            {
                GameObject toSell = target.GetObjectOnTile().gameObject;
                float income = target.GetObjectCost();
                income *= valueOnSale;
                onSale?.Invoke(new SaleData(income));
                Destroy(toSell);
            }
        }       
    }

    private bool outOfBounds(Vector2Int gridCoords)
    {
        if (gridCoords.x == -1 || gridCoords.y == -1) return true;
        else return false;
    }

    /// <summary>
    /// Checks if its posssible to build at the location(s) selected.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public bool CanBuild(Vector3 location)
    {
        if (location.x != 0 && location.y != 0 && location.z != 0)
        {            
            Vector2Int gridCoords = grid.GetCellOnWorldPosition(location);
            
            return CanBuild(gridCoords);
        }
        return false;
    }

    public bool CanBuild(Vector2Int gridCoords) 
    {
       if(outOfBounds(gridCoords)) return false;
        else
        {
            List<Vector2Int> gridPositionList = _objectToBuild.GetGridPositionList(new Vector2Int(gridCoords.x, gridCoords.y)); //list of tiles taken up by object
            bool canBuild = true; //default is true
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                Tile gridObject = grid.GetGridObject(gridPosition.x, gridPosition.y);
                if (!gridObject.IsCellFree() || !gridObject.isBuildZone)
                {
                    canBuild = false;
                    break; //stop checking after one spot isn't buildable
                }
            }
            return canBuild;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))// && _objectsBuilt < 5)
        {
            Vector3 location = Utilities.GetMousePositionWorld(Camera.main, _layer);
            Vector2Int gridCoords = grid.GetCellOnWorldPosition(location);
            if (sell) SellFromTile(gridCoords);
            else if (CanBuild(gridCoords) && _objectToBuild != _structureChoices[0]) BuildOnTile(gridCoords);
        }
    }
}
