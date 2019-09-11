using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText, highScoreText;
    
    private int score = 0;
    private int highScore, newHighScore, newScore;
    private const string HighScoreKey = "HighScore";
    private float duration = 0.5f;
    
    private void Awake()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScoreText.text = highScore.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        newScore = 0;
        scoreText.text = score.ToString();
    }
    
    public void AddPoints(int newPointsToAdd)
    {
        score = newScore;
        newScore = score + newPointsToAdd;
        
        AnimateScoreIncrease();
        CheckIfScoreIsNewHighScore();
    }

    private void AnimateScoreIncrease()
    {
        StopCoroutine(nameof(ScoreIncreaseCoroutine));
        
        StartCoroutine(nameof(ScoreIncreaseCoroutine));
    }
     
    IEnumerator ScoreIncreaseCoroutine () {
        int start = score;
        
        for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
            float progress = timer / duration;
            
            score = (int) Mathf.Lerp (start, newScore, progress);
            scoreText.text = score.ToString();
            
            yield return null;
        }

        score = newScore;
        scoreText.text = score.ToString();
    }

    private void CheckIfScoreIsNewHighScore()
    {
        if (newScore > highScore)
            SetNewHighScore();
    }

    private void SetNewHighScore()
    {
        PlayerPrefs.SetInt(HighScoreKey, newScore);
        PlayerPrefs.Save();
        
        AnimateHighScoreIncrease();
    }

    private void AnimateHighScoreIncrease()
    {
        StopCoroutine(nameof(HighScoreIncreaseCoroutine));

        newHighScore = newScore;
        StartCoroutine(nameof(HighScoreIncreaseCoroutine));
    }
    
    IEnumerator HighScoreIncreaseCoroutine () {
        int start = highScore;
        
        for (float timer = 0; timer <= duration; timer += Time.deltaTime) {
            float progress = timer / duration;
            
            highScore = (int) Mathf.Lerp (start, newHighScore, progress);
            highScoreText.text = highScore.ToString();
            
            yield return null;
        }

        highScore = newHighScore;
        highScoreText.text = highScore.ToString();
    }
}
