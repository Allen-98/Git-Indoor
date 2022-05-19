using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.alive)
        {
            Invoke("GameOver", 1f);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }


    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        Time.timeScale = 0f;
    }



}
