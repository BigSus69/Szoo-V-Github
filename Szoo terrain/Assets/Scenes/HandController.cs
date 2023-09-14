using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    public static float HandGoGetMilkSpeed = 500f;
    public static float HandGotMilkSpeed = 2000f;

    public GameObject Wrist;

    public bool isSexing = false;
    public bool isSlapping = false;
    public Animator m_handanimator;


    void Start()
    {
        m_handanimator = GetComponent<Animator>();
    }
    void Update()
    {
        this.transform.position = Wrist.transform.position;

        if (Input.GetKeyDown(KeyCode.Space) && isSexing)
        {
            //play slap animation
            isSexing = false;
            isSlapping = true;
            Player.Instance.Arm.m_Animator.SetBool("isSlapping", true);
            m_handanimator.SetBool("handIsSlapping", true);
            Player.Instance.Arm.m_Animator.SetBool("isSexing", false);

            StartCoroutine(StopSlapping());

        }
    }

    private IEnumerator StopSlapping()
    {
        yield return new WaitForSeconds(2f);
        Player.Instance.Arm.m_Animator.SetBool("isSlapping", false);
        isSlapping = false;
        m_handanimator.SetBool("handIsSlapping", false);
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "cat")
        {
            isSexing = true;
            Debug.Log("Sexing");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "cat")
        {
            isSexing = false;
            Debug.Log("NoSexing");
        }
    }


}
