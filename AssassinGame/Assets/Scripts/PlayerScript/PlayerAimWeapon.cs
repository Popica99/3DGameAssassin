using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System.Security.Cryptography;

public class PlayerAimWeapon : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    public Vector3 lookPos;
    public Transform shoulderTrans;
    public Transform rightShoulder;
    GameObject rsp;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rsp = new GameObject();
        rsp.name = transform.root.name + " Right Shoulder IK Helper";
        rsp.transform.SetParent(player.transform);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("ChangeWeaponBool", true);
        }
        else animator.SetBool("ChangeWeaponBool", false);
    }
    private void FixedUpdate()
    {
        
        HandleRotation();
        HandleAimingPos();
        //HandleShoulder();
    }    
    void HandleRotation()
    {
        Vector3 directonToLook = lookPos - transform.position;
        directonToLook.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directonToLook);

        //Debug.Log(lookPos.x + " " + transform.position.x);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);
    }
    void HandleAimingPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("mouse pos: " + Input.mousePosition);
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
}
