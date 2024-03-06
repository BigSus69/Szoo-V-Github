using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public float timeScore = 0f;

    void Start()
    {
        CatWander.OnCatPetBarEmpty += HandleCatPetBarEmpty;
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("cat").Length > 1)
        {
            timeScore += Time.deltaTime * 300f;
        }
        scoreText.text = "Score: " + Mathf.RoundToInt(timeScore);
        onlyOneCat();
    }

    private void HandleCatPetBarEmpty()
    {
        timeScore -= 1544;
        PlayerPrefs.SetFloat("Score", timeScore);
        onlyOneCat();
    }    

    public void onlyOneCat()
    {
        if (GameObject.FindGameObjectsWithTag("cat").Length <= 1)
        {
            PlayerPrefs.SetFloat("Score", timeScore); 
            SceneManager.LoadScene("EndScreen");
        }
    }
}