using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;
    public Transform enemy1;
    public Transform enemy2;
    public Transform enemy3;


    public int count1;
    public int count2;
    public int count3;
    public int countTotal;

    public float rate = 0.5f;

    public void SetUp()
    {
        countTotal = count1 + count2 + count3;
    }
}
