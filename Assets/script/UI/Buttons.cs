using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    Gamemanager gamemanager;
    [SerializeField] player player;
    [SerializeField] GameObject Player;
    [SerializeField] bool fire;
    [SerializeField] bool water;
    [SerializeField] bool wind;
    [SerializeField] bool earth;

    [Space(10)]
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject choiceScreen;
    [SerializeField] GameObject playerUI;


    // Start is called before the first frame update
    void Start()
    {
        gamemanager = FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        gamemanager.startGame();
        choiceScreen.SetActive(true);
        startScreen.SetActive(false);
    }

    public void chooseElement()
    {
        if (fire)
        {
            Player.SetActive(true);
            player.isFire = true;
            player.playerHealth = player.fireHealth;
        }
        if (water)
        {
            Player.SetActive(true);
            player.isWater = true;
            player.playerHealth = player.waterHealth;
        }
        if (wind)
        {
            Player.SetActive(true);
            player.isWind = true;
            player.playerHealth = player.windHealth;
        }
        if (earth)
        {
            Player.SetActive(true);
            player.isEarth = true;
            player.playerHealth = player.earthHealth;
        }
        playerUI.SetActive(true);
        choiceScreen.SetActive(false);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

}
