using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : GameState
{
    [SerializeField] private GameObject[] _enemiesOnField;
    [SerializeField] private float _searchCountdown = 1f;

    public System.Action onEnemiesDead;

    private void Update()
    {
        if (!areEnemiesAlive())
        {
            onEnemiesDead.Invoke();
        }
    }

    /// <summary>
    /// Checks if there are any alive enemies. Checks are done once a second to save processing power
    /// </summary>
    /// <returns></returns>
    private bool areEnemiesAlive()
    {
        _searchCountdown -= Time.deltaTime;
        _enemiesOnField = GameObject.FindGameObjectsWithTag("Enemy");

        if (_enemiesOnField.Length == 0)
        {
            Debug.Log("no enemies");
            return false;
        }
        else
        {
            _searchCountdown = 1f;
            return true;
        }
    }
}
