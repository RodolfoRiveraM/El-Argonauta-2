using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject[] toDrop;
    [Range(0f, 100f)] public float chanceToDrop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        if (Random.Range(0f, 100f) <= chanceToDrop)
        {
            if (toDrop.Length > 0)
            {
                Instantiate(toDrop[Random.Range(0, toDrop.Length)], transform.position, transform.rotation);
            }
        }
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(2);
    }
}
