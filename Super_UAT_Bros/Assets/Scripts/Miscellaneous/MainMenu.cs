using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Player_Spawn player_spawn;


    private void Update()
    {
        if (player_spawn == null)
        {
            player_spawn = FindObjectOfType<Player_Spawn>();
        }
    }
    //Play the game or play again
    public void Play()
    {
        //Set starting position!!!
        GameManager.instance.Scene_Name = "Level 1";
        GameManager.instance.posx = -6.89732f;
        GameManager.instance.posy = -1.98808f;

        //player = FindObjectOfType<Player>();
        player_spawn.gameObject.SetActive(true);
        GameManager.instance.GUI_ACTIVE = true;
        GameManager.instance.Goto_Scene(GameManager.instance.Scene_Name);
        GameManager.instance.currentHealth = GameManager.instance.maxHealth;
        GameManager.instance.healthUI.fillAmount = GameManager.instance.currentHealth / GameManager.instance.maxHealth;
        Instantiate(GameManager.instance.playerPrefab);
    
    SceneManager.LoadScene(1);
    }
    //Quit the game
    public void Quit()
    {
        Application.Quit();
    }
}
