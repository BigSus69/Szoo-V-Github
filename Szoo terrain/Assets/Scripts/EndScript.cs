using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        float score = PlayerPrefs.GetFloat("Score", 0); 
        float highScore = PlayerPrefs.GetFloat("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score); 
            highScore = score;
        }

        scoreText.text = "Score: " + Mathf.RoundToInt(score);
        highScoreText.text = "High Score: " + Mathf.RoundToInt(highScore);
    }
}