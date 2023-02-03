using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGetBuilding : MonoBehaviour, IGridCanBuild
{
   
    List<GameObject> IGridCanBuild.GetBuildingSpaces()
    {
        
        throw new System.NotImplementedException();
    }
}
