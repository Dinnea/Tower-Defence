using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyFactory : MonoBehaviour, IEnemyFactory
{
    GameObject enemy;

    public string path
    {
        get { return "Prefabs/Enemies/Hoverbuggy"; }
    }

    [SerializeField] GameObject _waypoints;
    [SerializeField] List<Vector3> _navPoints;

    private void Awake()
    {
        //_waypoints = ;
        enemy = (GameObject)Resources.Load(path);
        GetNavPoints();
        
    }
    public void SpawnEnemy()
    {
        if (enemy != null)
        {
            Debug.Log("Spawning enemy: " + enemy.name);
            GameObject newEnemy = Instantiate(enemy, _navPoints[0], Quaternion.Euler(0, -90, 0));
            IEnemy ai = newEnemy.GetComponent<IEnemy>();
            ai.SetNavPoints(_navPoints);
        }
    }

    public void GetNavPoints()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            _navPoints.Add(waypoint.transform.position);
        }
    }
}
