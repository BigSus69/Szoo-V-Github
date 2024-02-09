using UnityEngine;

public class cubeEnabled : MonoBehaviour
{
    public GameObject cube; // Reference to the Cube GameObject
    private bool isCubeEnabled = false; // Track whether the cube is enabled

    void Start()
    {
        if (cube != null)
        {
            cube.SetActive(false); // Disable the cube
            isCubeEnabled = false;
            Debug.Log("Cube is initially disabled");
        }
        else
        {
            Debug.Log("Cube is null in Start");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 key pressed");
            if (cube != null)
            {
                cube.SetActive(!isCubeEnabled); // Toggle the active state of the cube
                isCubeEnabled = !isCubeEnabled; // Update the isCubeEnabled variable

                if (isCubeEnabled)
                {
                    Debug.Log("Cube should now be enabled");
                }
                else
                {
                    Debug.Log("Cube should now be disabled");
                }
            }
            else
            {
                Debug.Log("Cube is null in Update");
            }
        }
    }
}