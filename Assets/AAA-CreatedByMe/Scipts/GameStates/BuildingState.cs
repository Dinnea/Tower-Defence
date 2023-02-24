using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuildingState : GameState
{
    [SerializeField] private int _waveCountdown;
    [SerializeField] private int _waveNr;
    [SerializeField] private TextMeshProUGUI _countdownDisplay;
    public System.Action<int> onStateEnd;
    public System.Action<int> onClockTick;

    private void OnEnable()
    {
        //_waveCountdown = manager.GetTimeBetweenWaves();
        //StartCoroutine(countdownToWave());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Update()
    {
        if(_waveCountdown <= 0)
        {
            onStateEnd?.Invoke(_waveNr);
        }
    }
    public IEnumerator CountdownToWave()
    {
        while (_waveCountdown > 0)
        {
            onClockTick?.Invoke(_waveCountdown);

            yield return new WaitForSeconds(1f);
            _waveCountdown--;
        }
    }

    public void SetWaveCountdown(int waveCountdown) 
    {
        _waveCountdown = waveCountdown;
    }

    public void EndStateEarly()
    {
        StopAllCoroutines();
        _waveCountdown = 0;
    }

    public void SetWaveNr(int nr)
    {
        _waveNr = nr;
    }

    public int GetWaveNr(int nr)
    {
        return _waveNr;
    }
}
