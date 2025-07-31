using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressChecker : MonoBehaviour
{

    public string progressToCheck;

    public bool deactivateIfMarked;

    // Start is called before the first frame update
    void Start()
    {
        bool isMarked = SaveManager.instance.CheckProgress(progressToCheck);

        if(deactivateIfMarked)
        {
            gameObject.SetActive(!isMarked);
        } else {
            gameObject.SetActive(isMarked);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
