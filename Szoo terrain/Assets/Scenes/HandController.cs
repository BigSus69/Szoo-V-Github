using System;
using System.Collections;
using JetBrains.Annotations;
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
    public int explodeCatScore = 0;
    public int catSpawn = 8;

    void Start()
    {
        m_handanimator = GetComponent<Animator>();
         for (int i = 0; i < catSpawn; i++)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
        Instantiate(cat, position, Quaternion.identity);
    }
    }

    void Update()
    {
        this.transform.position = Wrist.transform.position;
        if (Input.GetKeyDown(KeyCode.Space) && isSexing)
        {
            StartCoroutine(SlapCat());
        }

        if(explodeCatScore == catSpawn)
        {
            Debug.Log("Oh no! " + explodeCatScore + " This many cacats exploded!");
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

    GameObject closestCat = GetClosestCat();
    // If a cat was found, make it fly at a high speed away from the player
    if (closestCat != null)
    {
        Rigidbody catRigidbody = closestCat.GetComponent<Rigidbody>();
        catRigidbody.AddForce(Vector3.up * 1000f, ForceMode.Impulse);

        // Cat explodes after a few seconds
        StartCoroutine(ExplodeCat(closestCat));
    }

    StartCoroutine(StopSlappingCoroutine());

    isSexing = false;
    isSlapping = false;
}
 public GameObject GetClosestCat()
    {
        GameObject closestCat = null;
        float closestDistance = float.MaxValue;
        GameObject[] cats = GameObject.FindGameObjectsWithTag("cat");
        foreach (GameObject cat in cats)
        {
            float distance = Vector3.Distance(this.transform.position, cat.transform.position);
            if (distance < closestDistance)
            {
                closestCat = cat;
                closestDistance = distance;
            }
        }
        return closestCat;
    }

   private IEnumerator ExplodeCat(GameObject cat)
{
    explodeCatScore += 1;
    Debug.Log("ExplodeCat started. Score: " + explodeCatScore);
    yield return new WaitForSeconds(2f);
    Destroy(cat);
    Debug.Log("Cat destroyed. Score: " + explodeCatScore);
}

public void SomeMethod()
{
    Debug.Log("SomeMethod called");
    GameObject cat = GetClosestCat();
    if (cat != null)
    {
        StartCoroutine(ExplodeCat(cat));
    }
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
