using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomActivator : MonoBehaviour
{
    public GameObject[] allEnemies;
    private List<GameObject> clonedEnemies = new List<GameObject>();

    public bool lockDoors;
    public GameObject[] doors;
    private bool doorsLocked, dontSpawnEnemies;

    public bool inBossRoom;
    public Transform bossCamPointLwr, bossCamPointUpr;
    public GameObject theBoss;
    private bool dontReactivateBoss;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemy in allEnemies)
        {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doorsLocked)
        {
            bool enemyFound = false;

            for (int i = 0; i < clonedEnemies.Count; i++)
            {
                if (clonedEnemies[i] != null)
                {
                    enemyFound = true;
                }
            }

            if (!enemyFound)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                }

                doorsLocked = false;
                lockDoors = false;
                dontSpawnEnemies = true;
            }
        }
    }

    private void SpawnEnemies()
    {
        if (!dontSpawnEnemies)
        {
            foreach(GameObject enemy in allEnemies)
            {
                GameObject newEnemy = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
                newEnemy.SetActive(true);
                clonedEnemies.Add(newEnemy);
            }
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
        if (other.tag == "Player")
        {
            DugeonCameraController.instance.targetPoint = new Vector3(transform.position.x, transform.position.y, DugeonCameraController.instance.targetPoint.z);
            SpawnEnemies();

            if (lockDoors)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }

                doorsLocked = true;
            }

            if (inBossRoom)
            {
                DugeonCameraController.instance.ActivateBossRoom(bossCamPointUpr.position, bossCamPointLwr.position);
                
                if (!dontReactivateBoss)
                {
                    theBoss.SetActive(true);
                    dontReactivateBoss = true;
                }
            } else
            {
                DugeonCameraController.instance.inBossRoom = false;
            }
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
