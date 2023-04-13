using UnityEngine;
using System.Collections;

public class ArmControllerV2 : MonoBehaviour
{
    public float scaleSpeed = 1.0f;  // The speed at which the object will scale
    public float scaleDownSpeed = 5.0f; // The speed at which the object will scale down

    private bool isScaling = false;  // A flag to check if the object is currently being scaled
    private float originalZScale;   // The original z scale of the object

    void Start()
    {
        originalZScale = transform.localScale.z;
    }

    void Update()
    {
        // Check if the object is being scaled and scale it accordingly
        if (isScaling)
        {
            Vector3 newScale = transform.localScale;
            newScale.z += scaleSpeed * Time.deltaTime;
            transform.localScale = newScale;
        }
        else
        {
            // If the object is not being scaled, scale it back to its original size
            Vector3 newScale = transform.localScale;
            newScale.z -= scaleDownSpeed * Time.deltaTime;
            newScale.z = Mathf.Max(newScale.z, originalZScale);
            transform.localScale = newScale;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isScaling = true;
            Debug.Log("Pressed Mouse");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isScaling = false;
            Debug.Log("Mouse no pressed");
        }
    }

}