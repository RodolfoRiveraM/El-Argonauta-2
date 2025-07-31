using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToRestore;

    public float lifetime;

    public float waitToPickup = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (lifetime > 0)
        {
            Destroy(gameObject, lifetime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToPickup > 0)
        {
            waitToPickup -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player" && waitToPickup <= 0)
        {
            PlayerHealthController.instance.RestoreHealth(healthToRestore);
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(6);
        }
    }
}
