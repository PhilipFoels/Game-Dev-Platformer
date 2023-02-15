using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public float jumpForce;
    public float gravity;
    public float speed;
    public Transform orientation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = orientation.forward * vertical + orientation.right * horizontal;

        if(direction.magnitude >= 0.1f){
            rb.AddForce(direction * speed * Time.deltaTime);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, whatIsGround);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        rb.AddForce(Vector3.down * gravity * Time.deltaTime);
    }
}
