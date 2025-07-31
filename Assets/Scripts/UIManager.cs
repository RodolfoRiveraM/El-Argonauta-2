using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider healthSlider, staminaSlider;
    public TMP_Text healthText, staminaText, coinsText;

    public string mainMenuScene;
    public GameObject pauseScreen;

    public GameObject blackoutScreen;

    public Slider bossHealthSlider;
    public TMP_Text bossNameText;

    public GameObject deathScreen;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
        UpdateStamina();
        UpdateCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth()
    {
        healthSlider.maxValue = PlayerHealthController.instance.maxHealth;
        healthSlider.value = PlayerHealthController.instance.currentHealth;
        healthText.text = "HEALTH" + PlayerHealthController.instance.currentHealth + "/" + PlayerHealthController.instance.maxHealth;
    }

    public void UpdateStamina()
    {
        staminaSlider.maxValue = PlayerController.instance.totalStamina;
        staminaSlider.value = PlayerController.instance.currentStamina;
        staminaText.text = "Stamina" + Mathf.RoundToInt(PlayerController.instance.currentStamina) + "/" + PlayerController.instance.totalStamina;
    }

    public void UpdateCoins ()
    {
        coinsText.text = "Coins: " + GameManager.instance.currentCoins;
    }

    public void Resume ()
    {
        GameManager.instance.PauseUnpause();
    }

    public void GoToMainMenu ()
    {
        SceneManager.LoadScene(mainMenuScene);

        Time.timeScale = 1f;
    }

    public void QuitGame ()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
