using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;


public class StateChanged
{
    public LevelFSM.GameStateEnum state;
    public StateChanged(LevelFSM.GameStateEnum pState)
    {
        this.state = pState;
    }

}

public class LevelFSM : MonoBehaviour
{
    public enum GameStateEnum { SPAWNING, WAITING, BUILDING, GAMEOVER}
    private Dictionary<GameStateEnum, GameState> _stateDictionary;

    [SerializeField] private List<Wave> waves;
    public Wave GetCurrentWave()
    {
        return waves[_currentWave];
    }

    public int GetWaveNumber()
    {
        return _currentWave;
    }

    [SerializeField] private int _currentWave = -1;
    [SerializeField] private int timeBetweenWaves = 15;
    public int GetTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }

    public GameObject[] gameObjects;

    private GameStateEnum _state;

    [SerializeField] private IEnemyFactory _enemyFactory;

    private void Awake()
    {
        foreach (Wave wave in waves)
        {
            wave.SetUp();
        }
        _stateDictionary = new Dictionary<GameStateEnum, GameState>
        {
            { GameStateEnum.BUILDING, GetComponentInChildren<BuildingState>(true) },
            { GameStateEnum.SPAWNING, GetComponentInChildren<SpawningState>(true) },
            { GameStateEnum.WAITING,  GetComponentInChildren<WaitingState> (true) },
            { GameStateEnum.GAMEOVER, GetComponentInChildren<GameOverState>(true) }
        };

        SwitchToBuildingState();

    }

    private void Start()
    {
        
    }
    private void subscribeToState()
    {
        switch (_state)
        {
            case GameStateEnum.BUILDING:
                (_stateDictionary[_state] as BuildingState).onStateEnd += SwitchToSpawningState;
                break;

            case GameStateEnum.SPAWNING:
               (_stateDictionary[_state] as SpawningState).onSpawningFinished += SwitchToWaitingState;
                break;

            case GameStateEnum.WAITING:
                (_stateDictionary[_state] as WaitingState).onEnemiesDead += SwitchToBuildingState;
                break;

            case GameStateEnum.GAMEOVER:
                break;
        }
    }
    private void unsubscribeFromState()
    {
        switch (_state)
        {
            case GameStateEnum.BUILDING:
                (_stateDictionary[_state] as BuildingState).onStateEnd -= SwitchToSpawningState;
                break;

            case GameStateEnum.SPAWNING:
                (_stateDictionary[_state] as SpawningState).onSpawningFinished -= SwitchToWaitingState;
                break;

            case GameStateEnum.WAITING:
                (_stateDictionary[_state] as WaitingState).onEnemiesDead -= SwitchToBuildingState;
                break;

            case GameStateEnum.GAMEOVER:
                break;
        }
    }

    private void OnEnable()
    {
       //subscribeToState();
    }
    private void OnDisable()
    {
        unsubscribeFromState();
    }
    public void SwitchToBuildingState()
    {
        _currentWave++;
        switchGameState(GameStateEnum.BUILDING);
        handleBuildingState(_stateDictionary[_state] as BuildingState);
    }
    public void SwitchToSpawningState(int waveNr)
    {
        switchGameState(GameStateEnum.SPAWNING);
        handleSpawningState(_stateDictionary[_state] as SpawningState);
    }
    public void SwitchToWaitingState()
    {        
        switchGameState(GameStateEnum.WAITING);
    }
    private void switchGameState(GameStateEnum newState)
    {
        unsubscribeFromState();
        _stateDictionary[_state].gameObject.SetActive(false);

        _state = newState;

        _stateDictionary[_state].gameObject.SetActive(true);
        subscribeToState();
    }

    private void handleBuildingState(BuildingState state)
    {
        state.SetWaveCountdown(timeBetweenWaves);
        StartCoroutine(state.CountdownToWave());
        state.SetWaveNr(_currentWave);
    }

    private void handleSpawningState(SpawningState state)
    {
        StartCoroutine(state.SpawnWave(GetCurrentWave()));
    }
}
