using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    public void SpawnEnemy();
    public void GetNavPoints();
    string path { get; }
}