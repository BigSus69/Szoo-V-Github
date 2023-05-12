using UnityEngine;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour
{
    public static float HandGoGetMilkSpeed = 500f;
    public static float HandGotMilkSpeed = 2000f; 

    private float originalZScale;

        
    public GameObject Wrist;


    void Start()
    {
        Debug.Log(HandGoGetMilkSpeed);
    }

    public void Update(){

        this.transform.position = Wrist.transform.position;
    }

    
}
