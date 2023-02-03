using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [SerializeField] GameController _gameController;
    [SerializeField] GameObject _ui;
    

    private void Update()
    {
        _gameController.OnModesSwitchedEvent += GameModesChanged;
    }

    void Display(bool value)
    {
        _ui.SetActive(value);
    }

    public void GameModesChanged(object sender, GameController.OnModesSwitchedArgs args)
    {
        Display(args.switchChange);
    }

}
