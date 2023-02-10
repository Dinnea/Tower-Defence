using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTower : MonoBehaviour
{
    [SerializeField] public CapsuleCollider range;
    [SerializeField] protected bool isActive = false;
    [SerializeField] protected float actionCD = 5.0f;
    [SerializeField] protected float cdLeft = 0f;
    public System.Action onAction;

    private void Awake()
    {
        OnAwake();
    }
    protected virtual void Action() { }
    protected virtual void OnAwake()
    {
        range = GetComponentInChildren<CapsuleCollider>();
    }

    protected virtual IEnumerator countdownToAction()
    {
        while (cdLeft > 0)
        {
            yield return new WaitForSeconds(1.0f);
            cdLeft--;
        }
    }
    public void SetActive(bool value)
    {
        isActive = value;
        cdLeft = actionCD;
        StartCoroutine(countdownToAction());
    }

}
