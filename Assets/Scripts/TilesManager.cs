using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TilesManager : MonoBehaviour
{
    public GameObject tilePrefab, tileBackgroundPrefab, tileBackgroundPanel;
    public GameManager gameManager;
    public AutoExpandGridLayoutGroup backgroundGridLayout, tileGridLayout;

    public const int WinningTile = 2048;
    private int totalRows = 4;
    private int totalCols = 4;
    private Tile[,] allTiles;
    private HashSet<int> createdTiles = new HashSet<int>();
    private bool hasNewTileSpawnedThisTurn = false;
    
    private void Awake()
    {
        CreateAllTiles();
    }

    private void CreateAllTiles()
    {
        allTiles = new Tile[totalCols, totalRows];
        
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                GameObject tileBackground = Instantiate(tileBackgroundPrefab, tileBackgroundPanel.transform);
                GameObject tile = Instantiate(tilePrefab, transform);
                allTiles[i, j] = tile.GetComponentInChildren<Tile>(); 
                allTiles[i, j].tileObject = tile;
                allTiles[i, j].tileBackgroundObject = tileBackground;
            }
        }
    }

    public void SpawnInitialTiles()
    {
        int row1 = GetRandomNum(totalRows);
        int col1 = GetRandomNum(totalCols);

        int row2 = GetRandomNum(totalRows);
        int col2 = GetRandomNum(totalCols);

        while (row1 == row2 && col1 == col2)
        {
            row1 = GetRandomNum(totalRows);
            col1 = GetRandomNum(totalCols);

            row2 = GetRandomNum(totalRows);
            col2 = GetRandomNum(totalCols);
        }

        int num1 = GetRandomNewTile();
        int num2 = GetRandomNewTile();

        CreateTileAt(row1, col1, num1);
        CreateTileAt(row2, col2, num2);
    }

    int GetRandomNum(int num)
    {
        return Random.Range(0, num);
    }

    int GetRandomNewTile()
    {
        return GetRandomNum(10) == 0 ? 4 : 2;
    }

    public void CreateTileAt(int row, int col, int value)
    {
        allTiles[row, col].Spawn(value);
    }

    public void ResetCreatedTiles()
    {
        createdTiles.Clear();
        
        createdTiles.Add(2);
        createdTiles.Add(4);
        createdTiles.Add(8);
    }
    
    public void ResizeBoard(int numRowsAndCols)
    {
        DestroyAllTiles();

        totalCols = numRowsAndCols;
        totalRows = numRowsAndCols;

        backgroundGridLayout.constraintCount = numRowsAndCols;
        tileGridLayout.constraintCount = numRowsAndCols;
        
        CreateAllTiles();
    }

    private void DestroyAllTiles()
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                Destroy(allTiles[i, j].tileObject);
                Destroy(allTiles[i, j].tileBackgroundObject);
            }
        }
    }

    public bool MoveDown()
    {
        bool moveMade = false;
        
        for (int i = totalRows - 1; i >= 0; i--)
        {
            for (int j = 0; j < totalCols; j++)
            {
                if (SlideDown(i, j))
                    moveMade = true;

                if (i + 1 < totalRows && MergeTiles(allTiles[i, j], allTiles[i + 1, j]))
                {
                    moveMade = true;
                    j--;
                }
            }
        }

        return moveMade;
    }

    private bool SlideDown(int tileRow, int tileCol)
    {
        if (allTiles[tileRow, tileCol].IsEmpty())
        {
            for (int k = tileRow; k >= 0; k--)
            {
                if (!allTiles[k, tileCol].IsEmpty())
                {
                    allTiles[k, tileCol].SlideTo(allTiles[tileRow, tileCol]);
                    return true;
                }
            }
        }

        return false;
    }

    public bool MoveUp()
    {
        bool moveMade = false;
        
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                if (SlideUp(i, j))
                    moveMade = true;

                if (i - 1 >= 0 && MergeTiles(allTiles[i, j], allTiles[i - 1, j]))
                {
                    moveMade = true;
                    j--;
                }
            }
        }

        return moveMade;
    }
    
    private bool SlideUp(int tileRow, int tileCol)
    {
        if (allTiles[tileRow, tileCol].IsEmpty())
        {
            for (int k = tileRow; k < totalRows; k++)
            {
                if (!allTiles[k, tileCol].IsEmpty())
                {
                    allTiles[k, tileCol].SlideTo(allTiles[tileRow, tileCol]);
                    return true;
                }
            }
        }

        return false;
    }

    public bool MoveLeft()
    {
        bool moveMade = false;
        
        for (int j = 0; j < totalCols; j++)
        {
            for (int i = 0; i < totalRows; i++)
            {
                if (SlideLeft(i, j))
                    moveMade = true;

                if (j - 1 >= 0 && MergeTiles(allTiles[i, j], allTiles[i, j - 1]))
                {
                    moveMade = true;
                    i--;
                }
            }
        }

        return moveMade;
    }
    
    private bool SlideLeft(int tileRow, int tileCol)
    {
        if (allTiles[tileRow, tileCol].IsEmpty())
        {
            for (int k = tileCol; k < totalCols; k++)
            {
                if (!allTiles[tileRow, k].IsEmpty())
                {
                    allTiles[tileRow, k].SlideTo(allTiles[tileRow, tileCol]);
                    return true;
                }
            }
        }

        return false;
    }

    public bool MoveRight()
    {
        bool moveMade = false;
        
        for (int j = totalCols - 1; j >= 0; j--)
        {
            for (int i = 0; i < totalRows; i++)
            {
                if (SlideRight(i, j))
                    moveMade = true;

                if (j + 1 < totalCols && MergeTiles(allTiles[i, j], allTiles[i, j + 1]))
                {
                    moveMade = true;
                    i--;
                }
            }
        }

        return moveMade;
    }
    
    private bool SlideRight(int tileRow, int tileCol)
    {
        if (allTiles[tileRow, tileCol].IsEmpty())
        {
            for (int k = tileCol; k >= 0; k--)
            {
                if (!allTiles[tileRow, k].IsEmpty())
                {
                    allTiles[tileRow, k].SlideTo(allTiles[tileRow, tileCol]);
                    return true;
                }
            }
        }

        return false;
    }

    private bool MergeTiles(Tile tile1, Tile tile2)
    {
        if (!tile1.IsEmpty() && !tile2.IsEmpty() &&
            tile1.number == tile2.number &&
            !tile1.mergedThisTurn && !tile2.mergedThisTurn)
        {
            tile1.MergeWith(tile2);
            
            gameManager.AddPoints(tile2.number);
            
            if (IsNewTileCreated(tile2.number))
                PlayConfetti();
            
            if (IsGameWon(tile2.number))
                gameManager.GameWon();

            return true;
        }

        return false;
    }

    private bool IsNewTileCreated(int tileNumber)
    {
        return createdTiles.Add(tileNumber);
    }
    private void PlayConfetti()
    {
        gameManager.confettiAnimationHandler.PlayConfetti();
    }

    private bool IsGameWon(int newTileNumber)
    {
        return newTileNumber == WinningTile;
    }

    public void CheckIfTileSpawnedThisTurn()
    {
        if (!hasNewTileSpawnedThisTurn)
        {
            hasNewTileSpawnedThisTurn = true;
            SpawnNewTile();
        }
    }
    
    private void SpawnNewTile()
    {
        List<Tuple<int, int>> emptySpaces = GetAllEmptySpaces();

        if (emptySpaces.Count > 0)
        {
            Tuple<int, int> newTileCoordinates = GetRandomSpaceIn(emptySpaces);
            int newTileVal = GetRandomNewTile();
            
            CreateTileAt(newTileCoordinates.Item1, newTileCoordinates.Item2, newTileVal);
        }
    }

    private List<Tuple<int, int>> GetAllEmptySpaces()
    {
        List<Tuple<int, int>> emptySpaces = new List<Tuple<int, int>>();
        
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                if (allTiles[i, j].IsEmpty())
                    emptySpaces.Add(new Tuple<int, int>(i, j));
            }
        }

        return emptySpaces;
    }

    private Tuple<int, int> GetRandomSpaceIn(List<Tuple<int, int>> availableSpaces)
    {
        return availableSpaces.ElementAt(GetRandomNum(availableSpaces.Count));
    }

    public void RemoveAllTiles()
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                if (!allTiles[i, j].IsEmpty()) allTiles[i, j].Remove();
            }
        }
    }

    public void ResetThisTurnFlags()
    {
        hasNewTileSpawnedThisTurn = false;
        
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                allTiles[i, j].mergedThisTurn = false;
            }
        }
    }

    public bool MoveExists()
    {
        if (EmptyTileExists())
            return true;

        if (AdjacentEquivalentTilesExists())
            return true;

        return false;
    }

    private bool EmptyTileExists()
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                if (allTiles[i, j].IsEmpty())
                    return true;
            }
        }

        return false;
    }

    private bool AdjacentEquivalentTilesExists()
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalCols - 1; j++)
            {
                if (allTiles[i, j].number == allTiles[i, j + 1].number)
                    return true;
            }
        }
        
        for (int i = 0; i < totalRows - 1; i++)
        {
            for (int j = 0; j < totalCols; j++)
            {
                if (allTiles[i, j].number == allTiles[i + 1, j].number)
                    return true;
            }
        }

        return false;
    }

    public void SpawnRandomTiles()
    {
        List<Tuple<int, int>> emptySpaces = GetAllEmptySpaces();

        if (emptySpaces.Count > 0)
        {
            Tuple<int, int> newTileCoordinates = GetRandomSpaceIn(emptySpaces);
            int newTileVal = Random.Range(1, 10);
            
            CreateTileAt(newTileCoordinates.Item1, newTileCoordinates.Item2, (int) Math.Pow(GameManager.BaseNumber, newTileVal));
        }
    }
}
