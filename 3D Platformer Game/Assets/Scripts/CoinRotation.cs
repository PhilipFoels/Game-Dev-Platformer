using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public float height = 0.5f;
    public float bobSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed, 0, 0) * Time.deltaTime);
        //Vector3 pos = transform.position;
        //float newY = Mathf.Sin(Time.time * bobSpeed) * height + pos.y;
        //transform.position = new Vector3(pos.x, newY, pos.z) * height;

    }
}
