using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] Transform tf;
    [SerializeField] Collider2D playerCollider;

    [Header("Property")]
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    [SerializeField] float brakePower;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] LayerMask groundCheckLayer;
    

    private int groundCount;
    private Vector2 moveDir;
    private bool isGround;
    private bool isDashing;
    private float dashTimeLeft;


    private void FixedUpdate()
    {
        Move();
    }



    void Start()
    {
        tf = transform;
    }


    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        if (target.x < tf.position.x)
        {
            tf.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            tf.localScale = new Vector3(1, 1, 1);
        }
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

    private void Dash()
    {
        StartCoroutine(Dashing());
    }




    private IEnumerator Dashing()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dashDirection = (mousePosition - transform.position).normalized;

        isDashing = true;
        dashTimeLeft = dashTime;
        rigid.velocity = dashDirection * dashSpeed * movePower;
          
        playerCollider.enabled = false;
        yield return new WaitForSeconds(dashTime);
        playerCollider.enabled = true;
       
        isDashing = false;

    }
    private void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();

        if (moveDir.x < 0)
        {

            animator.SetBool("Run", true);

        }
        else if (moveDir.x > 0)
        {

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

    private void OnDash(InputValue value)
    {
        if (value.isPressed && !isDashing)
        {
            Dash();
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
