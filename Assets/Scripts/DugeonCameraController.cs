using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DugeonCameraController : MonoBehaviour
{
    public static DugeonCameraController instance;

    public float moveSpeed;

    public Vector3 targetPoint;

    public bool inBossRoom;
    private Vector3 limitUpr, limitLwr;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPoint.z = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (inBossRoom)
        {
            targetPoint.y = Mathf.Clamp(PlayerController.instance.transform.position.y, limitLwr.y, limitUpr.y);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }

    public void ActivateBossRoom(Vector3 upr, Vector3 lwr)
    {
        inBossRoom = true;
        limitUpr = upr;
        limitLwr = lwr;
    }
}
