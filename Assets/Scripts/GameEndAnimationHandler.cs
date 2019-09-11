using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndAnimationHandler : MonoBehaviour
{
    public Animator gameOverAnimator, winnerAnimator;
    public GameManager gameManager;

    private const string DropDownTrigger = "DropDown";
    private const string GoUpTrigger = "GoUp";
    
    public void GameOverDropDown()
    {
        gameOverAnimator.SetTrigger(DropDownTrigger);
    }

    public void GameOverGoUp()
    {
        gameOverAnimator.SetTrigger(GoUpTrigger);
    }

    public void GameOverPlayAgain()
    {
        GameOverGoUp();
        gameManager.NewGame();
    }

    public void WinnerDropDown()
    {
        winnerAnimator.SetTrigger(DropDownTrigger);
    }

    public void WinnerGoUp()
    {
        winnerAnimator.SetTrigger(GoUpTrigger);
    }

    public void ContinuePlaying()
    {
        WinnerGoUp();
        gameManager.AllowInput();
    }

    public void WinnerPlayAgain()
    {
        WinnerGoUp();
        gameManager.NewGame();
    }
}
