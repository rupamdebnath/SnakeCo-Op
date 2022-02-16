﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private List<Transform> snakeSegments;

    public Transform snakeBodyPrefab;

    public ScoreController scoreController;

    //private float Xpos, Ypos;

    private bool canMoveRight, canMoveLeft, canMoveUp, canMoveDown;

    private FoodController _foodcontroller;

    private Transform segment;

    private Vector3 oldPosition;
    public BoxCollider2D grid;

    private void Awake()
    {
        canMoveRight= canMoveLeft= canMoveUp= canMoveDown = true;
    }
    private void Start()
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
        //Debug.Log(snakeSegments.Count);
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
        oldPosition = transform.position;
        //Debug.Log("Old POsition:" + oldPosition);
        if (oldPosition.x < -22 || oldPosition.x > 22 || oldPosition.y < -14 || oldPosition.y > 14)
        {
            if (oldPosition.x < 0)
            {
                oldPosition.x = oldPosition.x + 1;
            }
            else if (oldPosition.x > 0)
            {
                oldPosition.x = oldPosition.x - 1;
            }
            if (oldPosition.y < 0)
            {
                oldPosition.y = oldPosition.y + 1;
            }
            else if (oldPosition.y > 0)
                {
                oldPosition.y = oldPosition.y - 1;
            }
            transform.position = new Vector2(-oldPosition.x, -oldPosition.y);
            //Debug.Log("New POsition:" + transform.position);
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
        //Vector3 oldPosition = transform.position;
        if (collision.tag == "Apple")
        {
            //Debug.Log("Print apple");
            _foodcontroller = collision.GetComponent<FoodController>();
            _foodcontroller.RandomizePosition();
            scoreController.IncreaseScore(10);
            Grow();
        }
        else if (collision.tag == "Body")
        {
            //Death for the player
            Die();
        }

    }

    private void Die()
    {
        rigidbody2d.velocity = Vector2.zero;
        gameObject.GetComponent<PlayerController>().enabled = false;               
    }

    private void Grow()
    {
        segment = Instantiate(this.snakeBodyPrefab);        
        segment.position = new Vector2((snakeSegments[snakeSegments.Count - 1].position.x), (snakeSegments[snakeSegments.Count - 1].position.y));
        snakeSegments.Add(segment);
    }

}
