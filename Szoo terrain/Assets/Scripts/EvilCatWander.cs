using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Random = UnityEngine.Random;

public class EvilCatWander : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;
    public float minWanderDistance = 5f;
    public float maxWanderDistance = 15f;
    public float minIdleTime = 1f;          // Minimum time the cat stays idle
    public float maxIdleTime = 5f;          // Maximum time the cat stays idle
    public bool m_isMoving;

    private bool isWandering = false;
    private bool isIdle = false;             // Flag to indicate if the cat is currently idle
    private Vector3 targetPosition;
    private float idleTimer = 0f;            // Timer for idle time
    private float idleDuration = 0f;         // Duration of the current idle period
    public static int explodeCatScore = 0;
    Animator m_Animator;
    public static event Action OnCatPetBarEmpty;
    public GameObject Explosion;

    private void Start()
    {
        StartWander();
        m_Animator = GetComponent<Animator>();
        m_isMoving = false;
    }
        private void Update()
    {
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;

        GameObject closestCat = Player.Instance.Hand.GetClosestCat();

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

        if (transform.localScale.x >= 10)
        {
            //Destroy the cat and instantiate explosion
            Debug.Log("Explode Cat Score: " + CatWander.explodeCatScore);
            Instantiate  (Explosion, transform.position, transform.rotation);
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
