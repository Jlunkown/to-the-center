using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRooms;

    public List<GameObject> rooms;

    public float waitTime;
    bool spawnBoss;
    bool spawnStair;
    public GameObject boss;
    public GameObject stair;

    void Start()
    {

    }

    void Update()
    {
        #region random stuff 
        /*if (waitTime <= 0 && spawnBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }*/
        #endregion
    }
}

