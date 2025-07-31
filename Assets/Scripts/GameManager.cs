using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentCoins;

    public bool dialogActive;

    public float waitForDeathScreen = 1f, waitToRespawn = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCoins = SaveManager.instance.activeSave.currentCoins;
        // instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            PauseUnpause();
        }
    }

    public void GetCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;

        UIManager.instance.UpdateCoins();

        SaveManager.instance.activeSave.currentCoins = currentCoins;
    }

    public void PauseUnpause()
    {
        if(!UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(true);

            Time.timeScale = 0f;

            PlayerController.instance.canMove = false;
        } else
        {
            UIManager.instance.pauseScreen.SetActive(false);

            Time.timeScale = 1f;

            PlayerController.instance.canMove = true;
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(waitForDeathScreen);

        UIManager.instance.deathScreen.SetActive(true);

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        UIManager.instance.blackoutScreen.SetActive(true);

        PlayerController.instance.ResetOnRespawn();
    }
}
