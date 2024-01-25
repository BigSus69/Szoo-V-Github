using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelOpacityChanger : MonoBehaviour
{
    public GameObject panel; // Reference to the Panel GameObject
    private Image panelImage; // Image component attached to the Panel GameObject
    private Score scoreInstance; // Reference to the Score instance
    private float previousTimeScore; // Variable to store the previous timeScore value

    void Start()
    {
        // Get the Image component attached to the Panel GameObject
        panelImage = panel.GetComponent<Image>();

        // Get the Score instance
        scoreInstance = GameObject.FindObjectOfType<Score>();

        // Initialize previousTimeScore with the current timeScore value
        if (scoreInstance != null)
        {
            previousTimeScore = scoreInstance.timeScore;
        }
    }

    void Update()
    {
        // Check if the Score instance is valid
        if (scoreInstance != null)
        {
            // Check if timeScore has decreased
            if (scoreInstance.timeScore < previousTimeScore)
            {
                // Start the coroutine to change the opacity
                StartCoroutine(ChangeOpacityTemporarily(1f, 0.5f));
            }

            // Update previousTimeScore with the current timeScore value
            previousTimeScore = scoreInstance.timeScore;
        }
    }

    IEnumerator ChangeOpacityTemporarily(float opacity, float duration)
    {
        // Change the opacity
        ChangeOpacity(.5f);

        // Wait for the specified duration
        yield return new WaitForSeconds(.35f);

        // Change the opacity back to 0
        ChangeOpacity(0f);
    }

    void ChangeOpacity(float opacity)
    {
        if (panelImage != null)
        {
            Color color = panelImage.color;
            color.a = opacity;
            panelImage.color = color;
        }
    }
}