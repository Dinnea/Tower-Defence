using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuildingState : GameState
{
    [SerializeField] private int _waveCountdown = 0;
    [SerializeField] private TextMeshProUGUI _countdownDisplay;
    public System.Action onStateEnd;
    public System.Action<int> onClockTick;

    private void OnEnable()
    {
        _waveCountdown = manager.GetTimeBetweenWaves();
        StartCoroutine(countdownToWave());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void Update()
    {
        if(_waveCountdown <= 0)
        {
            onStateEnd?.Invoke();
        }
    }
    private IEnumerator countdownToWave()
    {
        while (_waveCountdown > 0)
        {
            onClockTick?.Invoke(_waveCountdown);

            yield return new WaitForSeconds(1f);
            _waveCountdown--;
        }
    }

    public void EndStateEarly()
    {
        StopAllCoroutines();
        _waveCountdown = 0;
    }
}
