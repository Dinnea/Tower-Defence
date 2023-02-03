using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverbuggy : MonoBehaviour, IEnemy
{
    EnemyNavigator _navMesh;
    List<Vector3> _navPoints;
    private void Awake()
    {
        _navMesh = GetComponent<EnemyNavigator>();
    }

    private void Start()
    {
        _navMesh.navPoints = _navPoints;
    }
    public void SetNavPoints(List<Vector3> navPoints)
    {
        _navPoints = navPoints;
    }
}
