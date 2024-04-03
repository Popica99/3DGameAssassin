using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerObj;

    private float vertical;
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool isPlayerGrounded = true;
    private bool isMovingVertical = false;
    private bool isMovingHorizontal = false;

    // Update is called once per frame
    void Update()
    {
        //vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            isPlayerGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        Flip();
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontal * speed * 2, rb.velocity.y);
            //rb.velocity = new Vector2(rb.velocity.x, vertical * speed * 1.2f);
        }
       
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
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.z *= -1f;
            transform.localScale = localScale;
        }
    }

}
