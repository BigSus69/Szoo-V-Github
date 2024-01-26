using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;
        float score = PlayerPrefs.GetFloat("Score", 0); // Load the score
        float highScore = PlayerPrefs.GetFloat("HighScore", 0); // Load the high score

        // If the current score is higher than the high score
        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score); // Set new high score
            highScore = score;
        }

        scoreText.text = "Score: " + Mathf.RoundToInt(score);
        highScoreText.text = "High Score: " + Mathf.RoundToInt(highScore);
    }
}