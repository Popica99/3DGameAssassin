using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float vertical;
    private float horizontal;
    private float speedForSprint = 4f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isPlayerGrounded = true;
    private bool isMovingVertical = false;
    private bool isMovingHorizontal = false;

    private Vector3 lastPos;

    private Animator runAnimation;
    bool animationStarted = false;
    bool animationStartedA = false;
    bool animationStartedD = false;
    bool anyMovementKeyPressed = false;

    private void Start()
    {
        runAnimation = GetComponent<Animator>();
        lastPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        //runAnimation.SetFloat("TrStartRunFloat1", Math.Abs(Input.GetAxis("Horizontal")));
        //runAnimation.SetFloat("TrStartRunFloat2", Input.GetAxis("Horizontal"));
        //StartCoroutine(DelayBetweenAnim());
        // Check if the A key is pressed and the animation hasn't started yet
        /*if (Input.GetKeyDown(KeyCode.A) && !animationStartedA && !animationStartedD)
        {
            runAnimation.SetTrigger("TrStartRun"); // Start the animation
            animationStartedA = true; // Set the flag to true indicating animation has started by A
        }

        // Check if the D key is pressed and the animation hasn't started yet
        if (Input.GetKeyDown(KeyCode.D) && !animationStartedD && !animationStartedA)
        {
            runAnimation.SetTrigger("TrStartRun"); // Start the animation
            animationStartedD = true; // Set the flag to true indicating animation has started by D
        }

        // Check if the A key is released and reset the flag
        if (Input.GetKeyUp(KeyCode.A))
        {
            animationStartedA = false;
            if (!animationStartedD) // Check if animation hasn't started by D
                runAnimation.SetTrigger("TrStopRun"); // Stop the animation
        }

        // Check if the D key is released and reset the flag
        if (Input.GetKeyUp(KeyCode.D))
        {
            animationStartedD = false;
            if (!animationStartedA) // Check if animation hasn't started by A
                runAnimation.SetTrigger("TrStopRun"); // Stop the animation
        }*/
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontal * speedForSprint * 2, rb.velocity.y);
            runAnimation.speed = 1.5f;
            //rb.velocity = new Vector2(rb.velocity.x, vertical * speed * 1.2f);
        }
        else runAnimation.speed = 1f;*/
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            isPlayerGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        Flip();
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speedForSprint, rb.velocity.y);
        //rb.velocity = new Vector2(rb.velocity.x, vertical * speedForSprint);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontal * speedForSprint * 2, rb.velocity.y);
            runAnimation.speed = 1.5f;
            //rb.velocity = new Vector2(rb.velocity.x, vertical * speed * 1.2f);
        }
        else runAnimation.speed = 1f;
    }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Platform")
            {
                isPlayerGrounded = true;
            }
        }

        private void Flip()
        {


            if (horizontal < 0f)
            {
                isFacingRight = !isFacingRight;
                /*Vector3 localScale = transform.localScale;
                localScale.z *= -1f;
                transform.localScale = localScale;*/
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                /*Vector3 localScale = transform.localScale;
                localScale.z *= -1f;
                transform.localScale = localScale;*/
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
    private IEnumerator DelayBetweenAnim()
    {
        if (Input.GetKeyDown(KeyCode.A) && !animationStartedA)
        {
            runAnimation.SetTrigger("TrStartRun"); // Start the animation
            animationStartedA = true; // Set the flag to true indicating animation has started by A
        }

        // Check if the D key is pressed and the animation hasn't started yet
        if (Input.GetKeyDown(KeyCode.D) && !animationStartedD)
        {
            runAnimation.SetTrigger("TrStartRun"); // Start the animation
            animationStartedD = true; // Set the flag to true indicating animation has started by D
        }

        // Check if the A key is released and reset the flag
        yield return new WaitForSeconds(0.5f);
        if (Input.GetKeyUp(KeyCode.A))
        {
            animationStartedA = false;
            if (!animationStartedD) // Check if animation hasn't started by D
                runAnimation.SetTrigger("TrStopRun"); // Stop the animation
        }

        // Check if the D key is released and reset the flag
        yield return new WaitForSeconds(0.5f);
        if (Input.GetKeyUp(KeyCode.D))
        {
            animationStartedD = false;
            if (!animationStartedA) // Check if animation hasn't started by A
                runAnimation.SetTrigger("TrStopRun"); // Stop the animation
        }
    }
}
