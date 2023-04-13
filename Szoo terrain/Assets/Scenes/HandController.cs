using UnityEngine;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour
{
    public float HandGoGetMilkSpeed = 1.0f;
    public float HandGotMilkSpeed = 5.0f; 

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed Mouse 2");

        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse no pressed 2");
        }
    }
}
