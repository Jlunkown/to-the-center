using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    #region stuff
    Rigidbody2D playerrb;
    SpriteRenderer spriteRenderer;
    Animator anim;

    [SerializeField] Camera cam;

    Vector2 mousePos;
    Vector2 movement;

    //sprites
    [SerializeField] Sprite fireform;
    [SerializeField] Sprite waterform;
    [SerializeField] Sprite windform;
    [SerializeField] Sprite earthform;

    [HideInInspector] public float nextTimeToSmallAttack;
    [HideInInspector] public float nextTimeToMediumAttack;
    [HideInInspector] public float nextTimeToLargeAttack;

    [SerializeField] GameObject endScreen;

    [HideInInspector] public bool canJump;
    public float playerHealth;

    [Space(10)]
    [SerializeField] GameObject smallAttack;
    [SerializeField] GameObject mediumAtkCD;
    [SerializeField] GameObject bigAtkCD;
    #endregion

    #region player stats
    //base
    [Space(10)]
    [SerializeField] float ForceSpeed;
    public GameObject attackPoint;
    [SerializeField] GameObject rotateAttackPoint;
    [SerializeField] GameObject baseLight;

    //fire
    [Space(10)]
    [SerializeField] float speed;
    public float fireHealth;
    public float timeToUseFireMedium;
    [SerializeField] float fireSmallAttackRate;
    public float fireMediumAttackRate;
    public float fireBigAttackRate;
    [SerializeField] GameObject fireSmallBullet;
    [SerializeField] GameObject fireMediumBullet;
    [SerializeField] GameObject fireBigBullet;
    [SerializeField] GameObject fireLight;
    [HideInInspector] public float fireMediumLeft;

    //water
    [Space(10)]
    [SerializeField] float waterSpeed;
    public float waterHealth;
    [SerializeField] float jumpHeight;
    [SerializeField] float waterSmallAttackRate;
    public float waterMediumAttackRate;
    public float waterBigAttackRate;
    [SerializeField] GameObject waterSmallBullet;
    [SerializeField] GameObject waterMediumBullet;
    [SerializeField] GameObject waterBigBullet;

    //wind
    [Space(10)]
    public float windHealth;
    [SerializeField] float windSmallAttackRate;
    public float windMediumAttackRate;
    public float windBigAttackRate;
    [SerializeField] GameObject windSmallBullet;
    [SerializeField] GameObject windMediumBullet;
    [SerializeField] GameObject windBigBullet;

    //earth
    [Space(10)]
    [SerializeField] float earthSpeed;
    public float earthHealth;
    [SerializeField] float earthSmallAttackRate;
    public float earthMediumAttackRate;
    public float earthBigAttackRate;
    [SerializeField] GameObject earthSmallBullet;
    [SerializeField] GameObject earthMediumBullet;
    [SerializeField] GameObject earthBigBullet;

    #endregion

    #region checks/bools
    [Space(10)]
    public bool baseForm = true;

    public bool isFire;
    public bool isWater;
    public bool isWind;
    public bool isEarth;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region get components
        playerrb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();

        fireMediumLeft = timeToUseFireMedium;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        rotateAttackRotateThing();
        if (playerHealth > 0)
        {
            #region player movement
            if (baseForm)
            {
                basePlayerMovement();
                rotateCamera();
            }
            if (isFire)
            {
                smallAttack.GetComponent<playerUI>().fire = true;
                mediumAtkCD.GetComponent<playerUI>().fire = true;
                bigAtkCD.GetComponent<playerUI>().fire = true;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.maxValue = timeToUseFireMedium;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.value = fireMediumLeft;
                bigAtkCD.GetComponent<playerUI>().sliderCD.maxValue = fireBigAttackRate;
                bigAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToLargeAttack;

                baseLight.SetActive(false);
                fireLight.SetActive(true);
                spriteRenderer.sprite = fireform;
                anim.SetBool("hasElement", true);
                anim.SetFloat("Blend", 0);
                fireCharacterMoevemet();
                rotateCamera();
                fireAttacks();
            }
            if (isWater)
            {
                rotateAttackRotateThing();

                smallAttack.GetComponent<playerUI>().water = true;
                mediumAtkCD.GetComponent<playerUI>().water = true;
                bigAtkCD.GetComponent<playerUI>().water = true;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.maxValue = waterMediumAttackRate;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToMediumAttack;
                bigAtkCD.GetComponent<playerUI>().sliderCD.maxValue = waterBigAttackRate;
                bigAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToLargeAttack;

                spriteRenderer.sprite = waterform;
                anim.SetBool("hasElement", true);
                anim.SetFloat("Blend", 0.4f);
                waterCharacterMovement();
                waterAttack();
            }
            if (isWind)
            {
                smallAttack.GetComponent<playerUI>().wind = true;
                mediumAtkCD.GetComponent<playerUI>().wind = true;
                bigAtkCD.GetComponent<playerUI>().wind = true;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.maxValue = windMediumAttackRate;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToMediumAttack;
                bigAtkCD.GetComponent<playerUI>().sliderCD.maxValue = windBigAttackRate;
                bigAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToLargeAttack;

                spriteRenderer.sprite = windform;
                anim.SetBool("hasElement", true);
                anim.SetFloat("Blend", 0.7f);
                windCharacterMovement();
                rotateCamera();
                windAttack();
            }
            if (isEarth)
            {
                smallAttack.GetComponent<playerUI>().earth = true;
                mediumAtkCD.GetComponent<playerUI>().earth = true;
                bigAtkCD.GetComponent<playerUI>().earth = true;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.maxValue = earthMediumAttackRate;
                mediumAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToMediumAttack;
                bigAtkCD.GetComponent<playerUI>().sliderCD.maxValue = earthBigAttackRate;
                bigAtkCD.GetComponent<playerUI>().sliderCD.value = nextTimeToLargeAttack;

                spriteRenderer.sprite = earthform;
                anim.SetBool("hasElement", true);
                anim.SetFloat("Blend", 0.9f);
                earthAttack();
                rotateAttackRotateThing();
                earthCharacterMovement();
            }
            #endregion
        }
        else
        {
            endScreen.SetActive(true);
            playerHealth = -1000000;
        }
    }

    #region for player movement
    void basePlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerrb.AddForce(transform.up * ForceSpeed);
        }
    }

    void fireCharacterMoevemet()
    {
        baseForm = false;
        playerrb.mass = .9f;
        playerrb.gravityScale = 0;
        if (Input.GetKey(KeyCode.W))
        {
            playerrb.AddForce(transform.up * ForceSpeed);
        }
    }
    void waterCharacterMovement()
    {
        baseForm = false;
        playerrb.mass = 2f;
        playerrb.gravityScale = 5f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        movement.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerrb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            canJump = false;
        }

        transform.Translate(movement * waterSpeed * Time.deltaTime);
    }
    void windCharacterMovement()
    {
        baseForm = false;
        movement.y = Input.GetAxisRaw("Vertical");
        playerrb.mass = 0;
        playerrb.gravityScale = 0;

        transform.Translate(movement * speed * Time.deltaTime);
    }
    void earthCharacterMovement()
    {
        baseForm = false;
        playerrb.mass = 2f;
        playerrb.gravityScale = 5f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        movement.x = Input.GetAxisRaw("Horizontal");
        transform.Translate(movement * earthSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            playerrb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            canJump = false;
        }
    }
    void rotateCamera()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 LookingDir = mousePos - playerrb.position;
        float angle = Mathf.Atan2(LookingDir.y, LookingDir.x) * Mathf.Rad2Deg - 90f;
        playerrb.rotation = angle;
    }
    #endregion

    #region player attack
    void fireAttacks()
    {

        float timeAfterMedAtk = 0;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (nextTimeToSmallAttack <= 0)
            {
                Instantiate(fireSmallBullet, attackPoint.transform.position, transform.rotation);
                nextTimeToSmallAttack = fireSmallAttackRate;
            }
        }
        if (nextTimeToSmallAttack > 0)
        {
            nextTimeToSmallAttack -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (nextTimeToMediumAttack <= 0 && fireMediumLeft > 0)
            {
                fireMediumBullet.SetActive(true);
                nextTimeToMediumAttack = fireMediumAttackRate;
                timeAfterMedAtk = 2;
                fireMediumLeft -= Time.deltaTime;
            }
        }
        else
        {
            if (fireMediumLeft < timeToUseFireMedium && timeAfterMedAtk <= 0)
            {
                fireMediumLeft += Time.deltaTime;
            }
            else if (timeAfterMedAtk > 0)
            {
                timeAfterMedAtk -= Time.deltaTime;
            }
        }
        if (nextTimeToMediumAttack > 0)
        {
            nextTimeToMediumAttack -= Time.deltaTime;
        }
        if (fireMediumLeft <= 0)
        {
            fireMediumBullet.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (nextTimeToLargeAttack <= 0)
            {
                Instantiate(fireBigBullet, attackPoint.transform.position, transform.rotation);
                nextTimeToLargeAttack = fireBigAttackRate;
            }
        }
        if (nextTimeToLargeAttack > 0)
        {
            nextTimeToLargeAttack -= Time.deltaTime;
        }
    }
    void waterAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (nextTimeToSmallAttack <= 0)
            {
                Instantiate(waterSmallBullet, attackPoint.transform.position, rotateAttackPoint.transform.rotation);
                nextTimeToSmallAttack = waterSmallAttackRate;
            }
        }
        if (nextTimeToSmallAttack > 0)
        {
            nextTimeToSmallAttack -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (nextTimeToMediumAttack <= 0)
            {
                Instantiate(waterMediumBullet, attackPoint.transform.position, rotateAttackPoint.transform.rotation);
                nextTimeToMediumAttack = waterMediumAttackRate;
            }
        }
        if (nextTimeToMediumAttack > 0)
        {
            nextTimeToMediumAttack -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (nextTimeToLargeAttack <= 0)
            {
                Instantiate(waterBigBullet, attackPoint.transform.position, rotateAttackPoint.transform.rotation);
                nextTimeToLargeAttack = waterBigAttackRate;
            }
        }
        if (nextTimeToLargeAttack > 0)
        {
            nextTimeToLargeAttack -= Time.deltaTime;
        }
    }
    void windAttack()
    {
        Vector3 addPos = new Vector3(0, 10, 0);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (nextTimeToSmallAttack <= 0)
            {
                Instantiate(windSmallBullet, attackPoint.transform.position, transform.rotation);
                nextTimeToSmallAttack = windSmallAttackRate;
            }
        }
        if (nextTimeToSmallAttack > 0)
        {
            nextTimeToSmallAttack -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (nextTimeToMediumAttack <= 0)
            {
                Instantiate(windMediumBullet, attackPoint.transform.position, transform.rotation);
                nextTimeToMediumAttack = windMediumAttackRate;
            }
        }
        if (nextTimeToMediumAttack > 0)
        {
            nextTimeToMediumAttack -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (nextTimeToLargeAttack <= 0)
            {
                //Instantiate(windBigBullet, attackPoint.transform.position + addPos, transform.rotation);
                windBigBullet.SetActive(true);
                nextTimeToLargeAttack = windBigAttackRate;
            }
        }
        if (nextTimeToLargeAttack > 0)
        {
            nextTimeToLargeAttack -= Time.deltaTime;
        }
    }
    void earthAttack()
    {
        Vector3 addPos = new Vector3(0, 10, 0); 

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (nextTimeToSmallAttack <= 0)
            {
                Instantiate(earthSmallBullet, attackPoint.transform.position, rotateAttackPoint.transform.rotation);
                nextTimeToSmallAttack = earthSmallAttackRate;
            }
        }
        if (nextTimeToSmallAttack > 0)
        {
            nextTimeToSmallAttack -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (nextTimeToMediumAttack <= 0)
            {
                Instantiate(earthMediumBullet, attackPoint.transform.position, rotateAttackPoint.transform.rotation);
                nextTimeToMediumAttack = earthMediumAttackRate;
            }
        }
        if (nextTimeToMediumAttack > 0)
        {
            nextTimeToMediumAttack -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (nextTimeToLargeAttack <= 0)
            {
                Instantiate(earthBigBullet, transform.position + addPos, transform.rotation);
                nextTimeToLargeAttack = earthBigAttackRate;
            }
        }
        if (nextTimeToLargeAttack > 0)
        {
            nextTimeToLargeAttack -= Time.deltaTime;
        }
    }
    void rotateAttackRotateThing()
    {
        Vector2 LookingDir = mousePos - rotateAttackPoint.GetComponent<Rigidbody2D>().position;
        float angle = Mathf.Atan2(LookingDir.y, LookingDir.x) * Mathf.Rad2Deg - 90f;
        rotateAttackPoint.GetComponent<Rigidbody2D>().rotation = angle;
    }
    #endregion

}
