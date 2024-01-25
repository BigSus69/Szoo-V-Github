using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update

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
        timeScore -= 1500;
        PlayerPrefs.SetFloat("Score", timeScore); // Save the score
        onlyOneCat();
    }

    void OnDestroy()
    {
        CatWander.OnCatPetBarEmpty -= HandleCatPetBarEmpty;
    }

    public void onlyOneCat()
    {
        if (GameObject.FindGameObjectsWithTag("cat").Length == 1)
        {
            PlayerPrefs.SetFloat("Score", timeScore); // Save the score
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}