using UnityEngine;
using UnityEngine.UI;

public class OpacityChanger : MonoBehaviour
{
    public Image image; // Reference to the Image component
    public float targetOpacity = 1f; // The target opacity value (0 to 1)
    public HandController handController;
    void Start()
    {
        // Get the Image component attached to the object
        image = GetComponent<Image>();

        // Get the reference to the HandController script
        handController = FindObjectOfType<HandController>();
    }

    void Update()
    {
        // Check for input or condition to change opacity
        if (handController.isTouching == true)
        {
            // Call the method to change the image opacity
            ChangeOpacity(targetOpacity);
        }
        else
        {
            // Call the method to change the image opacity
            ChangeOpacity(0f);
        }
    }

    void ChangeOpacity(float opacity)
    {
        // Ensure the Image component is valid
        if (image != null)
        {
            // Get the current color from the image
            Color color = image.color;

            // Update the alpha value
            color.a = opacity;

            // Assign the updated color to the image
            image.color = color;
        }
    }
}
