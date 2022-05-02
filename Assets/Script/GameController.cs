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
    public bool isOver;

    public static GameController instance;

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
    }

    public void NextLevel(string levelName){
        SceneManager.LoadScene(levelName);
        isOver = false;
    }


}
