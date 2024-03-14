using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform weaponPoint;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] Animator animator;

    [Header("Prefabs")]
    [SerializeField] GameObject bulletPrefab;

    private bool isAttacking = false;
    private int currentWeaponIndex = 0;

    void Update()
    {
        PlayerFlip();
        WeaponRotate();
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            ActivateWeapon(0); // 첫 번째 무기 활성화
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            ActivateWeapon(1); // 두 번째 무기 활성화
        }  
    }

    private void ActivateWeapon(int weaponIndex)
    {
        if (weaponIndex < weapons.Length)
        {
            weapons[currentWeaponIndex].SetActive(false); 
            weapons[weaponIndex].SetActive(true); 
            currentWeaponIndex = weaponIndex; 
        }
    }



    private void PlayerFlip() // 플레이어가 마우스 위치에 따라 좌우로 바라보도록 적용
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < playerTransform.position.x)
        {
            playerTransform.localScale = new Vector2(-1, 1);
        }
        else
        {
            playerTransform.localScale = new Vector2(1, 1);
        }
    }

    private void WeaponRotate()  // 마우스 위치를 따라가는 무기 적용
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        Vector2 direction = mouseWorldPosition - weaponPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerTransform.localScale.x < 0)
        {
            angle -= 180f;
        }
        weaponPoint.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Fire()
    {


        // 현재 활성화된 무기가 첫 번째 무기일 때만 총알 발사
        if (currentWeaponIndex == 0)
        {
            GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 플레이어가 왼쪽을 바라보고 있을 때
            if (playerTransform.localScale.x < 0)
            {
                // 발사체의 회전을 조정하여 왼쪽으로
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 180);
            }
            else
            {
                // 플레이어가 오른쪽을 바라보고 있을 때 발사체의 정상 회전을 유지
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            rb.velocity = direction * bulletSpeed;
        }
    }
    private void OnAttack(InputValue value)
    {
        Fire(); // 총을 사용하는 경우

        if (currentWeaponIndex == 1 && !isAttacking) // 1번 인덱스인 검일 때만
        {
            isAttacking = true;
            animator.SetBool("isSwing", true);
            StartCoroutine(ResetSwordAnimation());
        }
    }

    IEnumerator ResetSwordAnimation()
    {
        yield return new WaitForSeconds(0.5f); // 애니메이션 연속 방지 
        animator.SetBool("isSwing", false);
        isAttacking = false;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // 발사체 생성
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z축 값 무시

        Vector2 fireDirection = mousePosition - firePoint.position;
        float fireAngle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        // 발사체의 회전을 설정 playerTransform의 반전 상태에 상관없이 작동
        projectile.transform.rotation = Quaternion.Euler(0, 0, fireAngle);

        // 발사체가 마우스를 향해 정확히 날아가도록 속도를 설정
        rb.velocity = fireDirection.normalized * bulletSpeed;

    }



}

