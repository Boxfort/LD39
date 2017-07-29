using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Dictionary<Color32, GameObject> tileMap = new Dictionary<Color32, GameObject>();
    public GameObject[] tiles;

    public Texture2D map;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("Initialising Tilemap");

        //Init Tilemap 
        tileMap.Add(new Color32(0, 0, 0, 255), tiles[0]);                //Wall
        tileMap.Add(new Color32(100, 100, 100, 255), tiles[1]); //Floor
        tileMap.Add(new Color32(0, 0, 255, 255), tiles[2]);             //Battery
        tileMap.Add(new Color32(255, 0, 0, 255), tiles[3]);             //Battery port
        tileMap.Add(new Color32(150, 150, 150, 255), tiles[4]); //terminal right
        tileMap.Add(new Color32(155, 150, 150, 255), tiles[5]); //terminal left
        tileMap.Add(new Color32(150, 155, 150, 255), tiles[6]); //terminal up
        tileMap.Add(new Color32(150, 150, 155, 255), tiles[7]); //terminal down


        Debug.Log("Tilemap Initialised");

        LoadLevel();
    }

    void LoadLevel()
    {
        Debug.Log("Loading level");


        //Loop through every pixel on the image supplied
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {
                Color32 pixel = map.GetPixel(i, j);

                Debug.Log("x: " + i + " y: " + j + " | Color = " + pixel.ToString());

                if (tileMap.ContainsKey(pixel))
                {
                    Instantiate(tileMap[pixel], new Vector3(i, j, 1.0f), tileMap[pixel].transform.rotation);
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
	}
}
