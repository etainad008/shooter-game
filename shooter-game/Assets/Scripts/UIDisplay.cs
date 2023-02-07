using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIDisplay : MonoBehaviour
{
    private int coinsCollected = 0;
    public int startingScore = 0;
    private int currentScore = 1;
    public int startingTime;
    private float countdown;
    readonly KeyCode pauseMenu = KeyCode.Escape;
    private bool isPaused = false;
    private static int numberOfUpgrades = 5;
    private int[] upgradesCosts = new int[numberOfUpgrades];

    public TMP_Text healthDisplay;
    public TMP_Text coinsCollectedDisplay;
    public TMP_Text scoreDisplay;
    public TMP_Text ammoDisplay;
    public TMP_Text countdownDisplay;
    public TMP_Text openShopButtonDisplay;
    
    public GameObject shopMenuDisplay;
    public GameObject pauseMenuDisplay;

    private void Start()
    {
        currentScore = startingScore;
        countdown = startingTime;
        
        coinsCollectedDisplay.text = $"Coins: {coinsCollected}";
        scoreDisplay.text = $"Score: {currentScore}";
        //ammoDisplay.text = string.Format("Ammo: {0}", (weapon.ammo == 0) ? "Empty" : weapon.ammo);
        countdownDisplay.text = $"{countdown}";
    }

    private void Update()
    {
        HandleInput();
        HandleCountdown();
    }

    public void UpdateHealthDisplay(Component sender, object data)
    {
        healthDisplay.text = $"Health: {data}";
    }

    public void UpdateCoinCollectedDisplay()
    {
        coinsCollected++;
        coinsCollectedDisplay.text = $"Coins: {coinsCollected}";
    }

    public void UpdateScoreDisplay(Component sender, object data)
    {
        if(data is not int)
        {
            Debug.Log("'data' type should be int");
        }
        
        else
        {
            int scoreToAdd = (int) data;
            currentScore += scoreToAdd;
        }
        scoreDisplay.text = $"Score: {currentScore}";
    }

    public void UpdateAmmoDisplay(Component sender, object data)
    {
        ammoDisplay.text = string.Format("Ammo: {0}", ((int) data == 0) ? "Empty" : data);
    }

    public void LoadNextScene()
    {
        PlayerPrefs.SetInt("lastGameScore", currentScore);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(pauseMenu))
        {
            HandlePause();
        }
    }

    public void HandlePause()
    {
        Time.timeScale = isPaused ? 1f : 0;
        isPaused = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        pauseMenuDisplay.SetActive(isPaused);
    }

    private void HandleCountdown()
    {
        countdown -= Time.deltaTime; ;
        countdownDisplay.text = countdown.ToString("0");

        if (countdown <= 0)
        {
            LoadNextScene();
        }
    }

    public void OpenShopButtonDisplay(Component sender, object data)
    {
        if(data is bool)
        {
            bool isPlayerInShopRange = (bool) data;
            openShopButtonDisplay.gameObject.SetActive(isPlayerInShopRange);
        }
    }

    public void UpdateShopMenuDisplay(Component sender, object data)
    {
        if (data is bool)
        {
            bool isShopMenuOpen = (bool)data;
            shopMenuDisplay.SetActive(isShopMenuOpen);
        }
    }
}
