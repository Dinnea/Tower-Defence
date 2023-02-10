using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] LaserTower _owner;
    [SerializeField] ParticleSystem _vfx;
    
    void Start()
    {
        _owner = GetComponentInParent<LaserTower>();
        _vfx = GetComponentInChildren<ParticleSystem>();
        _owner.onAction += playVFX;
    }

    void playVFX()
    {
        _vfx.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
