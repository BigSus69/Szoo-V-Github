using UnityEngine;
using System.Collections;

public class ArmControllerV2 : MonoBehaviour
{
    private bool isScaling = false;  // A flag to check if the object is currently being scaled
    private float originalZScale;   // The original z scale of the object

    void Start()
    {
        originalZScale = transform.localScale.z;
        Debug.Log(HandController.HandGoGetMilkSpeed);
    }

    void Update()
    {
        
        // Check if the object is being scaled and scale it accordingly
        if (isScaling)
        {
            Vector3 newScale = transform.localScale;
            newScale.z += HandController.HandGoGetMilkSpeed * Time.deltaTime;
            transform.localScale = newScale;
        }
        else
        {
            // If the object is not being scaled, scale it back to its original size
            Vector3 newScale = transform.localScale;
            newScale.z -= HandController.HandGotMilkSpeed * Time.deltaTime;
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