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
    private int _nextWave = 0;
    public int timeBetweenWaves = 15;
    [SerializeField] private int waveCountdown = 0;
    [SerializeField] bool debug = true;
    public event EventHandler<bool> switchGameState;

    public GameObject[] gameObjects;

    [SerializeField] TextMeshProUGUI _countdownDisplay;

    private float searchCountdown = 1f;
    float search;

    private SpawnState state = SpawnState.COUNTING;

    [SerializeField]IEnemyFactory enemyFactory;

    private void Start()
    {
        search = searchCountdown;
        enemyFactory = GetComponent<IEnemyFactory>();
        waveCountdown = timeBetweenWaves;
        StartCoroutine(CountdownToWave());

        foreach (Wave wave in waves)
        {
            wave.SetUp();
        }
    }

    private void Update()
    {
        
        if (state == SpawnState.WAITING)
        {
             //Debug.Log("Done spawning");//NewRound();
            if (!AreEnemiesAlive())// || !debug)
            {
                switchGameState?.Invoke(this, true);
                NewRound();
                return;
            }
            else return;
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                switchGameState?.Invoke(this, false);
                StartCoroutine(SpawnWave(waves[_nextWave]));
            }            
        }        
    }

    public void SetTimer(int t)
    {
        waveCountdown = t;
    }
    void NewRound()
    {
        
        Debug.Log("Wave completed");//begin next round
        _nextWave++;
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        StartCoroutine(CountdownToWave());
    }
    bool AreEnemiesAlive()
    {
        searchCountdown -= Time.deltaTime;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (gameObjects.Length == 0)//GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
            Debug.Log("no enemies");
            return false;
        }
        else 
        {
            searchCountdown = 1f;
            return true;
        } 
    }

    IEnumerator CountdownToWave()
    {

        while (waveCountdown > 0)
        {
            _countdownDisplay.text = waveCountdown.ToString();
            
            yield return new WaitForSeconds(1f);
            waveCountdown--;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave" + wave.name);
        state = SpawnState.SPAWNING;

        //spawn
        for (int i = 0; i<wave.countTotal; i++)
        {
            enemyFactory.SpawnEnemy();//SpawnEnemy(wave.enemy1);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        //spawn enemy
        Debug.Log("Spawning enemy: " + enemy.name);
        Instantiate(enemy, new Vector3(97.5f, 0, 7.5f), Quaternion.identity);
    }
}
