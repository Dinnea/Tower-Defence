using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    public void SpawnEnemy();
    public void SetWayPoints();
    public List<Vector3> GetWayPoints();
    string path { get; }
}