using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
 
    public Rigidbody2D rigidbody2d;
    public float speed;
    //public GameObject player;

    private bool isGameOver = false;
 
    void Update()
    {
        if(Input.GetAxis("Horizontal")>0) //positive
        {
            rigidbody2d.velocity = new Vector2(speed, 0f);
        }
        else if(Input.GetAxis("Horizontal")<0) //negative
        {
            rigidbody2d.velocity = new Vector2(-speed, 0f);
        }
        else if(Input.GetAxis("Vertical")>0) //positive
        {
            rigidbody2d.velocity = new Vector2(0f, speed);
        }
        else if(Input.GetAxis("Vertical")<0) //negative
        {
            rigidbody2d.velocity = new Vector2(0f, -speed);
        }
    }

}
