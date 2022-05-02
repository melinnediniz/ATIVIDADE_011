using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public int totalScore;
    public Text scoreText;
    public GameObject gameOver;
    public GameObject next;
    public GameObject resetButton;
    public GameObject healthBar;
    public bool isOver;

    public static GameController instance;

    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        resetButton.SetActive(false);
        healthBar.SetActive(false);
    }



    public void RestartGame(string levelName)
    {
        SceneManager.LoadScene(levelName);
        isOver = false;
    }

    public void ShowNextLevel()
    {
        next.SetActive(true);
        isOver = true;
        resetButton.SetActive(false);
        healthBar.SetActive(false);
    }

    public void NextLevel(string levelName){
        SceneManager.LoadScene(levelName);
        isOver = false;
    }


}
