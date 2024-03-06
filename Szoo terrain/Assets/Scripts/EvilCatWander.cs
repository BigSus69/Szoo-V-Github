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
    public float minIdleTime = 1f;
    public float maxIdleTime = 5f;
    public bool m_isMoving = false;

    private bool isWandering = false;
    private bool isIdle = false;
    private Vector3 targetPosition;
    private float idleTimer = 0f;
    private float idleDuration = 0f;
    public static int explodeCatScore = 0;
    Animator m_Animator;
    public GameObject Explosion;

    private void Start()
    {
        StartWander();
        m_Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;
        
        if (transform.localScale.x >= 6)
        {
            //Destroy the cat and instantiate explosion
            Debug.Log("Explode Cat Score: " + CatWander.explodeCatScore);
            Instantiate  (Explosion, transform.position, transform.rotation);
            CatWander.explodeCatScore += 1;
            Destroy(gameObject);
        }

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
