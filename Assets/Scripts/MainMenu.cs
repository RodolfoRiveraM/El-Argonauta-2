using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance = null;
        }

        if (PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);
            PlayerController.instance = null;
        }
        
        if (SaveManager.instance.activeSave.hasBegun)
        {
            continueButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
        SaveManager.instance.ResetSave();
        SaveManager.instance.activeSave.hasBegun = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void Continue()
    {
        SceneManager.LoadScene(SaveManager.instance.activeSave.currentScene);
    }
}
