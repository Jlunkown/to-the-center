using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{

    player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<player>().GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls"))
        {
            player.canJump = true;
        }
    }

}
