using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;

    [HideInInspector]
    public Rigidbody2D theRB;

    private Animator anim;

    private SpriteRenderer theSR;

    private float horizontal;
    private float vertical;

    public Animator wpnAnim;

    private bool isKnockingBack;
    public float knockbackTime, knockbackForce;
    private float knockbackCounter;
    private Vector2 knockDir;

    public GameObject hitEffect;

    public float dashSpeed, dashLength, dashStamCost;
    private float dashCounter, activeMoveSpeed;

    public float totalStamina, stamRefillSpeed;
    [HideInInspector]
    public float currentStamina;

    private bool isSpinning;
    public float spinCost, spinCooldwn;
    private float spinCounter;

    public bool canMove = true;

    public SpriteRenderer swordSR;
    public Sprite[] allSwords;
    public DamageEnemy swordDmg;
    public int currentSword;

    private Vector3 respawnPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SaveManager.instance.activeSave.sceneStartPosition;

        currentSword = SaveManager.instance.activeSave.currentSword;
        swordSR.sprite = allSwords[currentSword];
        swordDmg.damageToDeal = SaveManager.instance.activeSave.swordDamage;

        totalStamina = SaveManager.instance.activeSave.maxStamina;

        theSR = GetComponent<SpriteRenderer>();
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activeMoveSpeed = moveSpeed;
        currentStamina = totalStamina;

        UIManager.instance.UpdateStamina();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !GameManager.instance.dialogActive)
        {
            if (!isKnockingBack)
            {
                //transform.position = new Vector3(transform.position.x + (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime), transform.position.y + (Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime), transform.position.z);

                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * activeMoveSpeed;

                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");

                VoltearSprite();

                if (horizontal != 0 || vertical != 0)
                {
                    anim.SetBool("Corriendo", true);
                    SetXYAnimator();
                }
                else
                {
                    anim.SetBool("Corriendo", false);
                }

                if (Input.GetButtonDown("Atacar") && !isSpinning)
                {
                    wpnAnim.SetTrigger("Attack");
                    anim.SetTrigger("Atacar");
                    AudioManager.instance.PlaySFX(0);
                }

                if (dashCounter <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= dashStamCost)
                    {
                        activeMoveSpeed = dashSpeed;
                        dashCounter = dashLength;

                        currentStamina -= dashStamCost;
                    }
                } else
                {
                    dashCounter -= Time.deltaTime;

                    if (dashCounter <= 0)
                    {
                        activeMoveSpeed = moveSpeed;
                    }
                }

                //Girar la espada
                if (spinCounter <= 0)
                {
                   if (Input.GetButtonDown("Spin") && currentStamina >= spinCost)
                    {
                        wpnAnim.SetTrigger("SpinAttack");

                        currentStamina -= spinCost;

                        spinCounter = spinCooldwn;

                        isSpinning = true;

                        AudioManager.instance.PlaySFX(0);
                    } 
                } else
                {
                    spinCounter -= Time.deltaTime;
                    if (spinCounter <= 0)
                    {
                        isSpinning = false;
                    }
                }

                currentStamina += stamRefillSpeed * Time.deltaTime;
                if (currentStamina > totalStamina)
                {
                    currentStamina = totalStamina;
                }

                UIManager.instance.UpdateStamina();

            } else
            {
                knockbackCounter -= Time.deltaTime;
                theRB.velocity = knockDir * knockbackForce;

                if (knockbackCounter <= 0)
                {
                    isKnockingBack = false;
                }
            }
        } else {
            theRB.velocity = Vector2.zero;
            anim.SetFloat("Speed", 0f);
        }
    }

    public void knockBack(Vector3 knockerPosition)
    {
        knockbackCounter = knockbackTime;
        isKnockingBack = true;

        knockDir = transform.position - knockerPosition;
        knockDir.Normalize();

        Instantiate(hitEffect, transform.position, transform.rotation);
    }

    private void VoltearSprite()
    {
        if (horizontal > 0 && Mathf.Abs(vertical) <= Mathf.Abs(horizontal))
        {
            theSR.flipX = true;
        }
        else if (horizontal != 0)
        {
            theSR.flipX = false;
        }
    }

    private void SetXYAnimator()
    {
        anim.SetFloat("x", horizontal);
        anim.SetFloat("y", vertical);
        wpnAnim.SetFloat("dirX", horizontal);
        wpnAnim.SetFloat("dirY", vertical);
    }

    public void DoAtLevelStart()
    {
        canMove = true;

        respawnPos = transform.position;

        Debug.Log(transform.position);
    }

    public void UpgradeSword(int newDamage, int newSwordRef)
    {
        swordDmg.damageToDeal = newDamage;
        currentSword = newSwordRef;
        swordSR.sprite = allSwords[newSwordRef];

        SaveManager.instance.activeSave.currentSword = currentSword;
        SaveManager.instance.activeSave.swordDamage = newDamage;
    }

    public void ResetOnRespawn()
    {
        transform.position = respawnPos;
        
        canMove = true;

        gameObject.SetActive(true);
        currentStamina = totalStamina;
        knockbackCounter = 0f;
        PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
    }
}
