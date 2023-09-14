using UnityEngine;
using System.Collections;

public class ArmControllerV2 : MonoBehaviour
{
    private bool isScaling = false;  // A flag to check if the object is currently being scaled
    private float originalZScale;   // The original z scale of the object'


    public Animator m_Animator;

    public bool m_isSexing;

    void Start()
    {
        originalZScale = transform.localScale.z;
        Debug.Log(HandController.HandGoGetMilkSpeed);


        m_Animator = GetComponent<Animator>();
        m_isSexing = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isScaling = true;
            Debug.Log("Pressed Mouse");
        }

        if (Input.GetMouseButtonUp(0))
        {
            isScaling = false;
            m_Animator.SetBool("isSexing", false);
            m_Animator.SetBool("isSlapping", false);
            Debug.Log("Mouse not pressed");
        }

        // Check if the object is being scaled and scale it accordingly
        if (isScaling)
        {
            Vector3 newScale = transform.localScale;
            newScale.z += HandController.HandGoGetMilkSpeed * Time.deltaTime;
            transform.localScale = newScale;
            m_Animator.SetBool("isSexing", false);
            m_Animator.SetBool("isSlapping", false);
        }
        else if (Player.Instance.Hand.isSexing == true)
        {
            transform.localScale = transform.localScale;
            m_Animator.SetBool("isSlapping", false);
            m_Animator.SetBool("isSexing", true);
        }
        else if (Player.Instance.Hand.isSlapping)
        {
            transform.localScale = transform.localScale;
        }
        else
        {
            // If the object is not being scaled, scale it back to its original size
            Vector3 newScale = transform.localScale;
            newScale.z -= HandController.HandGotMilkSpeed * Time.deltaTime;
            newScale.z = Mathf.Max(newScale.z, originalZScale);
            transform.localScale = newScale;
            m_Animator.SetBool("isSexing", false);
            m_Animator.SetBool("isSlapping", false);
        }


    }
}
