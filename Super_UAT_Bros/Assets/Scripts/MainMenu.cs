using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Play the game or play again
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    //Quit the game
    public void Quit()
    {
        Application.Quit();
    }
}
