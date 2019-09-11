using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileStyle
{
    public Color32 tileColour;
    public string tileNumber;
    public Color32 tileTextColour;
    
    private static Color32 defaultColour = new Color32(255, 255, 255, 0 );

    public static TileStyle DefaultTileStyle()
    {
        TileStyle tileStyle = new TileStyle();
        tileStyle.tileColour = defaultColour;
        tileStyle.tileNumber = "0";
        tileStyle.tileTextColour = defaultColour;
        return tileStyle;
    }
}
public class TileStylesHandler : MonoBehaviour
{
    // all possible tiles (2, 4, 8, ..., 4096)
    public TileStyle[] tileStyles;
    public const int MaxTile = 4096;
}
