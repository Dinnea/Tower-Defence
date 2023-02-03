using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING}

    public List<Wave> waves;
    public int timeBetweenWaves = 15;
    public event EventHandler<bool> switchGameState;
    public GameObject[] gameObjects;

    private int _nextWave = 0;
    private float _searchCountdown = 1f;
    private float _search;
    private SpawnState _state = SpawnState.COUNTING;

    [SerializeField] private int _waveCountdown = 0;
    [SerializeField] private bool _debug = true;
    [SerializeField] private TextMeshProUGUI _countdownDisplay;
    [SerializeField] private IEnemyFactory _enemyFactory;
    private void Start()
    {
        _search = _searchCountdown;
        _enemyFactory = GetComponent<IEnemyFactory>();
        _waveCountdown = timeBetweenWaves;
        StartCoroutine(countdownToWave());

        foreach (Wave wave in waves)
        {
            wave.SetUp();
        }
    }

    private void Update()
    {
        
        if (_state == SpawnState.WAITING)
        {
            if (!areEnemiesAlive())
            {
                switchGameState?.Invoke(this, true);
                beginNewRound();
                return;
            }
            else return;
        }

        if(_waveCountdown <= 0)
        {
            if(_state != SpawnState.SPAWNING)
            {
                switchGameState?.Invoke(this, false);
                StartCoroutine(spawnWave(waves[_nextWave]));
            }            
        }        
    }
    /// <summary>
    /// Sets timer to next wave in seconds.
    /// </summary>
    /// <param name="seconds"></param>
    public void SetTimer(int seconds)
    {
        _waveCountdown = seconds;
    }

    /// <summary>
    /// Ends the defend part of the game, starts the timer to the next wave.
    /// </summary>
    private void beginNewRound()
    {
        Debug.Log("Wave completed");//begin next round
        _nextWave++;
        _state = SpawnState.COUNTING;
        _waveCountdown = timeBetweenWaves;
        StartCoroutine(countdownToWave());
    }
    /// <summary>
    /// Checks if there are any alive enemies. Checks are done once a second to save processing power
    /// </summary>
    /// <returns></returns>
    private bool areEnemiesAlive()
    {
        _searchCountdown -= Time.deltaTime;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (gameObjects.Length == 0)//GameObject.FindGameObjectsWithTag("Enemy") == null)
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

    private IEnumerator countdownToWave()
    {
        while (_waveCountdown > 0)
        {
            _countdownDisplay.text = _waveCountdown.ToString();
            
            yield return new WaitForSeconds(1f);
            _waveCountdown--;
        }
    }

    private IEnumerator spawnWave(Wave wave)
    {
        Debug.Log("Spawning wave" + wave.name);
        _state = SpawnState.SPAWNING;

        //spawn
        for (int i = 0; i<wave.countTotal; i++)
        {
            _enemyFactory.SpawnEnemy();//SpawnEnemy(wave.enemy1);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        _state = SpawnState.WAITING;
        yield break;
    }

    private void spawnEnemy(Transform enemy)
    {
        //spawn enemy
        Debug.Log("Spawning enemy: " + enemy.name);
        Instantiate(enemy, new Vector3(97.5f, 0, 7.5f), Quaternion.identity);
    }
}
