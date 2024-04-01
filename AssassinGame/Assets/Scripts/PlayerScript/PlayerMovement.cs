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
            Debug.Log("Player pressed jump");
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            playerObj.transform.eulerAngles = new Vector2(playerObj.transform.eulerAngles.x, -90f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            playerObj.transform.eulerAngles = new Vector2(playerObj.transform.eulerAngles.x, 90f);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontal * speed * 2, rb.velocity.y);
        }
        Flip();
    }


    /*private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        /*if (isMovingVertical == false)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                isMovingHorizontal = true;
            }
            else isMovingHorizontal = false;
        }
        if (isMovingHorizontal == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
                isMovingVertical = true;
            }
            else isMovingVertical = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontal * speed * 2, rb.velocity.y);
        }
        
    }*/

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
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}
