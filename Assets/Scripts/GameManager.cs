using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverUI.activeInHierarchy && !victoryUI.activeInHierarchy)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
      
    }
    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        Debug.Log("Game Over");
    }
    public void Victory()
    {
        Time.timeScale = 0f;
        victoryUI.SetActive(true);
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    public void Quit()
    {
        Application.Quit();
    }
}
