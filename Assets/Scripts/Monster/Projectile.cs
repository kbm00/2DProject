using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 moveDirection;
    private float Speed = 25;
    public int damage = 10;

    // 발사될 때 한 번 호출
    public void Launch(Vector2 direction, float speed)
    {
        moveDirection = direction;
        Speed = speed;

        GetComponent<Rigidbody2D>().velocity = moveDirection * Speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable hitObject = collision.GetComponent<IDamagable>();
        if (hitObject != null)
        {
            hitObject.TakeDamage(damage);
            Destroy(gameObject); 


        }
    }
}
