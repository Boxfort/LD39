using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Canvas UI;
    AudioSource[] sounds;
    public MessageboxScript mbox;

    public GameObject pauseScreen;

    public bool paused = false;

	// Use this for initialization
	void Start () {
        sounds = GetComponents<AudioSource>();
	}
	
    void Pause()
    {
        paused = true;
        sounds[0].Pause();
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    void UnPause()
    {
        paused = false;
        sounds[0].UnPause();
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                UnPause();
        }
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
        StartCoroutine(winner());
    }

    IEnumerator winner()
    {
        mbox.addMessagesToQueue(new string[] { "The bounty hunters have bugged out, you made it! " });
        yield return new WaitForSeconds(6.5f);
        UI.enabled = false;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("winner");
    }
}
