using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public GameObject tileObject, tileBackgroundObject;

    public bool mergedThisTurn = false;
    public int number = 0;
    private const int EmptyTilesNum = 0;
    private TileStylesHandler tileStylesHandler;
    private TileAnimationHandler tileAnimationHandler;
    
    private void Awake()
    {
        tileAnimationHandler = GetComponent<TileAnimationHandler>();
        tileStylesHandler = GetComponentInParent<TileStylesHandler>();
    }

    public void Spawn(int newValue)
    {
        number = newValue;
        ApplyTileStyle();

        tileAnimationHandler.PopupAnimation();
    }
    
    public void ApplyTileStyle()
    {
        TileStyle ts = GetTileStyle();
        
        tileObject.GetComponent<Image>().color = ts.tileColour;
        tileObject.GetComponentInChildren<Text>().text = number.ToString();
        tileObject.GetComponentInChildren<Text>().color = ts.tileTextColour;
    }
    
    private TileStyle GetTileStyle()
    {
        if (IsEmpty())
            return TileStyle.DefaultTileStyle();
        if (number > TileStylesHandler.MaxTile)
            return tileStylesHandler.tileStyles[CalculateTileStyleIndexNumber(TileStylesHandler.MaxTile)];
        
        return tileStylesHandler.tileStyles[CalculateTileStyleIndexNumber(number)];
    }

    private int CalculateTileStyleIndexNumber(int tileNum)
    {
        return (int) Math.Log(tileNum, GameManager.BaseNumber) - 1;
    }

    public bool IsEmpty()
    {
        return number == EmptyTilesNum;
    }

    public void Remove()
    {
        number = EmptyTilesNum;
        ApplyTileStyle();
    }

    public void SlideTo(Tile otherTile)
    {
        otherTile.number = number;
        number = EmptyTilesNum;

        tileAnimationHandler.AnimateTileSlide(this, otherTile, false);
    }

    public void MergeWith(Tile otherTile)
    {
        otherTile.number *= GameManager.BaseNumber;
        otherTile.mergedThisTurn = true;
        number = EmptyTilesNum;
        
        tileAnimationHandler.AnimateTileSlide(this, otherTile, true);
    }

    public void Merged()
    {
        tileAnimationHandler.MergeAnimation();
    }
}
