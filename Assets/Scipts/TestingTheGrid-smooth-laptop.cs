using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Utilities;

/*public class TestingTheGrid : MonoBehaviour
{
    private GridXZ<int> _grid;
    [SerializeField] LayerMask _layer;

    private void Start()
    {
        //_grid = new GridXZ<int>(5, 5, 10, new Vector3(0, 0, 0));
        for(int x = 0; x<5; x++)
        {
            for (int y =0; y<5; y++)
            {
                _grid.SetGridObject(x, y, 0);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int value = _grid.GetGridObject(Utilities.GetMousePositionWorld(Camera.main, _layer));
           _grid.SetGridObject(Utilities.GetMousePositionWorld(Camera.main, _layer), value+1);
        }

        if (Input.GetMouseButtonDown(1))
        {
           // Debug.Log(_grid.GetValue(Utilities.GetMouseInWorldPositionXZ()));
        }
    }
}*/
