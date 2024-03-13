using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamagable
{
    [Header("Effect")]



    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Transform tf;
    [SerializeField] PhysicsMaterial2D lowMaterial;
    [SerializeField] PhysicsMaterial2D highMaterial;

    [Header("PlayerState")]
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    [SerializeField] float brakePower;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] int hp;


    [Header("Check")]
    [SerializeField] LayerMask groundCheckLayer;

    private int groundCount;
    private Vector2 moveDir;
    private bool isGround;
    private bool isDashing;
    private float dashTimeLeft;

    private void FixedUpdate()
    {
        Move();
        SlopeChecker();
       
    }
    void Start()
    {
        tf = transform;
          
    }

    void Update()
    {
        
    }

    private void Move()
    {
        float slopeAngles = Slope();
        float slopeFactor = Mathf.Max(2, 1 + slopeAngles / 15f); // 경사 각도에 따라 이동 힘 보정

        Vector2 moveForce = moveDir * movePower * slopeFactor;

        // 경사면에서 수직 힘 추가
        if (slopeAngles > 0)
        {
            moveForce += Vector2.up * Mathf.Abs(moveDir.x) * movePower * Mathf.Sin(slopeAngles * Mathf.Deg2Rad);
        }

        if (Mathf.Abs(rigid.velocity.x) < maxXSpeed)
        {
            rigid.AddForce(moveForce);
        }

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

    private bool CheckIfOnSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, groundCheckLayer);
        if (hit.collider != null)
        {
            float angle = Vector2.Angle(hit.normal, Vector2.up);
            if (angle > 0 && angle <= 50  ) // 경사면 감지
            {
                return true;
            }
        }
        return false;
    }

    private void SlopeChecker()
    {
        bool isOnSlope = CheckIfOnSlope();
        if (isOnSlope && Mathf.Approximately(rigid.velocity.x, 0f))
        {
            playerCollider.sharedMaterial = highMaterial;
            Debug.Log("Check");
        }
        else
        {
            playerCollider.sharedMaterial = lowMaterial;
        }

         playerCollider.enabled = false;
         playerCollider.enabled = true;
    }

    private float Slope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundCheckLayer);
        if (hit)
        {
            return Vector2.Angle(hit.normal, Vector2.up);
        }
        return 0;
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
    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

}
