using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] GameObject player;

    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 LookingDir = mousePos - GetComponent<Rigidbody2D>().position;
        float angle = Mathf.Atan2(LookingDir.y, LookingDir.x) * Mathf.Rad2Deg - 90f;
        GetComponent<Rigidbody2D>().rotation = angle;
    }
}
