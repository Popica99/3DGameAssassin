using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    public Vector3 lookPos;
    private void FixedUpdate()
    {
        HandleRotation();
        HandleAimingPos();
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
}
