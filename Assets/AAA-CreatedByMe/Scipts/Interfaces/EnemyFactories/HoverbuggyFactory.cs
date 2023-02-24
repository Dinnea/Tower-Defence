using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverbuggyFactory : MonoBehaviour, IEnemyFactory
{
    private GameObject _enemy;

    private Vector3 _spawnLocation;

    public string path
    {
        get { return "Prefabs/Enemies/Hoverbuggy"; }
    }

    //[SerializeField] private GameObject _waypoints;
    [SerializeField] private List<Vector3> _waypoints;

    private void Awake()
    {
        //_waypoints = ;
        _enemy = (GameObject)Resources.Load(path);
        SetWayPoints();
        SetSpawnLocation(_waypoints[0]);
        
    }
    public void SpawnEnemy()
    {
        if (_enemy != null)
        {
            Debug.Log("Spawning enemy: " + _enemy.name);
            GameObject newEnemy = Instantiate(_enemy, _waypoints[0], Quaternion.Euler(0, -90, 0));
            Enemy ai = newEnemy.GetComponent<Enemy>();
            ai.GetMoveable().SetNavigation();
        }
    }

    public void SetWayPoints()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            _waypoints.Add(waypoint.transform.position);
        }
    }

    public void SetSpawnLocation(Vector3 location)
    {
        _spawnLocation = location;
    }

    public List<Vector3> GetWayPoints()
    {
        return _waypoints;
    }
}
