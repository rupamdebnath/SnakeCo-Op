using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 _direction;

    public float speed = 3f;

    public Rigidbody2D rigidbody;

    private bool canMoveRight, canMoveLeft, canMoveUp, canMoveDown;

    private void Awake()
    {
        canMoveRight = canMoveLeft = canMoveUp = canMoveDown = true;
    }

    //Movement for Snake
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canMoveUp)
        {
            _direction = Vector2.up;            
            canMoveDown = false;
            canMoveLeft = true;
            canMoveRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMoveDown)
        {
            _direction = Vector2.down;
            canMoveUp = false;
            canMoveLeft = true;
            canMoveRight = true;
        }
        if (Input.GetKeyDown(KeyCode.A) && canMoveLeft)
        {
            _direction = Vector2.left;
            canMoveRight = false;
            canMoveUp = true;
            canMoveDown = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && canMoveRight)
        {
            _direction = Vector2.right;
            canMoveLeft = false;
            canMoveUp = true;
            canMoveDown = true;            
        }
    }

    private void FixedUpdate()
    {
        //this.transform.position = new Vector3(this.transform.position.x + (_direction.x * speed), (this.transform.position.y + (_direction.y * speed)), 0.0f);
        rigidbody.velocity = new Vector2(speed, 0f);
        //Debug.Log(_direction);

        //transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);

        //transform.Rotate(Vector3.forward * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Apple")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Wall")
        {
            //_direction = new Vector2(0,0);
            //this.transform.position = this.transform.position;
        }
    }

}
