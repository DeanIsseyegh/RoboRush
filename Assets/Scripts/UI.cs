using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverScoreText;

    private float score = 0;
    private int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: ";
    }

    public void UpdateScore(float amount)
    {
        score += amount;
        scoreText.text = "Score: " + (long) score;
    }

    public void UpdateHealth(int amount)
    {
        var healthIcon = GameObject.Find("Heart" + health);
        if (amount < 0 && healthIcon) healthIcon.SetActive(false);
        health += amount;
    }

    public void UpdateGameOverScore()
    {
        gameOverScoreText.text = "Score: " + (long)score;
    }


    public int GetHealth()
    {
        return health;
    }

}
