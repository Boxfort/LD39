using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    bool clicked = false;

    public GameObject[] toDisable;
    public GameObject[] toEnable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        if (!clicked)
        {
            foreach(GameObject i in toDisable)
            {
                i.SetActive(false);
            }

            foreach (GameObject i in toEnable)
            {
                i.SetActive(true);
            }

            clicked = true;
        }
        else
        {
            SceneManager.LoadScene("game", LoadSceneMode.Single);
        }
    }
}
