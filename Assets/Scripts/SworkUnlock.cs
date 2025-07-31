using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SworkUnlock : MonoBehaviour
{
    public GameObject door;

    public int newSwordDamage, swordSpriteRef;

    public string[] pickupDialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.SetActive(false);

            if(door != null)
            {
                door.SetActive(false);
            }

            PlayerController.instance.UpgradeSword(newSwordDamage, swordSpriteRef);

            if (pickupDialog.Length > 0)
            {
                DialogManager.instance.ShowDialog(pickupDialog, false);
            }
        }
    }
}
