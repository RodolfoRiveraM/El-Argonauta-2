using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public float moveSpeed, rotateSpeed;
    public int damageToPlayer;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    public void SetDirection(Vector3 spawnerPosition)
    {
        moveDir = transform.position - spawnerPosition;
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