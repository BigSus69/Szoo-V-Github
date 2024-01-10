using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class rayCastingScript : MonoBehaviour
{
    public GameObject raycastObject;
    public ArmBar armBar;
    public float startDistance;

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.black);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "cat")
            {
                raycastObject = hit.collider.gameObject;
                Debug.Log("raycastObject: " + raycastObject.name);
                startDistance = Vector3.Distance(transform.position, hit.point);
                //Debug.Log("startDistance to cat: " + startDistance);
                armBar.UpdateArmBar(startDistance);
            }
            else
            {
                raycastObject = null;
            }
        }
    }

    public float GetStartDistance()
    {
        return startDistance;
    }
}
