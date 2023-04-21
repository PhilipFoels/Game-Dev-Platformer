using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Player")]
    public float xpos;
    public float ypos;
    public float zpos;
    public Transform groundCheck;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    public Transform orientation;
    public int startLives;
    public int livesLeft;
    public int score = 0;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    GameObject [] allCheckpoints;
    GameObject [] allCoins;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;

        allCheckpoints = GameObject.FindGameObjectsWithTag("checkpoint");
        allCoins = GameObject.FindGameObjectsWithTag("coin");
    }

    private void Update()
    {
        // ground check
        grounded = Physics.CheckSphere(groundCheck.position, 0.2f, whatIsGround); //checks whether or not the player is on the ground

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (gameObject.transform.position.y <= 0) { //checks to see if the player's y coordinate is less than or equal to zero
            livesLeft--;
            gameObject.transform.position = new Vector3(xpos, ypos, zpos); //changes the player's position to the position held in xpos, ypos, and zpos
            rb.velocity = new Vector3(0f, 0f, 0f); //Resets the player's velocities
        }
        if (livesLeft < 0){
            xpos = 0;
            ypos = 10;
            zpos = 0;
            gameObject.transform.position = new Vector3(xpos, ypos, zpos);
            livesLeft = 3;
            score = 0;
            foreach (GameObject go in allCheckpoints){ //resets all of the deactivated checkpoints so they can all be used when the game is reset
                if (!go.activeInHierarchy){
                    go.SetActive(true);
                }
            }
            foreach (GameObject go in allCoins){
                if (!go.activeInHierarchy){
                    go.SetActive(true);
                }
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            int startScene = 0;
            SceneManager.LoadScene(startScene);
        }
    }

    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "checkpoint")//Checks if the collision is tagged as a checkpoint
        {
            //Sets the reset positions to the checkpoint
            xpos = col.gameObject.transform.position.x;
            ypos = 9;
            zpos = col.gameObject.transform.position.z;
            
            col.gameObject.SetActive(false); //makes is so checkpoints can't be used more than once
        }
    }
    void OnCollisionEnter(Collision col) //Checks for a collision
    {
        if (col.gameObject.tag == "finish")//Checks if the collision is tagged as a finish
        {
            //Goes to the next scene
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex){
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
        if (col.gameObject.tag == "coin")
        {
            col.gameObject.SetActive(false);
            score += 10;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}

