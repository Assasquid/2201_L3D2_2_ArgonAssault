using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = "Score: " + score.ToString("0,000,000");
    }
}
