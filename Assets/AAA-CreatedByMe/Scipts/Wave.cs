using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string name;

    [Header("Enemies present in wave")]
    public bool enemyType1;
    public bool enemyType2;
    public bool enemyType3;


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
