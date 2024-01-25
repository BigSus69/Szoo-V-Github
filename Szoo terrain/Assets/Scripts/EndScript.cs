using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        float score = PlayerPrefs.GetFloat("Score", 0); // Load the score
        scoreText.text = "Score: " + Mathf.RoundToInt(score);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
