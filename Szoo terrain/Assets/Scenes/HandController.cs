using UnityEngine;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour
{
    public static float HandGoGetMilkSpeed = 250f;
    public static float HandGotMilkSpeed = 1000f; 

    private bool isMoving = false;
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
