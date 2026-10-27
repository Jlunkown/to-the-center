using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    #region game objects
    [SerializeField] GameObject startingMap;
    [SerializeField] GameObject player;
    #endregion

    #region data
    //public
    public float amountOfRooms;
    public float amoutnOfRoomPlayerEnter;

    public float minimalAmountOfRoom;

    //private
    float endRoom;

    public float score;
    #endregion

    void Start()
    {

    }

    public void startGame()
    {
        Instantiate(startingMap, transform.position, Quaternion.identity);
    }

    void Update()
    {
        checkIfMapCreationIsDone();
    }

    void checkIfMapCreationIsDone()
    {
        endRoom = GameObject.FindGameObjectsWithTag("end").Length;
        if (endRoom > 0)
        {
            if (amountOfRooms <= 20)
            {
                /*for (int i = 0; i < GameObject.FindGameObjectsWithTag("isARoom").Length; i++)
                {
                    Destroy(GameObject.FindGameObjectWithTag("isARoom"));
                    amountOfRooms -= 1;
                }
                if (amountOfRooms <= 0)
                {
                    amountOfRooms = 0;
                }
                if (amountOfRooms == 0)
                {
                    Destroy(GameObject.FindGameObjectWithTag("end"));
                    Instantiate(startingMap, transform.position, Quaternion.identity);
                }*/
            }
            else
            {
                //player.SetActive(true);
            }
            
        }
    }

}
