using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{

    [SerializeField] GameObject cam;
    [SerializeField] GameObject[] enemy;

    [SerializeField] bool isTB;
    bool hasSpawnedEnemies;
    bool hasHealed;

    float randx;
    float randy;

    float minimalAmountOfChanceEnemy;
    float maximalAmountOfChanceEnemy = 4;

    float posXMax;
    float posXMin;
    float posYMax;
    float posYMin;

    Gamemanager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>().gameObject;
        posXMax = transform.position.x + 5f;
        posXMin = transform.position.x - 5f;
        posYMax = transform.position.y + 5f;
        posYMin = transform.position.y - 5f;

        gamemanager = FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gamemanager.amoutnOfRoomPlayerEnter < 5)
        {
            minimalAmountOfChanceEnemy = -3;
        }
        else if (gamemanager.amoutnOfRoomPlayerEnter < 10)
        {
            minimalAmountOfChanceEnemy = -2;
        }
        else if (gamemanager.amoutnOfRoomPlayerEnter < 15)
        {
            minimalAmountOfChanceEnemy = -1;
        }
        else if (gamemanager.amoutnOfRoomPlayerEnter < 20)
        {
            minimalAmountOfChanceEnemy = 0;
        }
        else if (gamemanager.amoutnOfRoomPlayerEnter < 25)
        {
            minimalAmountOfChanceEnemy = 1;
            maximalAmountOfChanceEnemy = 4;
        }
        else if (gamemanager.amoutnOfRoomPlayerEnter < 30)
        {
            minimalAmountOfChanceEnemy = 2;
            maximalAmountOfChanceEnemy = 5;
        }
        else if (gamemanager.amoutnOfRoomPlayerEnter < 35)
        {
            minimalAmountOfChanceEnemy = 3;
            maximalAmountOfChanceEnemy = 6;
        }
        else
        {
            minimalAmountOfChanceEnemy = 4;
            maximalAmountOfChanceEnemy = 7;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float randEnemy;
        Vector3 plus = new Vector3(0, 0, -10);

        if (collision.CompareTag("Player"))
        {
            //cam.transform.position = gameObject.transform.position + plus;
            if (!hasHealed)
            {
                collision.GetComponent<player>().playerHealth += 30;
                if (FindObjectOfType<player>().GetComponent<player>().isFire)
                {
                    if (FindObjectOfType<player>().GetComponent<player>().playerHealth >= FindObjectOfType<player>().GetComponent<player>().fireHealth)
                    {
                        FindObjectOfType<player>().GetComponent<player>().playerHealth = FindObjectOfType<player>().GetComponent<player>().fireHealth;
                    }
                }
                if (FindObjectOfType<player>().GetComponent<player>().isWater)
                {
                    if (FindObjectOfType<player>().GetComponent<player>().playerHealth >= FindObjectOfType<player>().GetComponent<player>().waterHealth)
                    {
                        FindObjectOfType<player>().GetComponent<player>().playerHealth = FindObjectOfType<player>().GetComponent<player>().waterHealth;
                    }
                }
                if (FindObjectOfType<player>().GetComponent<player>().isWind)
                {
                    if (FindObjectOfType<player>().GetComponent<player>().playerHealth >= FindObjectOfType<player>().GetComponent<player>().windHealth)
                    {
                        FindObjectOfType<player>().GetComponent<player>().playerHealth = FindObjectOfType<player>().GetComponent<player>().windHealth;
                    }
                }
                if (FindObjectOfType<player>().GetComponent<player>().isEarth)
                {
                    if (FindObjectOfType<player>().GetComponent<player>().playerHealth >= FindObjectOfType<player>().GetComponent<player>().earthHealth)
                    {
                        FindObjectOfType<player>().GetComponent<player>().playerHealth = FindObjectOfType<player>().GetComponent<player>().earthHealth;
                    }
                }
                gamemanager.amoutnOfRoomPlayerEnter += 1;
                gamemanager.minimalAmountOfRoom += 1;
                hasHealed = true;
            }

            if (!isTB && !hasSpawnedEnemies)
            {
                randEnemy = Random.Range(minimalAmountOfChanceEnemy, maximalAmountOfChanceEnemy);
                if (randEnemy <= 0)
                {
                    randEnemy = 0;
                }
                if (gamemanager.amoutnOfRoomPlayerEnter <= 1)
                {
                    randEnemy = 0;
                }

                for (int i = 0; i < randEnemy; i++)
                {
                    randx = Random.Range(posXMin, posXMax);
                    randy = Random.Range(posYMin, posYMax);
                    Instantiate(enemy[Random.Range(0, enemy.Length)], new Vector2(randx, randy), transform.rotation);
                }


                hasSpawnedEnemies = true;
            }
        }
    }

}
