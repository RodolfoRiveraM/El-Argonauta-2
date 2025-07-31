using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool inRange;

    public GameObject objectToSwitchOff;

    private bool isOn;

    public SpriteRenderer switchSprite;
    public Sprite offSprite, onSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Comunicar"))
            {
                isOn = !isOn;

                if (isOn)
                {
                    switchSprite.sprite = onSprite;
                } else
                {
                    switchSprite.sprite = offSprite;
                }

                objectToSwitchOff.SetActive(!isOn);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
