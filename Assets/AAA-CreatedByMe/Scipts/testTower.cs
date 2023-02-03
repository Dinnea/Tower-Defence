using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTower : MonoBehaviour
{
    [SerializeField] List<GameObject> targetQ = new List<GameObject>();
    [SerializeField] GameObject target = null;
    [SerializeField] GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetQ.Count>0) target = targetQ[0];

        if(target != null)
        {
            GameObject shot = Instantiate(bullet, transform.position, transform.rotation);
            shot.GetComponent<testbullet>().target = target;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targetQ.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (targetQ.Contains(other.gameObject))
        {
            targetQ.Remove(other.gameObject);
        }
    }




}
