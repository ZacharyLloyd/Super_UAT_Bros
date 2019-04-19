using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winning : MonoBehaviour
{
    public float enemiesToBeKilled;
    // Start is called before the first frame update
    void Start()
    {
        enemiesToBeKilled = FindObjectsOfType<EnemyPawn>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesToBeKilled == 0)
        {
            GameManager.instance.GUI_ACTIVE = false;
            GameManager.instance.healthUIParent.gameObject.SetActive(GameManager.instance.GUI_ACTIVE);
            GameManager.instance.Scene_Name = "Win Screen";
            SceneManager.LoadScene(GameManager.instance.Scene_Name);
           
        }
    }
    private void FixedUpdate()
    {
    }
}
