using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public bool behaveLikeNormalBullet;
    [SerializeField] bool followPlyerAttack;
    [SerializeField] bool followPlayerAttackPoint;
    [SerializeField] bool spawnSomeWhereElse;
    [SerializeField] bool penetrate;
    [SerializeField] bool isEnemyBullet;
    [SerializeField] bool isWater;
 
    [Space(10)] 
    [SerializeField] float dmg;
    [SerializeField] float bulletSpeed;
    [SerializeField] float amountOfHealthHeal;

    float timer=3;
    GameObject player;
    GameObject playerAttackPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<player>().gameObject;
        playerAttackPoint = FindObjectOfType<player>().attackPoint.gameObject;

        if (FindObjectOfType<player>().GetComponent<player>().isEarth && isEnemyBullet)
        {
            dmg /= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (behaveLikeNormalBullet)
        {
            transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
        }
        if (followPlyerAttack)
        {
            transform.position = player.transform.position;
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                gameObject.SetActive(false);
            }
        }
        if (followPlayerAttackPoint)
        {
            //transform.position = playerAttackPoint.transform.position + new Vector3(3,0,0);
            //transform.rotation = playerAttackPoint.transform.rotation;
            if (timer <= 0)
            {
                gameObject.SetActive(false);
                timer = 3;
            }
            else if (gameObject.activeSelf)
            {
                timer -= Time.deltaTime;
            }
        }
        if (spawnSomeWhereElse)
        {

        }

        //time to destory self
        if (timer <= 0 && !followPlayerAttackPoint && !followPlyerAttack)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isEnemyBullet)
        {
            collision.GetComponent<enemy>().health -= dmg;
            if (isWater)
            {
                FindObjectOfType<player>().GetComponent<player>().playerHealth += amountOfHealthHeal;
                if (FindObjectOfType<player>().GetComponent<player>().playerHealth >= 250)
                {
                    FindObjectOfType<player>().GetComponent<player>().playerHealth = 250;
                }
            }
            if (!penetrate)
            {
                Destroy(gameObject);
            }
        }
        if (isEnemyBullet)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<player>().playerHealth -= dmg;
                if (!penetrate)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (followPlyerAttack || followPlayerAttackPoint)
            {
                collision.GetComponent<enemy>().health -= dmg * Time.deltaTime;
            }
        }
    }

}
