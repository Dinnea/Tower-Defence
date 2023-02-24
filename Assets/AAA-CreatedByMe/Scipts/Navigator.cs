using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Navigator : MonoBehaviour, IMoveable
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
            Move();
        }
    }

 

    public void Move()
    {
        _navMeshAgent.destination = navPoints[_pointsReached];

        if (transform.position.x == navPoints[_pointsReached].x &&
            transform.position.z == navPoints[_pointsReached].z)
        {
            _pointsReached++;
        }
    }

    public void SetNavigation()
    {
        throw new NotImplementedException();
    }
}
