using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverMenu : MonoBehaviour {

    public void TryAgain()
    {
        SceneManager.LoadScene("game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
