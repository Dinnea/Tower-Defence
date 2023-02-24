using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningState : GameState
{
    //[SerializeField] private Wave _wave;
    Dictionary<string, IEnemyFactory> _factories;
    public System.Action onSpawningFinished;

    private void OnEnable()
    {
        //StartCoroutine(SpawnWave(_wave));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public IEnumerator SpawnWave(Wave wave)
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

    //public void SetWave(Wave wave)
    //{
    //    _wave = wave;
    //}
    protected override void OnAwake()
    {
      
        _factories = new Dictionary<string, IEnemyFactory>()
        {
            {"basic", GetComponent<HoverbuggyFactory>() }
        };
    }
}
