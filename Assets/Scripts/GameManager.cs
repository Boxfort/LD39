using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Canvas UI;
    AudioSource[] sounds;

	// Use this for initialization
	void Start () {
        sounds = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOverShip()
    {
        StartCoroutine(ship());
    }

    IEnumerator ship()
    {
        UI.enabled = false;
        sounds[1].Play();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("gameover_ship");
    }

    public void GameOverOxygen()
    {
        StartCoroutine(oxygen());
    }

    IEnumerator oxygen()
    {
        UI.enabled = false;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("gameover_oxygen");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("winner");
    }
}
