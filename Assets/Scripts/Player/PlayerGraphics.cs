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
            ActivateWeapon(0); // ù ��° ���� Ȱ��ȭ
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            ActivateWeapon(1); // �� ��° ���� Ȱ��ȭ
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



    private void PlayerFlip() // �÷��̾ ���콺 ��ġ�� ���� �¿�� �ٶ󺸵��� ����
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

    private void WeaponRotate()  // ���콺 ��ġ�� ���󰡴� ���� ����
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


        // ���� Ȱ��ȭ�� ���Ⱑ ù ��° ������ ���� �Ѿ� �߻�
        if (currentWeaponIndex == 0)
        {
            GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // �÷��̾ ������ �ٶ󺸰� ���� ��
            if (playerTransform.localScale.x < 0)
            {
                // �߻�ü�� ȸ���� �����Ͽ� ��������
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 180);
            }
            else
            {
                // �÷��̾ �������� �ٶ󺸰� ���� �� �߻�ü�� ���� ȸ���� ����
                projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            rb.velocity = direction * bulletSpeed;
        }
    }
    private void OnAttack(InputValue value)
    {
        Fire(); // ���� ����ϴ� ���

        if (currentWeaponIndex == 1 && !isAttacking) // 1�� �ε����� ���� ����
        {
            isAttacking = true;
            animator.SetBool("isSwing", true);
            StartCoroutine(ResetSwordAnimation());
        }
    }

    IEnumerator ResetSwordAnimation()
    {
        yield return new WaitForSeconds(0.5f); // �ִϸ��̼� ���� ���� 
        animator.SetBool("isSwing", false);
        isAttacking = false;
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // �߻�ü ����
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z�� �� ����

        Vector2 fireDirection = mousePosition - firePoint.position;
        float fireAngle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        // �߻�ü�� ȸ���� ���� playerTransform�� ���� ���¿� ������� �۵�
        projectile.transform.rotation = Quaternion.Euler(0, 0, fireAngle);

        // �߻�ü�� ���콺�� ���� ��Ȯ�� ���ư����� �ӵ��� ����
        rb.velocity = fireDirection.normalized * bulletSpeed;

    }



}

