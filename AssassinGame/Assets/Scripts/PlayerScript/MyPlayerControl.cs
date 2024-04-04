using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerControl : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float acceleration = 64f;
    public float jumpSpeed = 8f;
    public float jumpDuration = 150f;

    private float horizontal;
    private float vertical;
    private float jumpInput;

    private bool onTheGround;
    private float jmpDuration;
    private bool jumpKeyDown = false;
    private bool canVariableJump = false;
    private float movement_Aim;

    Rigidbody rb_velocity;
    Animator runAnimatior;
    LayerMask layerMask;
    Transform modelTrans;

    public Transform shoulderTrans;
    public Transform rightShoulder;
    public Vector3 lookPos;
    GameObject rsp;

    private void Start()
    {
        rb_velocity = GetComponent<Rigidbody>();
        //SetupAnimator();

        layerMask = ~(1 << 8);

        rsp = new GameObject();
        rsp.name = transform.root.name + " Right Shoulder IK Helper";
    }

    private void FixedUpdate()
    {
        InputHandler();
        UpdateRigidbodyValues();
        MovementHandler();
        HandleRotation();
        HandleAimingPos();
        //HandleAnimations();
        HandleShoulder();
    }

    void InputHandler()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jumpInput = Input.GetAxis("Fire2");
    }

    void MovementHandler()
    {
        onTheGround = isOnGround();
        if(horizontal < -0.1f)
        {
            if (rb_velocity.velocity.x > -this.maxSpeed)
            {
                rb_velocity.AddForce(new Vector3(-this.acceleration, 0, 0));
            }
            else
            {
                rb_velocity.velocity = new Vector3(-this.maxSpeed, rb_velocity.velocity.y, 0);
            }
        }
        else if (horizontal > 0.1f)
        {
            if (rb_velocity.velocity.x < this.maxSpeed) 
            {
                rb_velocity.AddForce(new Vector3(this.acceleration, 0, 0));
            }
            else
            {
                rb_velocity.velocity = new Vector3(this.maxSpeed, rb_velocity.velocity.y, 0);
            }
        }

        if (jumpInput > 0.1f)
        {
            if (!jumpKeyDown)
            {
                jumpKeyDown = true;

                if (onTheGround)
                {
                    rb_velocity.velocity = new Vector3(rb_velocity.velocity.y, this.jumpSpeed, 0);
                    jumpDuration = 0.0f;
                }
            }
            else if (canVariableJump)
            {
                jumpDuration += Time.deltaTime;

                if (jumpDuration <this.jumpDuration / 1000)
                {
                    rb_velocity.velocity = new Vector3(rb_velocity.velocity.x, this.jumpSpeed, 0);
                }
            }
            else
            {
                jumpKeyDown = false;
            }
        }
    }

    void HandleRotation()
    {
        Vector3 directonToLook = lookPos - transform.position;
        directonToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directonToLook);

        Debug.Log(lookPos.x + " " + transform.position.x);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
    }

    void HandleAimingPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
        {
            Vector3 lookP = hit.point;
            lookP.z = transform.position.z;
            lookPos = lookP;
        }
    }

    void HandleShoulder()
    {
        shoulderTrans.LookAt(lookPos);

        Vector3 rightShoulderPos = rightShoulder.TransformPoint(Vector3.zero);
        rsp.transform.position = rightShoulderPos;
        rsp.transform.parent = transform;

        shoulderTrans.position = rsp.transform.position;
    }
    private bool isOnGround()
    {
        bool retVal = false;
        float lengthToSearch = 1.5f;
        Vector3 lineStart = transform.position + Vector3.up;
        Vector3 vectorToSearch = -Vector3.up;
        RaycastHit hit;
        if (Physics.Raycast(lineStart, vectorToSearch, out hit, lengthToSearch, layerMask))
        {
            retVal = true;
        }
        return retVal;
    }

    void UpdateRigidbodyValues()
    {
        if (onTheGround) 
        {
            rb_velocity.drag = 4;
        }
        else
        {
             rb_velocity.drag = 0;
        }
    }
}
