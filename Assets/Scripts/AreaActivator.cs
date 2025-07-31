using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaActivator : MonoBehaviour
{
    private BoxCollider2D areaBox;

    public GameObject[] allEnemies;
    public List<GameObject> clonedEnemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        areaBox = GetComponent<BoxCollider2D>();

        foreach(GameObject enemy in allEnemies)
        {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemies()
    {
        foreach(GameObject enemy in allEnemies)
        {
            GameObject newEnemy = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            newEnemy.SetActive(true);
            clonedEnemies.Add(newEnemy);
        }
    }

    private void DespawnEnemies()
    {
        foreach(GameObject enemy in clonedEnemies)
        {
            Destroy(enemy);
        }

        clonedEnemies.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.areaBox = areaBox;

            SpawnEnemies();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerHealthController.instance.currentHealth > 0)
            {
                DespawnEnemies();
            }
        }
    }
}
