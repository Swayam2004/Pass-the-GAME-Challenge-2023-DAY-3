using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private bool holdingUp, holdingDown, holdingLeft, holdingRight;

    private float vX, vY;
    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    //The sprite from the GFX child
    [SerializeField]
    private SpriteRenderer sprite;
    private GameManagerScript manager;

    private void Awake()
    {
        manager = (GameManagerScript)FindObjectOfType(typeof(GameManagerScript));
    }

    private void Update()
    {
        //Input logic
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            holdingUp = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            holdingDown = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            holdingLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            holdingRight = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            holdingUp = false;
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            holdingDown = false;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            holdingLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            holdingRight = false;
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("isWalking", rb.velocity != Vector2.zero);
        if(manager.playerCanMove)
            HandleMovement();

    }

    //Turning inputs into movement
    private void HandleMovement()
    {
        if (holdingLeft)
        {
            vX = -speed;
            sprite.flipX = true;
        }
        else if (holdingRight)
        {
            vX = speed;
            sprite.flipX = false;
        }
        else
            vX = 0;
        if (holdingUp)
            vY = speed;
        else if (holdingDown)
            vY = -speed;
        else
            vY = 0;

        rb.velocity = new Vector2(vX, vY);
    }
}
