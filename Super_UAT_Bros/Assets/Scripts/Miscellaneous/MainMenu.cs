using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Player_Spawn player_spawn; //Setting the palyer_Spawn


    private void Update()
    {
        //Getting the player_spawn
        if (player_spawn == null)
        {
            player_spawn = FindObjectOfType<Player_Spawn>();
        }
    }
    //Play the game or play again
    public void Play()
    {
        //Set starting position
        GameManager.instance.Scene_Name = "Level 1";
        GameManager.instance.posx = -6.89732f;
        GameManager.instance.posy = -1.98808f;

        player_spawn.gameObject.SetActive(true);
        GameManager.instance.GUI_ACTIVE = true;
        GameManager.instance.Goto_Scene(GameManager.instance.Scene_Name);
        GameManager.instance.currentHealth = GameManager.instance.maxHealth;
        GameManager.instance.healthUI.fillAmount = GameManager.instance.currentHealth / GameManager.instance.maxHealth;
        Instantiate(GameManager.instance.playerPrefab);
        AudioManager.mastersounds.Play("Music");
    
    SceneManager.LoadScene(1);
    }
    //Quit the game
    public void Quit()
    {
        Application.Quit();
    }
}
