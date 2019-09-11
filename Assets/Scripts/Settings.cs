using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Animator settingsAnimator;
    public GameObject settingsPanel;
    public GameManager gameManager;
    public Text boardSizeNumberText, confettiOnOffText;

    private const string SlideInTrigger = "SlideIn";
    private const string SlideOutTrigger = "SlideOut";
    private const string IdleTrigger = "Idle";

    public void Open()
    {
        settingsPanel.SetActive(true);
        settingsAnimator.SetTrigger(SlideInTrigger);
        gameManager.isInputAllowed = false;
    }

    public void Close()
    {
        settingsAnimator.SetTrigger(SlideOutTrigger);
        gameManager.isInputAllowed = true;
    }

    private void Update()
    {
        if (settingsAnimator.GetCurrentAnimatorStateInfo(0).IsName(IdleTrigger))
            settingsPanel.SetActive(false);
    }

    public void SetBoardSize(float numRowsAndCols)
    {
        gameManager.ResizeBoard((int) numRowsAndCols);
        boardSizeNumberText.text = numRowsAndCols.ToString();
    }

    public void PlayAgain()
    {
        Close();
        gameManager.NewGame();
    }

    public void SetConfettiOnOff(float value)
    {
        switch (value)
        {
            case 0:
                gameManager.TurnOffConfetti();
                confettiOnOffText.text = "OFF";
                break;
            case 1:
                gameManager.TurnOnConfetti();
                confettiOnOffText.text = "ON";
                break;
            default:
                Debug.LogError("Expected 0 or 1 to turn on/off confetti; actual: " + value.ToString());
                break;
        }
    }
}
