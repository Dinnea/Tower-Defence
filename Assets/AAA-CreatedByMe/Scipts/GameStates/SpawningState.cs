using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningState : GameState
{
    [SerializeField] private Wave _wave;
    Dictionary<string, IEnemyFactory> _factories;
    public System.Action onSpawningFinished;

    private void OnEnable()
    {
        _wave = manager.GetCurrentWave();
        StartCoroutine(spawnWave(_wave));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private IEnumerator spawnWave(Wave wave)
    {
        Debug.Log("Spawning wave " + wave.name);

        //spawn
        for (int i = 0; i < wave.countTotal; i++)
        {
            yield return new WaitForSeconds(1f / wave.rate);
            _factories["basic"].SpawnEnemy();
        }
        onSpawningFinished?.Invoke();
        yield break;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        _factories = new Dictionary<string, IEnemyFactory>()
        {
            {"basic", GetComponent<BasicEnemyFactory>() }
        };
    }
}
