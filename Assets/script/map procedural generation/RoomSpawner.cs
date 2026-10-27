using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    Gamemanager gamemanager;

    public int openingDirection;
    //1 --> bottom door
    //2 --> top door
    //3 --> left door
    //4 --> right door

    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    float minimalAmountOfRoom;

    void Start()
    {
        gamemanager = FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>();

        minimalAmountOfRoom = gamemanager.minimalAmountOfRoom;

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        if (gamemanager.amountOfRooms < minimalAmountOfRoom)
        {
            Invoke("spawn", 0.1f);
        }
    }

    private void Update()
    {
        minimalAmountOfRoom = gamemanager.minimalAmountOfRoom;
        if (gamemanager.amountOfRooms < minimalAmountOfRoom)
        {
            Invoke("spawn", 0.1f);
        }
    }

    void spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                //open bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                if (gamemanager.amountOfRooms < 2)
                {
                    rand = Random.Range(3, 4);
                }
                if (rand == 6 && gamemanager.amountOfRooms <= minimalAmountOfRoom)
                {
                    rand = 3;
                }
                if (gamemanager.amountOfRooms > minimalAmountOfRoom)
                {
                    //rand = 6;
                }
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            /*else if (openingDirection == 2)
            {
                //open top door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }*/
            else if (openingDirection == 3)
            {
                rand = Random.Range(0, templates.leftRooms.Length);
                if (gamemanager.amountOfRooms > minimalAmountOfRoom)
                {
                    //rand = 0;
                }
                //open left door
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                //open right door
                rand = Random.Range(0, templates.rightRooms.Length);
                if (gamemanager.amountOfRooms > minimalAmountOfRoom)
                {
                    //rand = 0;
                }
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            gamemanager.amountOfRooms += 1;
            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomSpawner"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                //spawn just a block
                Instantiate(templates.closedRooms, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true; ;
        }
    }

}
