using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    //[SerializeReference] public LevelFSM manager;
    //protected LevelFSM.GameStateEnum gameState;
    private void Awake()
    {
       OnAwake();
    }

    protected virtual void OnAwake() { }
    //{
    //    manager = GetComponentInParent<LevelFSM>();
    //}
}