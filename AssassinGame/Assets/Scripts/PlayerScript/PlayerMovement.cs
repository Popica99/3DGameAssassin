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
    public Vector3 lookPos;

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
        runAnimation.SetFloat("Movement", Input.GetAxis("Horizontal"));
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            isPlayerGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        //Flip();
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
        /*if ((Input.mousePosition.x >= 345 && Input.GetKey(KeyCode.A)) || (Input.mousePosition.x < 345 && Input.GetKey(KeyCode.D)))
        {
            runAnimation.SetBool("Backward", true);
        }
        else runAnimation.SetBool("Backward", false);*/
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
}
