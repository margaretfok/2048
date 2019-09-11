using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TilesManager tilesManager;
    public GameEndAnimationHandler gameEndAnimationHandler;
    public ConfettiAnimationHandler confettiAnimationHandler;
    public ScoreManager scoreManager;
    public Settings settings;

    public const int BaseNumber = 2;
    public bool isInputAllowed = true;

    private void Start()
    {
        NewGame();
    }
    
    public void NewGame()
    {
        AllowInput();
        
        scoreManager.ResetScore();
        tilesManager.RemoveAllTiles();
        tilesManager.ResetCreatedTiles();

        tilesManager.SpawnInitialTiles();
    }
    
    public void AllowInput()
    {
        isInputAllowed = true;
    }
    
    public void DisableInput()
    {
        isInputAllowed = false;
    }

    public void Move(InputDirection id)
    {
        switch (id)
        {
            case InputDirection.Down:
                tilesManager.MoveDown();
                break;
            case InputDirection.Left:
                tilesManager.MoveLeft();
                break;
            case InputDirection.Right:
                tilesManager.MoveRight();
                break;
            case InputDirection.Up:
                tilesManager.MoveUp();
                break;
            default:
                Debug.LogError("Invalid input direction: " + id.ToString());
                break;
        }
        
        tilesManager.ResetThisTurnFlags();

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (IsGameOver())
        {
            gameEndAnimationHandler.GameOverDropDown();
            DisableInput();
        }
    }

    private bool IsGameOver()
    {
        return isInputAllowed && !tilesManager.MoveExists();
    }

    public void GameWon()
    {
        DisableInput();
        gameEndAnimationHandler.WinnerDropDown();
        confettiAnimationHandler.PlayConfetti();
    }

    public void AddPoints(int points)
    {
        scoreManager.AddPoints(points);
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }

    public void ResizeBoard(int numRowsAndCols)
    {
        tilesManager.ResizeBoard(numRowsAndCols);
        NewGame();
    }

    public void TurnOffConfetti()
    {
        confettiAnimationHandler.DisableConfetti();
    }

    public void TurnOnConfetti()
    {
        confettiAnimationHandler.EnableConfetti();
    }

    public void SpawnTwo1024Tiles()
    {
        tilesManager.CreateTileAt(0, 0, TilesManager.WinningTile / 2);
        tilesManager.CreateTileAt(0, 1, TilesManager.WinningTile / 2);
    }

    public void SpawnRandomTiles()
    {
        tilesManager.SpawnRandomTiles();
    }
}
