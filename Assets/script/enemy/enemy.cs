using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class enemy : MonoBehaviour
{

    #region enemy stats
    public float StartHealth;
    [HideInInspector] public float health;
    [SerializeField] float speed;
    [SerializeField] float stoppingDistance;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject atkPoint;
    [SerializeField] float atkRate;
    float nextTimeToAttack;
    #endregion

    #region to make things work
    [Space(10)]
    [SerializeField] bool sepukuEnemy;

    GameObject target;
    Rigidbody2D enemyrb;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region getting stuff
        enemyrb = gameObject.GetComponent<Rigidbody2D>();
        target = FindObjectOfType<player>().gameObject;
        health = StartHealth;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        moving();

        #region died
        if (health <= 0)
        {
            FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>().score += 1;
            Destroy(gameObject);
        }
        #endregion
    }

    void moving()
    {
        //moving
        if (!sepukuEnemy)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, target.transform.position) <= stoppingDistance)
            {
                if (nextTimeToAttack <= 0)
                {
                    Instantiate(bullet, atkPoint.transform.position, transform.rotation);
                    nextTimeToAttack = atkRate;
                }
            }
            if (nextTimeToAttack > 0)
            {
                nextTimeToAttack -= Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, target.transform.position) > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            /*if (Vector2.Distance(transform.position, target.transform.position) <= stoppingDistance)
            {
                if (nextTimeToAttack <= 0)
                {
                    Instantiate(bullet, atkPoint.transform.position, transform.rotation);
                    nextTimeToAttack = atkRate;
                }
            }
            if (nextTimeToAttack > 0)
            {
                nextTimeToAttack -= Time.deltaTime;
            }*/
        }

        //rotating
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        enemyrb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           if (sepukuEnemy)
           {
                collision.GetComponent<player>().playerHealth -= 20;
                Destroy(gameObject);
           }
        }
    }

}

