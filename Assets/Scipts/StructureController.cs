using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;

/// <summary>
/// Grid template implementation. Can build towers and other structures. Keeps track of how many objects have been built.
/// </summary>
public class StructureController : MonoBehaviour
{
    
    [SerializeField] public List<BuildingTypeSO> structureChoices;
    [SerializeField] private LayerMask _layer;
    
    //Those variables shouldn't be changed during runtime.
    [Space]
    [SerializeField] private int _columns = 20;
    [SerializeField] private int _rows = 15;
    [SerializeField] private int _cellSize = 5;
    [SerializeField] private Vector3 origin = Vector3.zero;

    private BuildingTypeSO _objectToBuild;
    private PlayerController _playerController;
    private int _objectsBuilt = 0;
    public GridXZ<Cell> grid;

    public event EventHandler<OnBuildingBuiltEventArgs> OnBuildingBuilt;

    public class OnBuildingBuiltEventArgs : EventArgs
    {
        public int cost;
    }
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        grid = new GridXZ<Cell>(_columns, _rows, _cellSize, origin, (GridXZ<Cell> g, int x, int z) => new Cell(g, x, z));
        _objectToBuild = structureChoices[0];

    }
    /// <summary>
    /// Chooses a structure to build from the list. Default is choice [0]. Structure chosen will remain unchanged if the number is out of scope.
    /// </summary>
    /// <param name="number"></param>
    public void SetStructure(int number)
    {
        if (number >= 0 && number <= structureChoices.Count)
        {
            _objectToBuild = structureChoices[number];
        }
        else Debug.Log("No such building");
    }

    /// <summary>
    /// BuildOnTile, ignoring CanBuild and does not trigger the OnBuildingBuilt event. Can build objects larger than one tile.
    /// Should only be used at level setup. X, Z are the gridCoordinates.
    /// </summary>
    /// <param name="gridPositionList"></param>
    /// <param name="gridX"></param>
    /// <param name="gridZ"></param>
    /// <param name="objectToBuild"></param>
    public void BuildOnTileSetup(List<Vector2Int> gridPositionList, int gridX, int gridZ, BuildingTypeSO objectToBuild)
    {
        Transform built = Instantiate(objectToBuild.prefab, grid.GetCellPositionInWorld(gridX, gridZ), Quaternion.identity);
        Instantiate(objectToBuild.visual, grid.GetCellPositionInWorld(gridX, gridZ), Quaternion.identity, built);

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

    public void BuildOnTile(Vector3 location, BuildingTypeSO objectToBuild)
    {
        if (CanBuild(location))
        {
            Vector3Int gridCoords = grid.GetCellOnPosition(location);

            List<Vector2Int> gridPositionList = objectToBuild.GetGridPositionList(new Vector2Int(gridCoords.x, gridCoords.z)); //list of tiles taken up by object
            Transform built = Instantiate(objectToBuild.prefab, grid.GetCellPositionInWorld(gridCoords.x, gridCoords.z), Quaternion.identity);
            Instantiate(objectToBuild.visual, grid.GetCellPositionInWorld(gridCoords.x, gridCoords.z), Quaternion.identity, built);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetObjectOnTile(built);
            }
            OnBuildingBuilt?.Invoke(this, new OnBuildingBuiltEventArgs { cost = objectToBuild.cost });
            _objectsBuilt++;
        }
        else Debug.Log("Spot is taken, cannot build!"); //pop up can't build message

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
            Vector3Int gridCoords = grid.GetCellOnPosition(location);
            List<Vector2Int> gridPositionList = _objectToBuild.GetGridPositionList(new Vector2Int(gridCoords.x, gridCoords.z)); //list of tiles taken up by object
            bool canBuild = true; //default is true
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                Cell gridObject = grid.GetGridObject(gridPosition.x, gridPosition.y);
                if (!gridObject.isSlotFree() || !gridObject.isBuildZone)
                {
                    canBuild = false;
                    break; //stop checking after one spot isn't buildable
                }
            }
            return canBuild;
        }
        else return false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))// && _objectsBuilt < 5)
        {
            if (_playerController.CanAffordBuild(_objectToBuild.cost))
            {
                Vector3 location = Utilities.GetMousePositionWorld(Camera.main, _layer);
                BuildOnTile(location, _objectToBuild);
            }
            else
            {
                Debug.Log("You can't afford that!");
            }
        }
    }
}
