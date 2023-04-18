using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float initialZ;
    public float xValue = 1f;
    public float yValue = 1f;
    public float zValue = 1f;
    public float speed = 1f;
    Vector3 direction = new Vector3(1f, 1f, 1f);
    public GameObject player;

    void Start()
    {
        //direction = new Vector3(xValue, yValue, zValue);
        initialZ = transform.position.z;

    }

    void Update()
    {
        if(gameObject.transform.position.z >= initialZ + 10)
        {
           speed *= -1;
        } else if (gameObject.transform.position.z <= initialZ - 10){
           speed *= -1;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    
    /*void OnCollisionEnter(Collision col) //Checks for a collision
    {
        if (col.gameObject.tag == "change")
        {
            speed *= -1;
        }
    }*/
}
