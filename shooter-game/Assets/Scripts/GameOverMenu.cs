using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    private int gameOverScoreDisplay;
    private int gameOverBestScoreDisplay;

    public TMP_Text scoreDisplay;
    public TMP_Text bestScoreDisplay;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        // Display score and max score
        gameOverScoreDisplay = PlayerPrefs.GetInt("lastGameScore", 0);
        gameOverBestScoreDisplay = PlayerPrefs.GetInt("bestScore", 0);


        if (gameOverBestScoreDisplay < gameOverScoreDisplay)
        {
            PlayerPrefs.SetInt("bestScore", gameOverScoreDisplay);
            gameOverBestScoreDisplay = PlayerPrefs.GetInt("bestScore", 0);
        }

        scoreDisplay.text = $"Score: {gameOverScoreDisplay}";
        bestScoreDisplay.text = $"Best Score: {gameOverBestScoreDisplay}";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
