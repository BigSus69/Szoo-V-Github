using UnityEngine;

public class HandController : MonoBehaviour
{
    public static float HandGoGetMilkSpeed = 500f;
    public static float HandGotMilkSpeed = 2000f;

    public GameObject Wrist;

    public bool isSexing = false;

    void Update()
    {
        this.transform.position = Wrist.transform.position;
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
