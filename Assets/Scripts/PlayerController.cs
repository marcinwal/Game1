using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float jumpForce = 6f;
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
                //Debug.Log("Mouse clicked");
                Jump();
            }
            animator.SetBool("isGrounded", IsGrounded());
        }
	}


    void Jump()
    {
//        Debug.Log("jump inovkeed");
//        Debug.Log(IsGrounded());
        if (IsGrounded())
        {
            Debug.Log("jump force");
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

        if(PlayerPrefs.GetFloat("highscore",0) < this.GetDistance())
        {
            PlayerPrefs.SetFloat("highscore", this.GetDistance());
        }
    }

    public float GetDistance()
    {
        float travelDistance = Vector2.Distance(new Vector2(startingPosition.x, 0),
                                                new Vector2(this.transform.position.x, 0));
        return travelDistance;
    }
}
