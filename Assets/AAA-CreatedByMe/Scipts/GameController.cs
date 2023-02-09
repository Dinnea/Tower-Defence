using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the game states (finite state machine implementation will be done properly later)
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private LevelFSM _spawner;
    [SerializeField] private GridManager _structureController;

    private bool _switchChange = true;

    public EventHandler<OnModesSwitchedArgs> OnModesSwitchedEvent;

    public class OnModesSwitchedArgs : EventArgs
    {
        public bool switchChange;
    }

    private void Update()
    {
        _spawner.onSwitchGameState += stateChanged;
    }

    public void SwitchModes(bool value)
    {
        //_spawner.enabled = !_spawner.enabled;
        _structureController.enabled = value;
        _switchChange = value;
        OnModesSwitchedEvent?.Invoke(this, new OnModesSwitchedArgs { switchChange = _switchChange });
    }

    private void stateChanged(object sender, bool value)
    {
        SwitchModes(value);
    }
}
