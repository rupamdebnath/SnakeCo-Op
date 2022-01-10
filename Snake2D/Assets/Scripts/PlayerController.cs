using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private List<Transform> snakeSegments;

    public Transform snakeBodyPrefab;

    private float Xpos, Ypos;

    private bool canMoveRight, canMoveLeft, canMoveUp, canMoveDown = true;

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
        //for (int i = snakeSegments.Count - 1; i > 0; i--)
        //{
        //    snakeSegments[i].position = new Vector3((snakeSegments[i - 1].position.x + Xpos), (snakeSegments[i - 1].position.y + Ypos), snakeSegments[i - 1].position.z);
        //}

        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            snakeSegments[i].position = snakeSegments[i-1].position;
        }

            if (Input.GetKeyDown(KeyCode.D) && canMoveRight) //positive
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            rigidbody2d.velocity = new Vector2(speed, 0f);
            Xpos = -0.4f;
            Ypos = 0;
            canMoveLeft = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        else if(Input.GetKeyDown(KeyCode.A) && canMoveLeft) //negative
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation =  Quaternion.Euler(0, 0, 90);
            rigidbody2d.velocity = new Vector2(-speed, 0f);
            Xpos = 0.4f;
            Ypos = 0;
            canMoveRight = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        else if(Input.GetKeyDown(KeyCode.W) && canMoveUp) //positive
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, speed);
            Ypos = -0.4f;
            Xpos = 0;
            canMoveDown = false;
            canMoveLeft = true;
            canMoveRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown)  //negative
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, (Mathf.Abs(transform.localScale.y)*-1), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, -speed);
            Ypos = 0.4f;
            Xpos = 0;
            canMoveUp = false;
            canMoveLeft = true;
            canMoveRight = true;
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
        //segment.position = snakeSegments[snakeSegments.Count - 1].position;

        //segment.position = new Vector3((snakeSegments[snakeSegments.Count - 1].position.x + Xpos), (snakeSegments[snakeSegments.Count - 1].position.y + Ypos), snakeSegments[snakeSegments.Count - 1].position.z);
        segment.position = new Vector3(0, 0, 0);
        snakeSegments.Add(segment);
        Debug.Log(snakeSegments.Count);
    }

}
