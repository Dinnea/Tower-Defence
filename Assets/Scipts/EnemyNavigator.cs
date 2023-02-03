using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyNavigator : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _navDestination;
    public List<Vector3> navPoints;
    private int _pointsReached = 0;
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_pointsReached < 7)
        {
            MoveToGoal();
        }
    }

    void MoveToGoal()
    {
        _navMeshAgent.destination = navPoints[_pointsReached];

        if (this.transform.position.x == navPoints[_pointsReached].x && 
            this.transform.position.z == navPoints[_pointsReached].z)
        {
            _pointsReached++;
        }
    }
}
