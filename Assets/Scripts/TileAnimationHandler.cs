using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationHandler : MonoBehaviour
{
    public Animator tileAnimator;
    public TilesManager tilesManager;
    
    private const float Time = 0.2f;
    private const string PopupTrigger = "Popup";
    private const string MergeTrigger = "Merge";

    private void Awake()
    {
        tilesManager = GetComponentInParent<TilesManager>();
    }

    public void PopupAnimation()
    {
        tileAnimator.SetTrigger(PopupTrigger);
    }

    public void MergeAnimation()
    {
        tileAnimator.SetTrigger(MergeTrigger);
    }
    
    public void AnimateTileSlide(Tile slidingTile, Tile destinationTile, bool isMerge)
    {
        LeanTween.cancel(slidingTile.tileObject, true);
        
        Vector3 originalPosition = slidingTile.tileObject.transform.position;

        LeanTween.move(slidingTile.tileObject, destinationTile.transform.position, Time).setOnComplete(ApplyTileStylesAndResetPosition);

        void ApplyTileStylesAndResetPosition()
        {
            slidingTile.ApplyTileStyle();
            destinationTile.ApplyTileStyle();
            slidingTile.tileObject.transform.position = originalPosition;
            
            tilesManager.CheckIfTileSpawnedThisTurn();
            
            if (isMerge)
                destinationTile.Merged();
        }
    }
}
