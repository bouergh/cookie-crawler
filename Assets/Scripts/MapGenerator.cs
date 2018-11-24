using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour {

    public Texture2D map;
    public ColorToTile[] colorMappings;
    public Tilemap baseMap;
    public Tilemap collisionMap;
    public GameObject player;

    // Use this for initialization
    void Start () {
        GenerateLevel();
	}
	
    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        foreach (ColorToTile colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                if (colorMapping.map == 0)
                    baseMap.SetTile(new Vector3Int(x, y, 0), colorMapping.tile);
                else
                    collisionMap.SetTile(new Vector3Int(x, y, 0), colorMapping.tile);
                break;
            }
           
            if (pixelColor.Equals(Color.green))
            {
                player.transform.position = new Vector3Int(x, y, 0);
            }
        }
    }
}
