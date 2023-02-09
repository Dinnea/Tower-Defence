using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbullet : MonoBehaviour
{
    public GameObject target = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3.MoveTowards(transform.position, target.transform.position, 1);
            if(transform.position == target.transform.position)
            {
                target.transform.position = new Vector3(1000, 1000, 1000);
                Destroy(target);
                Destroy(gameObject);

            }
        }
    }
}
