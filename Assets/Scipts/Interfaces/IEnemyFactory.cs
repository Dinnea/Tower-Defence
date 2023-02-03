using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemyFactory
{
    public void SpawnEnemy();
    public void GetNavPoints();
    string path { get; }
}