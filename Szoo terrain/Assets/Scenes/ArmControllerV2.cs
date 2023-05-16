using UnityEngine;
using System.Collections;

public class ArmControllerV2 : MonoBehaviour
{
    private bool isScaling = false;  // A flag to check if the object is currently being scaled
    private float originalZScale;   // The original z scale of the object

    public HandController handController; // Reference to the HandController script

    private bool isArmStopped = false; // Flag to track if the arm is stopped

    Animator m_Animator;

    public bool m_isSexing;

    void Start()
    {
        originalZScale = transform.localScale.z;
        Debug.Log(HandController.HandGoGetMilkSpeed);

        // Get the reference to the HandController script
        handController = FindObjectOfType<HandController>();

        m_Animator = GetComponent<Animator>();
        m_isSexing = false;
    }

    void Update()
    {
        // Check if the object is being scaled and scale it accordingly
        if (isScaling)
        {
            Vector3 newScale = transform.localScale;
            newScale.z += HandController.HandGoGetMilkSpeed * Time.deltaTime;
            transform.localScale = newScale;
            m_Animator.SetBool("isSexing", false);
        }
        else if (handController.isSexing == true)
        {
            transform.localScale = transform.localScale;
            m_Animator.SetBool("isSexing", true);
        }
        else
        {
            // If the object is not being scaled, scale it back to its original size
            Vector3 newScale = transform.localScale;
            newScale.z -= HandController.HandGotMilkSpeed * Time.deltaTime;
            newScale.z = Mathf.Max(newScale.z, originalZScale);
            transform.localScale = newScale;
            m_Animator.SetBool("isSexing", false);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            isScaling = true;
            Debug.Log("Pressed Mouse");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isScaling = false;
            Debug.Log("Mouse not pressed");
        }

        // Access the value of the isSexing bool from the HandController script
        if (handController.isSexing == true)
        {
            if (!isArmStopped)
            {
                StartCoroutine(DelayedArmStop());
            }
        }
        else
        {
            // Do something when isSexing is false
        }
    }

    IEnumerator DelayedArmStop()
    {
        isArmStopped = true;

        yield return new WaitForSeconds(1f); // Delay for 1 seconds

        // Perform the "Arm stop" action
        Debug.Log("Arm stop");

        isArmStopped = false;
    }
}
