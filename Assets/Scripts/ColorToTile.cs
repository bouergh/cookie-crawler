using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ColorToTile
{
    public Color color;
    public Tile tile;
    public int map; // 0: base, 1: collision
}
