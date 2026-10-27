using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment : MonoBehaviour
{

    [SerializeField] bool bomb;
    [SerializeField] bool spikes;

    [Space(10)]
    [SerializeField] float dmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && bomb)
        {
            if (!collision.GetComponent<player>().isFire && !collision.GetComponent<player>().isEarth)
            {
                collision.GetComponent<player>().playerHealth -= dmg;
                Destroy(gameObject);
            }
            if (collision.GetComponent<player>().isFire)
            {
                collision.GetComponent<player>().playerHealth += 10;
                if (collision.GetComponent<player>().playerHealth >= collision.GetComponent<player>().fireHealth)
                {
                    collision.GetComponent<player>().playerHealth = collision.GetComponent<player>().fireHealth;
                }
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Player") && spikes && collision.GetComponent<player>().isEarth == false)
        {
            collision.GetComponent<player>().playerHealth -= dmg;
        }
    }

}
