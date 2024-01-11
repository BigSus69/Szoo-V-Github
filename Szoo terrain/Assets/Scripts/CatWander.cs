using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

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
    public static int explodeCatScore = 0;
    Animator m_Animator;
    public Slider petBar;

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
        // slowly reduce slider
        if (petBar.value > 0 && !Player.Instance.Hand.isTouching)
        {
            petBar.value -= 0.0007f;

        }
        //if m_touching is true then slider increases until it has reached MaxPealth
        if (Player.Instance.Hand.isTouching)
        {
            // Get the closest cat
            GameObject closestCat = Player.Instance.Hand.GetClosestCat();

            // Check if the current cat is the closest
            if (closestCat == this.gameObject)
            {
                if (petBar.value < MaxPealth)
                {
                    petBar.value += 0.003f;

                    // Make sure the value doesn't exceed MaxPealth
                    if (petBar.value > MaxPealth)
                    {
                        petBar.value = MaxPealth;
                    }
                }
            }
        }

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

        if (petBar.value == 0)
        {
            //Scale the cat dependent on time
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        }

        if (transform.localScale.x >= 3)
        {
            //Destroy the cat
            Destroy(gameObject);
            CatWander.explodeCatScore += 1;
            Debug.Log("Explode Cat Score: " + CatWander.explodeCatScore);
        }

        // Update animator parameter
        m_Animator.SetBool("isMoving", !isIdle);
    }

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
