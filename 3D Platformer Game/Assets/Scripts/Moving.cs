using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float xSpeed = 1f;
    public float ySpeed = 1f;
    public float zSpeed = 1f;

    public int xLength = 10;
    public int yLength = 10;
    public int zLength = 10;

    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;

    Vector3 direction = new Vector3(1f, 1f, 1f);

    void Start()
    {
        xOffset = transform.position.x - xOffset;
        yOffset = transform.position.y - yOffset;
        zOffset = transform.position.z - zOffset;
    }

    void Update()
    {
        //x movement
        if (gameObject.transform.position.x >= xOffset + xLength)
        {
            xSpeed *= -1;
        }
        else if (gameObject.transform.position.x <= xOffset - xLength)
        {
            xSpeed *= -1;
        }

        //y movement
        if (gameObject.transform.position.y >= yOffset + yLength)
        {
            ySpeed *= -1;
        }
        else if (gameObject.transform.position.y <= yOffset - yLength)
        {
            ySpeed *= -1;
        }

        //z movement
        if (gameObject.transform.position.z >= zOffset + zLength)
        {
            zSpeed *= -1;
        }
        else if (gameObject.transform.position.z <= zOffset - zLength)
        {
            zSpeed *= -1;
        }

        //applies the movement to the object
        transform.Translate(Vector3.right * Time.deltaTime * xSpeed); //x axis
        transform.Translate(Vector3.up * Time.deltaTime * ySpeed); //y axis
        transform.Translate(Vector3.forward * Time.deltaTime * zSpeed); //z axis
    }
}
