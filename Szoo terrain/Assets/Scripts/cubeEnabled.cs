using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cubeEnabled : MonoBehaviour
{
    // If pressed 1 on the keyboard, the cube will be enabled
    public GameObject cube;
    public bool isCubeEnabled = false;
    public HandController handController;


    void Start()
    {
        // Get the reference to the HandController script
        handController = FindObjectOfType<HandController>();
    }

    void Update()
    {
        // Check for input or condition to change opacity
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Call the method to change the image opacity
            isCubeEnabled = true;
            ChangeCubeEnabled(isCubeEnabled);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Call the method to change the image opacity
            isCubeEnabled = false;
            ChangeCubeEnabled(isCubeEnabled);
        }
    }

    void ChangeCubeEnabled(bool isEnabled)
    {
        if (cube != null)
        {
            cube.SetActive(isEnabled);
        }
    }
}