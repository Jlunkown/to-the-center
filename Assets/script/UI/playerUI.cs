using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class playerUI : MonoBehaviour
{
    [SerializeField] player player;
    [SerializeField] enemy enemy;
    [SerializeField] Slider slider;

    [Space(10)]
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    [Space(10)]
    [SerializeField] bool isSlider;
    [SerializeField] bool enemyHealthBar;

    [Space(10)]
    [SerializeField] bool mustAlwaysStayUP;

    [Space(10)]
    public Slider sliderCD;
    [SerializeField] bool isCoolDown;
    [SerializeField] bool isSmallCD;
    [SerializeField] bool isMediumCD;
    [SerializeField] bool isHardCD;

    [Space(10)]
    [SerializeField] Sprite fireSmall;
    [SerializeField] Sprite fireMed;
    [SerializeField] Sprite fireBig;

    [SerializeField] Sprite windSmall;
    [SerializeField] Sprite windMed;
    [SerializeField] Sprite windBig;

    [SerializeField] Sprite EarthSmall;
    [SerializeField] Sprite EarthMed;
    [SerializeField] Sprite EarthBig;

    [SerializeField] Sprite waterSmall;
    [SerializeField] Sprite waterMed;
    [SerializeField] Sprite waterBig;

    Image image;

    [HideInInspector] public bool fire;
    [HideInInspector] public bool water;
    [HideInInspector] public bool wind;
    [HideInInspector] public bool earth;

    [Space(10)]
    [SerializeField] bool isRestartButton;

    [Space(10)]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool score;

    [Space(10)]
    [SerializeField] bool isAudio;
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlider)
        {
            if (player.isFire)
            {
                slider.maxValue = player.fireHealth;
            }
            if (player.isWater)
            {
                slider.maxValue = player.waterHealth;
            }
            if (player.isEarth)
            {
                slider.maxValue = player.earthHealth;
            }
            if (player.isWind)
            {
                slider.maxValue = player.windHealth;
            }
            slider.value = player.playerHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        if (enemyHealthBar)
        {
            slider.maxValue = enemy.StartHealth;
            slider.value = enemy.health;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        if (isCoolDown)
        {
            if (isSmallCD)
            {
                if (fire)
                {
                    image.sprite = fireSmall;
                }
                if (water)
                {
                    image.sprite = waterSmall;
                }
                if (wind)
                {
                    image.sprite = windSmall;
                }
                if (earth)
                {
                    image.sprite = EarthSmall;
                }
            }
            if (isMediumCD)
            {
                if (fire)
                {
                    image.sprite = fireMed;
                }
                if (water)
                {
                    image.sprite = waterMed;
                }
                if (wind)
                {
                    image.sprite = windMed;
                }
                if (earth)
                {
                    image.sprite = EarthMed;
                }
            }
            if (isHardCD)
            {
                if (fire)
                {
                    image.sprite = fireBig;
                }
                if (water)
                {
                    image.sprite = waterBig;
                }
                if (wind)
                {
                    image.sprite = windBig;
                }
                if (earth)
                {
                    image.sprite = EarthBig;
                }
            }
        }

        if (mustAlwaysStayUP)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (isRestartButton)
        {
            SceneManager.LoadScene(0);
        }

        if (score)
        {
            scoreText.text = "Final score = " + FindObjectOfType<Gamemanager>().GetComponent<Gamemanager>().amoutnOfRoomPlayerEnter;
        }

        if (isAudio)
        {
            //FindObjectOfType<AudioSource>().GetComponent<AudioSource>().volume = volumeSlider.value;
            audioSource.volume = volumeSlider.value;
        }
    }
}
