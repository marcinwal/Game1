using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 25f;
    public float runningSpeed = 1.5f;
    private Rigidbody2D rigidBody;
    public static PlayerController instance;
    private Vector3 startingPosition;

    public LayerMask groundLayer;
    public Animator animator;

    private void Awake()
    {
        instance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        startingPosition = this.transform.position;
    }
    // Use this for initialization
    public void StartGame () {
        animator.SetBool("isAlive", true);
        this.transform.position = startingPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
            animator.SetBool("isGrounded", IsGrounded());
        }
	}


    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }
        }
    }

    bool IsGrounded()
    {
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, groundLayer.value))
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void Kill()
    {
        GameManager.instance.GameOver();
        animator.SetBool("isAlive", false);
    }
}
