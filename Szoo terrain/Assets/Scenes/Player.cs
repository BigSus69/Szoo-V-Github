using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float rotationSpeed = 100f;
    public float maxVerticalAngle = 80f;
    public float minVerticalAngle = -80f;

    private float verticalRotation = 0f;

    void Start()
    {
        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move camera based on input
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Get input from mouse movement
        float horizontalMouse = Input.GetAxis("Mouse X");

        // Rotate camera based on input
        float rotationAmount = horizontalMouse * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount, Space.Self);


        // Apply vertical rotation to camera
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}