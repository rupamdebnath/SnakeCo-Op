using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public GameObject snakeBody;
    Vector3 originalAngles;
    private void Awake()
    {
        originalAngles = gameObject.transform.eulerAngles;
    }

    private void Start()
    {
        //Debug.Log("Test statements on Start");
    }

    public Rigidbody2D rigidbody2d;
    public float speed;
    //public GameObject player;

    private bool isGameOver = false;
 
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.D)) //positive
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            rigidbody2d.velocity = new Vector2(speed, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.A)) //negative
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
            rigidbody2d.velocity = new Vector2(-speed, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.W)) //positive
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, speed);
        }
        else if (Input.GetKeyDown(KeyCode.S))  //negative
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, (Mathf.Abs(transform.localScale.y)*-1), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, -speed);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {
            Destroy(collision.gameObject);
        }
    }

}
