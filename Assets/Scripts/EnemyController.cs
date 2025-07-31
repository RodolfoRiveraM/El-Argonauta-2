using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public Animator anim;
    public BoxCollider2D area;

    public float moveSpeed;

    public float waitTime, moveTime;
    private float waitCounter, moveCounter;

    private Vector2 moveDir;

    public bool shouldChase;
    private bool isChasing;
    public float chaseSpeed, rangeToChase, waitAfterHitting;

    public int damageToDeal = 10;

    private bool isKnockingBack;
    public float knockbackTime, knockbackForce, waitAfterKnocking;
    private float knockbackCounter, knockWaitcounter;
    private Vector2 knockDir;

    public bool shouldShoot;
    public GameObject bullet;
    public float timeBetweenShots;
    private float shotCounter;
    public Transform shotPoint;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = Random.Range(waitTime * .5f, waitTime * 1.25f);

        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnockingBack)
        {
            if (!isChasing)
            {
                if (waitCounter > 0)
                {
                    waitCounter = waitCounter - Time.deltaTime;
                    theRB.velocity = Vector2.zero;
                    if (waitCounter <= 0)
                    {
                        moveCounter = Random.Range(moveTime * .3f, moveTime * 1.25f);

                        anim.SetBool("moving", true);

                        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        moveDir.Normalize();
                    }
                }
                else
                {
                    moveCounter -= Time.deltaTime;

                    theRB.velocity = moveDir * moveSpeed;

                    if (moveCounter <= 0)
                    {
                        waitCounter = Random.Range(waitTime * .3f, waitTime * 1.25f);
                        anim.SetBool("moving", false);
                    }

                    if (shouldChase && PlayerController.instance.gameObject.activeInHierarchy)
                    {
                        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase)
                        {
                            isChasing = true;
                        }
                    }
                }

                if (shouldShoot)
                {
                    shotCounter -= Time.deltaTime;
                    if(shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots;

                        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                    }
                }
            }
            else
            {
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                    theRB.velocity = Vector2.zero;

                    if (waitCounter <= 0)
                    {
                        anim.SetBool("moving", true);
                    }
                }
                else
                {
                    moveDir = PlayerController.instance.transform.position - transform.position;
                    moveDir.Normalize();

                    theRB.velocity = moveDir * chaseSpeed;
                }

                if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > rangeToChase || !PlayerController.instance.gameObject.activeInHierarchy)
                {
                    isChasing = false;

                    waitCounter = waitTime;

                    anim.SetBool("moving", false);
                }
            }
        } else
        {
            Debug.Log(knockbackCounter);
            Debug.Log("-------------");
            Debug.Log(knockWaitcounter);
            Debug.Log("-------------");
            if (knockbackCounter > 0)
            {
                knockbackCounter -= Time.deltaTime;

                theRB.velocity = knockDir * knockbackForce;

                if (knockbackCounter <= 0)
                {
                    knockWaitcounter = waitAfterKnocking;
                }
            } else
            {
                knockWaitcounter -= Time.deltaTime;
                theRB.velocity = Vector2.zero;
                if (knockWaitcounter <= 0)
                {
                    isKnockingBack = false;
                }
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, area.bounds.min.x + 1f, area.bounds.max.x - 1f),
          Mathf.Clamp(transform.position.y, area.bounds.min.y + 1f, area.bounds.max.y - 1f), transform.position.z);
    }

    public void knockBack(Vector3 knockerPosition)
    {
        Debug.Log("*************************");
        knockbackCounter = knockbackTime;
        isKnockingBack = true;

        knockDir = transform.position - knockerPosition;
        knockDir.Normalize();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (isChasing)
            {
                waitCounter = waitAfterHitting;
                anim.SetBool("moving", false);

                PlayerController.instance.knockBack(transform.position);

                PlayerHealthController.instance.DamagePlayer(damageToDeal);
            }
        }
    }
}
