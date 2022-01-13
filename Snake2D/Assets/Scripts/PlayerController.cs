using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private List<Transform> snakeSegments;

    public Transform snakeBodyPrefab;

    //private float Xpos, Ypos;

    private bool canMoveRight, canMoveLeft, canMoveUp, canMoveDown;

    private FoodController _foodcontroller;

    private Transform segment;

    private void Awake()
    {
        canMoveRight= canMoveLeft= canMoveUp= canMoveDown = true;
    }
    private void Start()
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
        Debug.Log(snakeSegments.Count);
    }

    public Rigidbody2D rigidbody2d;
    public float speed;    

    //Get Keyboard inputs and set movement rules, rotate the snake head as well respectively
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && canMoveRight) //positive
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            rigidbody2d.velocity = new Vector2(speed, 0f);
            canMoveLeft = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        else if(Input.GetKeyDown(KeyCode.A) && canMoveLeft) //negative
        {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            transform.localRotation =  Quaternion.Euler(0, 0, 90);
            rigidbody2d.velocity = new Vector2(-speed, 0f);
            canMoveRight = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        else if(Input.GetKeyDown(KeyCode.W) && canMoveUp) //positive
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, speed);
            canMoveDown = false;
            canMoveLeft = true;
            canMoveRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown)  //negative
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(transform.localScale.x, (Mathf.Abs(transform.localScale.y)*-1), transform.localScale.z);
            rigidbody2d.velocity = new Vector2(0f, -speed);
            canMoveUp = false;
            canMoveLeft = true;
            canMoveRight = true;
        }        
    }

    //Snake tails following the last segment, shift the draw of each object to its previous object's position
    private void FixedUpdate()
    {
        for (int i = snakeSegments.Count - 1; i > 0; i--)
        {
            snakeSegments[i].position = snakeSegments[i - 1].position;
        }
    }

    //When Snake collides with Apple trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {
            //Destroy(collision.gameObject);
            _foodcontroller = collision.GetComponent<FoodController>();
            _foodcontroller.RandomizePosition();
            Grow();
        }
    }
    //Spawn segments of snake's tail
    private void Grow()
    {
        segment = Instantiate(this.snakeBodyPrefab);
        //segment.position = snakeSegments[snakeSegments.Count - 1].position;
        segment.position = new Vector2((snakeSegments[snakeSegments.Count - 1].position.x), (snakeSegments[snakeSegments.Count - 1].position.y));
        Debug.Log(segment.position);
        snakeSegments.Add(segment);
        Debug.Log(snakeSegments.Count);
    }

}
