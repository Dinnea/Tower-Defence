using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class aoeTower : AbstractTower
{
    private void Update()
    {
        if (isActive)
        {
           if(cdLeft == 0)
           {
                Action();
           }
        }
    }

    protected override void Action()
    {
        onAction?.Invoke();
        Debug.Log("something is happening");
        cdLeft = actionCD;
        StartCoroutine(countdownToAction());
    }

}
