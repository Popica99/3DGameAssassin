using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    Camera cam;
    Collider planecollider;
    RaycastHit hit;
    Ray ray;

    public void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planecollider = GameObject.Find("Background").GetComponent<Collider>();
    }
    void Update()
    {
        //transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        /*if (Input.GetMouseButton(0))
        {*/
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == planecollider)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 100);
                }
            }

        //}
        /*Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, cursorPos.z);
        Debug.Log("x: " + cursorPos.x + " y: " + cursorPos.y + " z: " + cursorPos.z);*/
    }
}
