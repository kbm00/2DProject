using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
     

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    [SerializeField] float brakePower;
    [SerializeField] float jumpSpeed;
    [SerializeField] LayerMask groundCheckLayer;

    private int groundCount;
    private Vector2 moveDir;
    private bool isGround;

    


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (moveDir.x < 0 && rigid.velocity.x > -maxXSpeed)
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower);
        }
        else if (moveDir.x > 0 && rigid.velocity.x < maxXSpeed)
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower);
        }
        else if (moveDir.x == 0 && rigid.velocity.x > 0)
        {
            rigid.AddForce(Vector2.left * brakePower);
        }
        else if (moveDir.x == 0 && rigid.velocity.x < 0)
        {
            rigid.AddForce(Vector2.right * brakePower);
        }

        if (rigid.velocity.y < -maxYSpeed)
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = -maxYSpeed;
            rigid.velocity = velocity;
        }
        
    }

    private void Jump()
    {
        Vector2 velocity = rigid.velocity;
        velocity.y = jumpSpeed;
        rigid.velocity = velocity;
    }
    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();

        if (moveDir.x < 0)
        {
            render.flipX = true;
            animator.SetBool("Run", true);

        }
        else if (moveDir.x > 0)
        {
            render.flipX = false;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed && isGround)
        {
            Jump();
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount++;
            isGround = groundCount != 0;
            animator.SetBool("IsGround", isGround);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount--;
            isGround = groundCount != 0;
            animator.SetBool("IsGround", isGround);
        }
    }
}
