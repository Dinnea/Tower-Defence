using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    [SerializeReference] protected LevelFSM manager;
    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        manager = GetComponentInParent<LevelFSM>();
    }
}