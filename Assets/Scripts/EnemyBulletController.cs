using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float moveSpeed;
    public int damageToPlayer;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        moveDir = PlayerController.instance.transform.position - transform.position;
        moveDir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer(damageToPlayer);
        }

        // if (other.tag != "Untagged")
        // {
            Destroy(gameObject);
            Debug.Log(other.tag);
        // }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
