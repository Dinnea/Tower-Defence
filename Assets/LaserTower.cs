using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : AbstractTower
{
    [SerializeField] private List<GameObject> _enemiesInRange;
    [SerializeField] private GameObject _target;
    [SerializeField] private Transform _barrel;
    bool hasShot = false;
    private void Update()
    {
        if (_enemiesInRange.Count > 0)
        {
            if (_target != _enemiesInRange[0]) _target = _enemiesInRange[0];
        }
        else _target = null;

        if(_target != null)
        {
            _barrel.LookAt(_target.transform);
            if(cdLeft == 0) Action();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_enemiesInRange.Contains(other.gameObject))
            {
                _enemiesInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            _enemiesInRange.Remove(other.gameObject);
        }
    }

    protected override void Action()
    {
        onAction?.Invoke();
        Debug.Log("something is happening");
        cdLeft = actionCD;
        StartCoroutine(countdownToAction());
    }

    protected override void OnAwake()
    {
        base.OnAwake();
    }
}
