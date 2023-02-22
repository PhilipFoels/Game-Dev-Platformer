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
    public float xpos;
    public float ypos;
    public float zpos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //references rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //references horizontal axis
        float vertical = Input.GetAxisRaw("Vertical"); //references vertical axis

        Vector3 direction = orientation.forward * vertical + orientation.right * horizontal; //creates a vector called direction

        if(direction.magnitude >= 0.1f){ //moves in the direction of the vector direction
            rb.AddForce(direction * speed * Time.deltaTime); //moves the player
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, whatIsGround); //checks whether or not the player is on the ground

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){ //jumps when space is pressed
            rb.AddForce(Vector3.up * jumpForce); //makes the player jump
            isGrounded = false; //changes the isGrounded state to false while the object is in the air after jumping
        }

        if(gameObject.transform.position.y <= 0){ //checks to see if the player's y coordinate is less than or equal to zero
            gameObject.transform.position = new Vector3(xpos, ypos, zpos); //changes the player's position to the position held in xpos, ypos, and zpos
        }

        rb.AddForce(Vector3.down * gravity * Time.deltaTime); //adds gravity
    }
}
