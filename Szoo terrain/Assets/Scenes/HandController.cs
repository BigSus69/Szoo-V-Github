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
    bool isCatEvil = false;
    public Animator m_handanimator;
    public ArmBar armBar;

    public GameObject cat;
    public int catSpawn = 8;
    public GameObject raycastObject;
    RaycastHit objectHit;
    public int slapCount = 0;

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
/*
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
                // Debug.Log("raycastObject: " + raycastObject.name);
                DistanceToCat = Vector3.Distance(transform.position, hit.point); // Update the DistanceToCat property
            }
            else
            {
                raycastObject = null;
            }
        }
    }
*/

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
        GameObject closestEvilCat = GetClosestEvilCat();

        float distanceToClosestCat = closestCat != null ? Vector3.Distance(this.transform.position, closestCat.transform.position) : float.MaxValue;
        float distanceToClosestEvilCat = closestEvilCat != null ? Vector3.Distance(this.transform.position, closestEvilCat.transform.position) : float.MaxValue;

        if (distanceToClosestCat < distanceToClosestEvilCat)
        {
            // If a cat was found and it's closer than the evil cat, make it fly at a high speed away from the player
            Rigidbody catRigidbody = closestCat.GetComponent<Rigidbody>();
            catRigidbody.AddForce(Vector3.up * 1000f, ForceMode.Impulse);

            // Cat explodes after a few seconds
            StartCoroutine(ExplodeCat(closestCat));

            isCatEvil = false;
        }
        else if (closestEvilCat != null)
        {
            // If an evil cat was found and it's closer than the cat, make it fly at a high speed away from the player
            Rigidbody evilCatRigidbody = closestEvilCat.GetComponent<Rigidbody>();
            evilCatRigidbody.AddForce(Vector3.up * 1000f, ForceMode.Impulse);

            // Evil cat explodes after a few seconds
            StartCoroutine(ExplodeCat(closestEvilCat));

            isCatEvil = true;
        }

        Score scoreInstance = GameObject.FindObjectOfType<Score>();
        if (scoreInstance != null)
        {
            if (isCatEvil)
            {
                scoreInstance.timeScore += 1000;
                Debug.Log("You're good");
            }
            else
            {
                scoreInstance.timeScore -= 1000;
                Debug.Log("You're evil");
            }
        }

        slapCount++;

        // Wait for the slap animation to finish playing
        yield return new WaitForSeconds(animationLength + 0.7f);

        StartCoroutine(StopSlappingCoroutine());

        isTouching = false;
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
    public GameObject GetClosestEvilCat()
    {
        GameObject closestEvilCat = null;
        float closestDistance = float.MaxValue;
        GameObject[] evilCats = GameObject.FindGameObjectsWithTag("evilCat");
        foreach (GameObject evilCat in evilCats)
        {
            if (evilCat != null)
            {
                float distance = Vector3.Distance(this.transform.position, evilCat.transform.position);
                if (distance < closestDistance)
                {
                    closestEvilCat = evilCat;
                    closestDistance = distance;
                }
            }
        }
        return closestEvilCat;
    }

    private IEnumerator ExplodeCat(GameObject cat)
    {
        yield return new WaitForSeconds(0.5f);
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
            isCatEvil = false;
        }
        if (col.gameObject.tag == "evilCat")
        {
            isTouching = true;
            isCatEvil = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "cat" || col.gameObject.tag == "evilCat")
        {
            isTouching = false;
        }
    }

}