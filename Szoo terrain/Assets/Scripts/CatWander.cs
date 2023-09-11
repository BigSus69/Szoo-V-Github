using UnityEngine;
using UnityEngine.UI;

public class CatWander : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;
    public float minWanderDistance = 5f;
    public float maxWanderDistance = 15f;
    public float minIdleTime = 1f;          // Minimum time the cat stays idle
    public float maxIdleTime = 5f;          // Maximum time the cat stays idle
    public bool m_isMoving;
    public float Pealth = 100f;
    public float MaxPealth = 100f;

    private bool isWandering = false;
    private bool isIdle = false;             // Flag to indicate if the cat is currently idle
    private Vector3 targetPosition;
    private float idleTimer = 0f;            // Timer for idle time
    private float idleDuration = 0f;         // Duration of the current idle period
    Animator m_Animator;
    public Slider petBar;
    /*public ArmControllerV2 armController;*/

    private void Start()
    {
        StartWander();
        m_Animator = GetComponent<Animator>();
        m_isMoving = false;
    }
    private void Awake()
    {
        Pealth = MaxPealth;
    }

    private void Update()
    {
        /*bool m_isSexing = armController.m_isSexing;*/
        // slowly reduce slider
        if (petBar.value > 0 /*&& m_isSexing == false*/)
        {
            petBar.value -= 0.0005f;
            //If the value is 0 then the slider is destroyed
            if (petBar.value == 0)
            {
                Destroy(gameObject);
            }
        }
        /*if m_sexing is true then slider increases until it has reached MaxPealth
        if (m_isSexing == true)
        {
            if (petBar.value < MaxPealth)
            {
                petBar.value += 0.0005f;
        
                // Make sure the value doesn't exceed MaxPealth
                if (petBar.value > MaxPealth)
                {
                    petBar.value = MaxPealth;
                }
            }
        }*/

        if (isWandering)
        {
            if (isIdle)
            {
                idleTimer += Time.deltaTime;

                // Check if idle duration has been reached
                if (idleTimer >= idleDuration)
                {
                    isIdle = false;
                    idleTimer = 0f;
                    GenerateTargetPosition();
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
                {
                    // Start idle period
                    isIdle = true;
                    idleDuration = Random.Range(minIdleTime, maxIdleTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                    var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }

        // Update animator parameter
        m_Animator.SetBool("isMoving", !isIdle);
    }

    /*public void LooseHealth(float amount)
    {
        // Update Health bar
        Pealth -= amount;
        petBar.value = Pealth / MaxPealth;

        if (Pealth <= 0)
        {
            
        }
    }*/

    private void StartWander()
    {
        GenerateTargetPosition();
        isWandering = true;
    }

    private void GenerateTargetPosition()
    {
        targetPosition = transform.position + Random.insideUnitSphere * Random.Range(minWanderDistance, maxWanderDistance);
        targetPosition.y = transform.position.y;
    }
}
