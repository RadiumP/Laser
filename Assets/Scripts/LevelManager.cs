using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public void LoadNewLevel(string name)
    {
        //Debug.Log("Level load requested" + name);
        //Brick.breakableCount = 0;
        //Application.LoadLevel(name);
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();//Limitations: Works great for pc and consoles but not debug and Web/Mobile, so not good
    }

    public void ReturnToMain()
    {
        string name = "Start";
        SceneManager.LoadScene(name);       
    }

    public void GoToRank()
    {
        string name = "Rank";
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel()
    {
        //Application.loadedLevel outdated
        //Brick.breakableCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
   
    public int getLvl()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
