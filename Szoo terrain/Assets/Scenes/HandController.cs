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

    public GameObject cat;


    void Start()
    {
        m_handanimator = GetComponent<Animator>();
         for (int i = 0; i < 5; i++)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
        Instantiate(cat, position, Quaternion.identity);
    }
    }
    
    private IEnumerator SlapCat()
{
    //play slap animation
    isSexing = false;
    isSlapping = true;
    Player.Instance.Arm.m_Animator.SetBool("isSlapping", true);
    m_handanimator.SetBool("handIsSlapping", true);
    Player.Instance.Arm.m_Animator.SetBool("isSexing", false);

    // Wait for the slap animation to finish playing
    AnimatorStateInfo stateInfo = Player.Instance.Arm.m_Animator.GetCurrentAnimatorStateInfo(0);
    float animationLength = stateInfo.length;
    yield return new WaitForSeconds(animationLength + 0.5f);

    //cat fly at a high speed away from the player
    Rigidbody catRigidbody = cat.GetComponent<Rigidbody>();
    catRigidbody.AddForce(Vector3.up * 1000f, ForceMode.Impulse);

    // Cat explodes after a few seconds
    StartCoroutine(ExplodeCat());

    StartCoroutine(StopSlappingCoroutine());

    isSexing = false;
    isSlapping = false;
}

private IEnumerator StopSlappingCoroutine()
{
    yield return new WaitForSeconds(2f);

    //stop slap animation
    isSlapping = false;
    Player.Instance.Arm.m_Animator.SetBool("isSlapping", false);
    m_handanimator.SetBool("handIsSlapping", false);
    isSexing = false;
}

void Update()
{
    this.transform.position = Wrist.transform.position;
    if (Input.GetKeyDown(KeyCode.Space) && isSexing)
    {
        StartCoroutine(SlapCat());
    }
}

    private IEnumerator ExplodeCat()
    {
        yield return new WaitForSeconds(2f);
        GameObject[] cats = GameObject.FindGameObjectsWithTag("cat");
        foreach (GameObject cat in cats)
        {
            Destroy(cat);
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
