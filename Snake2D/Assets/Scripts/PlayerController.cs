using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private List<Transform> snakeSegments;

    public Transform snakeBodyPrefab;

    private float Xpos, Ypos;

    private FoodController _foodcontroller;
      
    private void Start()
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
        Debug.Log(snakeSegments.Count);
    }

    public Rigidbody2D rigidbody2d;
    public float speed;    

    void Update()
    {
        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            snakeSegments[i].position = new Vector3((snakeSegments[i - 1].position.x + Xpos), (snakeSegments[i - 1].position.y + Ypos), snakeSegments[i - 1].position.z);
        }

        if (Input.GetKeyDown(KeyCode.D)) //positive
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            rigidbody2d.velocity = new Vector2(speed, 0f);
            Xpos = -0.4f;
            Ypos = 0;
        }
        else if(Input.GetKeyDown(KeyCode.A)) //negative
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation =  Quaternion.Euler(0, 0, 90);
            rigidbody2d.velocity = new Vector2(-speed, 0f);
            Xpos = 0.4f;
            Ypos = 0;
        }
        else if(Input.GetKeyDown(KeyCode.W)) //positive
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, speed);
            Ypos = -0.4f;
            Xpos = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S))  //negative
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, (Mathf.Abs(transform.localScale.y)*-1), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, -speed);
            Ypos = 0.4f;
            Xpos = 0;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {
            //Destroy(collision.gameObject);
            _foodcontroller = collision.GetComponent<FoodController>();
            _foodcontroller.RandomizePosition();

            Grow();

            //Instantiate(snakeBodyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.snakeBodyPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;

        snakeSegments.Add(segment);
        Debug.Log(snakeSegments.Count);
    }

    private void ReturnPosition()
    {

    }

}
