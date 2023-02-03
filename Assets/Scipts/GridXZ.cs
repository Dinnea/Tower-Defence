using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;

/// <summary>
/// Class for creating and managing a grid set on the XZ dimensions. 
/// </summary>
/// <typeparam name="TGenericGridObject"></typeparam>
public class GridXZ<TGenericGridObject>
{
    public static bool debug = false;
    private int _columns;
    private int _rows;
    private float _cellSize;
    private Vector3 _origin;

    private TGenericGridObject[,] _gridArray;
    private TextMesh[,] _debugTextArray;

    private Vector3 _offset;

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }
    /// <summary>
    /// Horizontal grid, set on the XZ dimensions. Can contain anything. 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="rows"></param>
    /// <param name="cellSize"></param>
    /// <param name="origin"></param>
    /// <param name="createDefaultGridObject"></param>
    public GridXZ(int columns, int rows, float cellSize, Vector3 origin, Func<GridXZ<TGenericGridObject>, int, int, TGenericGridObject> createDefaultGridObject) 
    {
        _columns = columns;
        _rows = rows;
        _cellSize = cellSize;
        _origin = origin;
        _offset = new Vector3(_cellSize, 0, _cellSize) * 0.5f; //origin of cells set to middle

        _gridArray = new TGenericGridObject[columns, rows];
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < _gridArray.GetLength(1); z++)
            {
                _gridArray[x, z] = createDefaultGridObject(this, x, z);
            }
        }


        //Debug, show the grid visualisation + labelled cells (X, Z position + name of the object on cell)
        _debugTextArray = new TextMesh[columns, rows];

        if (debug)
        {
            showDebug(columns, rows);
        }
       
    }

    /// <summary>
    /// Show the debug gizmos; XZ coords, grid lines, name of the grid object content. 
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="rows"></param>
    private void showDebug(int columns, int rows)
    {
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < _gridArray.GetLength(1); z++)
            {

                _debugTextArray[x, z] = Utilities.CreateTextInWorld(_gridArray[x, z]?.ToString(), null, GetCellPositionInWorld(x, z) + _offset, 10, Color.white, new Vector3(90, 0, 0), TextAnchor.MiddleCenter);
                Debug.DrawLine(GetCellPositionInWorld(x, z), GetCellPositionInWorld(x, z + 1), Color.white, 1000f);
                Debug.DrawLine(GetCellPositionInWorld(x, z), GetCellPositionInWorld(x + 1, z), Color.white, 1000f);
            }
        }

        Debug.DrawLine(GetCellPositionInWorld(0, rows), GetCellPositionInWorld(columns, rows), Color.white, 1000f);
        Debug.DrawLine(GetCellPositionInWorld(columns, 0), GetCellPositionInWorld(columns, rows), Color.white, 1000f);

        //If object on the grid changed, update the text

        OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
            _debugTextArray[eventArgs.x, eventArgs.z].text = _gridArray[eventArgs.x, eventArgs.z].ToString();
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>Amount of columns in the grid (int)</returns>
    public int GetWidthInColumns()
    {
        return _columns;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Amount of rows in the grid (int)</returns>
    public int GetHeightInColumns()
    {
        return _rows;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetCellSize()
    {
        return _cellSize;
    }
    /// <summary>
    /// Convert grid coords to world position. Needs to be within the grid.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>World position at location column x, row z.</returns>
    public Vector3 GetCellPositionInWorld(int x, int z) 
    {
        if(x<=_columns && z<=_rows && _columns>0 && _rows>0)
        {
            return new Vector3(x, 0, z) * _cellSize + _origin;
        }
        
        else
        {
            Debug.Log("Error, the location is outside the bounds of the grid");
            return Vector3.zero;
        }
    }
  
    /// <summary>
    /// Converts world postition to grid coords.
    /// </summary>
    /// <param name="worldposition"></param>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns>Returns the grid coords (x, 0, z) </returns>
    public Vector3Int GetCellOnPosition(Vector3 worldposition)
    {
        int x = Mathf.FloorToInt((worldposition-_origin).x / _cellSize);
        int z = Mathf.FloorToInt((worldposition -_origin).z / _cellSize);

        return new Vector3Int(x,0, z);
    }
    /// <summary>
    /// Set grid object on grid using grid coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <param name="value"></param>
    public void SetGridObject(int x, int z, TGenericGridObject value) //
    {
        if  ( (x>= 0 && z >=0) && (x<_columns && z<_rows) )
        {
            _gridArray[x, z] = value;
           // if(debug) _debugTextArray[x, z].text = _gridArray[x,z].ToString();
           TriggerGridObjectChanged(x, z);
        }
    }
    /// <summary>
    /// Set grid object on grid using world position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="value"></param>
    public void SetGridObject(Vector3 worldPosition, TGenericGridObject value) //set value based on world position
    {
        Vector3Int coords = GetCellOnPosition(worldPosition);
        SetGridObject(coords.x, coords.z, value);
    }

   
    public void TriggerGridObjectChanged(int x, int z) {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }

    /// <summary>
    /// Find object on grid coords x, z. 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public TGenericGridObject GetGridObject(int x, int z) 
    {
        if ((x >= 0 && z >= 0) && (x < _columns && z < _rows))
        {
            return _gridArray[x, z];
        }
        else return default;
    }

    /// <summary>
    /// Returns object on grid at world position.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public TGenericGridObject GetGridObject(Vector3 worldPosition)
    {
        Vector3Int gridCoords =  GetCellOnPosition(worldPosition);
        return GetGridObject(gridCoords.x, gridCoords.z);
    }
}
