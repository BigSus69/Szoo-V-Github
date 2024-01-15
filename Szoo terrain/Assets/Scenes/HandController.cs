using System;
using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public static float HandGoGetMilkSpeed = 500f;
    public static float HandGotMilkSpeed = 2000f;
    public float DistanceToCat { get; private set; }

    public GameObject Wrist;

    public bool isTouching = false;
    public bool isSlapping = false;
    public Animator m_handanimator;
    public ArmBar armBar;

    public GameObject cat;
    public int catSpawn = 8;
    public GameObject raycastObject;
    RaycastHit objectHit;

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
        if (Input.GetKeyDown(KeyCode.Space) && isTouching)
        {
            StartCoroutine(SlapCat());
        }


    }

  void FixedUpdate()
{
    Ray ray = new Ray(transform.position, transform.forward);
    Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
        if (hit.collider.gameObject.tag == "cat")
        {
            raycastObject = hit.collider.gameObject;
            Debug.Log("raycastObject: " + raycastObject.name);
            DistanceToCat = Vector3.Distance(transform.position, hit.point); // Update the DistanceToCat property
        }
        else
        {
            raycastObject = null;
        }
    }
}

    private IEnumerator SlapCat()
    {
        //play slap animation
        isTouching = false;
        isSlapping = true;
        Player.Instance.Arm.m_Animator.SetBool("isSlapping", true);
        m_handanimator.SetBool("handIsSlapping", true);
        Player.Instance.Arm.m_Animator.SetBool("isTouching", false);

        // Wait for the slap animation to finish playing
        AnimatorStateInfo stateInfo = Player.Instance.Arm.m_Animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        yield return new WaitForSeconds(animationLength + 0.7f);

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

        isTouching = false;
        isSlapping = false;
    }
    public GameObject GetClosestCat()
    {
        Debug.Log("GetClosestCat called");
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
        yield return new WaitForSeconds(2f);
        Destroy(cat);
    }

    private IEnumerator StopSlappingCoroutine()
    {
        yield return new WaitForSeconds(2f);

        //stop slap animation
        isSlapping = false;
        Player.Instance.Arm.m_Animator.SetBool("isSlapping", false);
        m_handanimator.SetBool("handIsSlapping", false);
        isTouching = false;
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
            isTouching = true;
            Debug.Log("Touching");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "cat")
        {
            isTouching = false;
            Debug.Log("NoTouching");
        }
    }

}