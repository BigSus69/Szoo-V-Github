using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class CatWander : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;
    public float minWanderDistance = 5f;
    public float maxWanderDistance = 15f;
    public float minIdleTime = 1f;
    public float maxIdleTime = 5f;
    public bool m_isMoving;
    public float Pealth = 100f;
    public float MaxPealth = 100f;

    private bool isWandering = false;
    private bool isIdle = false;
    private Vector3 targetPosition;
    private float idleTimer = 0f;
    private float idleDuration = 0f;
    public static int explodeCatScore = 0;
    Animator m_Animator;
    public Slider petBar;
    public static event Action OnCatPetBarEmpty;
    public bool isPetBarEmptyTriggered = false;
    public GameObject evilCat;

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
        GameObject closestCat = Player.Instance.Hand.GetClosestCat();

        // slowly reduce slider
        if (petBar.value > 0 && (!Player.Instance.Hand.isTouching))
        {
            petBar.value -= 0.055f * Time.deltaTime;
        }

        //if m_touching is true then slider increases until it has reached MaxPealth
        if (Player.Instance.Hand.isTouching && closestCat == this.gameObject)
        {
            if (petBar.value < MaxPealth)
            {
                petBar.value += 0.3f * Time.deltaTime;

                // Make sure the value doesn't exceed MaxPealth
                if (petBar.value > MaxPealth)
                {
                    petBar.value = MaxPealth;
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

        if (petBar.value == 0 && !isPetBarEmptyTriggered)
        {
            OnCatPetBarEmpty?.Invoke();
            isPetBarEmptyTriggered = true;
        }

        if (petBar.value == 0)
        {
            //Scale the cat dependent on time
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
            Instantiate  (evilCat, transform.position, transform.rotation);
            CatWander.explodeCatScore += 1;
            Destroy(gameObject);
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
