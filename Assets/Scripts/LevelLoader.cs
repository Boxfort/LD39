using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Dictionary<Color, GameObject> tileMap = new Dictionary<Color, GameObject>();
    public GameObject[] tiles;

    public Texture2D map;

	// Use this for initialization
	void Start ()
    {
        //Init Tilemap
        tileMap.Add(new Color(0, 0, 0), tiles[0]);       //Wall
        tileMap.Add(new Color(100, 100, 100), tiles[1]); //Floor
        tileMap.Add(new Color(0, 0, 255), tiles[2]);     //Battery
        tileMap.Add(new Color(255, 0, 0), tiles[3]);     //Battery port
        tileMap.Add(new Color(150, 150, 150), tiles[4]); //terminal right
        tileMap.Add(new Color(155, 150, 150), tiles[5]); //terminal left
        tileMap.Add(new Color(150, 155, 150), tiles[6]); //terminal up
        tileMap.Add(new Color(150, 150, 155), tiles[7]); //terminal down

        LoadLevel();
    }

    void LoadLevel()
    {
        //Loop through every pixel on the image supplied
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; i++)
            {
                Color pixel = map.GetPixel(i, j);
                if (tileMap.ContainsKey(pixel))
                {
                    Instantiate(tileMap[pixel], new Vector3(i, j, 1.0f), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
	}
}
