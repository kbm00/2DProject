using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(attackDamage);
        }

        // 충돌한 오브젝트가 벽이나 몬스터인지 확인
        if (collision.CompareTag("Wall") || collision.CompareTag("Monster"))
        {
            // 총알 비활성화
            gameObject.SetActive(false);
        }
    }
    

}
