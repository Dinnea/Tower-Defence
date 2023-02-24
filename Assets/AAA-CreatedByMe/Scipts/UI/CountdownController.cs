using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Contains countdown to wave.
/// </summary>
public class CountdownController : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    private TextMeshProUGUI _countdownText;

    private void Awake()
    {
        _countdownText = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        if (gameState is BuildingState)
        {
            (gameState as BuildingState).onClockTick += updateTimer;
            (gameState as BuildingState).onStateEnd += emptySelf;
        }
    }

    private void OnDisable()
    {
       if(gameState is BuildingState)
            (gameState as BuildingState).onClockTick = updateTimer;
    }

    private void updateTimer(int seconds)
    {
        _countdownText.text = seconds.ToString();
    }

    private void emptySelf(int notUsed)
    {
        _countdownText.text = "";
    }
}
