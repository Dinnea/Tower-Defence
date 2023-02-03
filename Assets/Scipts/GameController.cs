using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the game states (finite state machine implementation possible?)
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private WaveSpawner _spawner;
    [SerializeField] private StructureController _gridController;

    bool _switchChange = true;

    public EventHandler<OnModesSwitchedArgs> OnModesSwitchedEvent;

    public class OnModesSwitchedArgs : EventArgs
    {
        public bool switchChange;
    }

    private void Update()
    {
        _spawner.switchGameState += StateChanged;
    }

    public void SwitchModes(bool value)
    {
        //_spawner.enabled = !_spawner.enabled;
        _gridController.enabled = value;
        _switchChange = value;
        OnModesSwitchedEvent?.Invoke(this, new OnModesSwitchedArgs { switchChange = _switchChange });
    }

    void StateChanged(object sender, bool value)
    {
        SwitchModes(value);
    }
}
