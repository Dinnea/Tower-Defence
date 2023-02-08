using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetUp : MonoBehaviour
{
    private BuildingTypeSO _hq;
    private List<Vector3> _buildableLocations;
    [SerializeField] private GridManager _structureController;


    private void Awake()
    {
       getBuildableAreas();
        _hq =_structureController.GetBuildingType(3);
    }

    private void Start()
    {
        buildLayout();
    }

    /// <summary>
    /// Contains building functions to be executed when the level starts.
    /// </summary>
    private void buildLayout()
    {
        buildHQ(0,0);
        setUpBuildableAreas();
    }

    /// <summary>
    /// Places the HQ building - this is the building that will be defended. Must be reachable by enemies.
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridZ"></param>
    private void buildHQ(int gridX, int gridZ)
    {
        List<Vector2Int> gridPositionList = _hq.GetGridPositionList(new Vector2Int(gridX, gridZ));
        _structureController.BuildOnTileSetup(gridPositionList, gridX, gridZ, _hq);
    }

    /// <summary>
    /// Finds all the positions which are meant to be buildable.
    /// </summary>
    void getBuildableAreas()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        _buildableLocations = new List<Vector3>(children.Length);
        foreach (Transform child in children)
        {
            _buildableLocations.Add(child.position);
        }
        _buildableLocations.RemoveAt(0);
    }
    /// <summary>
    /// Marks cells at buildable locations as buildzones.
    /// </summary>
    void setUpBuildableAreas()
    {
        foreach (Vector3 location in _buildableLocations)
        {
            Vector2Int gridCoords =  _structureController.grid.GetCellOnWorldPosition(location);
            _structureController.grid.GetGridObject(gridCoords.x, gridCoords.y).isBuildZone = true;            
        }
    }
}
