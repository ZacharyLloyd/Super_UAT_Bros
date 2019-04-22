using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    public float enemiesToBeKilled; //Setting the number of enemies needed to be killed to win
    // Start is called before the first frame update
    void Start()
    {
        //Gets the number of enemies in the level
        enemiesToBeKilled = FindObjectsOfType<EnemyPawn>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        //Setting up the winning factor for when all the enemies are killed
        if (enemiesToBeKilled == 0)
        {
            GameManager.instance.GUI_ACTIVE = false;
            GameManager.instance.healthUIParent.gameObject.SetActive(GameManager.instance.GUI_ACTIVE);
            GameManager.instance.Scene_Name = "Win Screen";
            SceneManager.LoadScene(GameManager.instance.Scene_Name);
            AudioManager.mastersounds.Stop("Music");
        }
    }
    private void FixedUpdate()
    {
    }
}
