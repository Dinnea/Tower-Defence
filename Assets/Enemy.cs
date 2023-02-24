using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private IAttackable attackable;
    private IAttacker attacker;
    private IMoveable moveable;

    protected void ExecuteOnAwake()
    {
        attackable = GetComponent<IAttackable>();
        attacker = GetComponent<IAttacker>();
        moveable = GetComponent<IMoveable>();
    }

    public IAttackable GetAttackable()
    {
        return attackable;
    }

    public IAttacker GetAttacker()
    {
        return attacker;
    }

    public IMoveable GetMoveable()
    {
        return moveable;
    }
}
