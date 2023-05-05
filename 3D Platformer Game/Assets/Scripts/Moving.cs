using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float initialZ;
    public float speed = 1f;
    public GameObject player;
    public int zLength = 10;
    void Start()
    {
        //direction = new Vector3(xValue, yValue, zValue);
        initialZ = transform.position.z - initialZ;

    }

    void Update()
    {
        if(gameObject.transform.position.z >= initialZ + zLength)
        {
           speed *= -1;
        } else if (gameObject.transform.position.z <= initialZ - zLength){
           speed *= -1;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
