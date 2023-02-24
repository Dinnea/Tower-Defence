using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetWaveNr : MonoBehaviour
{
    [SerializeField] private BuildingState _state;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _state.onStateEnd += setText;
    }
    private void OnDisable()
    {
        _state.onStateEnd -= setText;
    }

    private void setText(int waveNr)
    {
        _text.text = "Wave: " + (waveNr + 1).ToString();
    }
}
