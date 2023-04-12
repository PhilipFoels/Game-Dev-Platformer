using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    public float xValue = 1f;
    public float yValue = 1f;
    public float zValue = 1f;
    public float speed = 1f;
    Vector3 direction = new Vector3(1f, 1f, 1f);

    void Start()
    {
        direction = new Vector3(xValue, yValue, zValue);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision col) //Checks for a collision
    {
        if (col.gameObject.tag == "change")
        {
            speed *= -1;
        }
    }
}
