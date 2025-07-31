using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 exitLocation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.transform.position = exitLocation;

            PlayerController.instance.theRB.velocity = Vector2.zero;
            PlayerController.instance.canMove = true;

            UIManager.instance.blackoutScreen.SetActive(true);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
