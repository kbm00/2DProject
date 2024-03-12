using System.Collections;
using UnityEngine;

public enum RedBatState { Idle, Attack }

public class RedBat : Monster
{
    private RedBatState currentState;

    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float attackDelay;

    private Coroutine attackRoutine;
    private Transform target;
    private Vector2 startPos;

    private void Start()
    {
        currentState = RedBatState.Idle;
        target = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private void Update()
    {

        switch (currentState)
        {
            case RedBatState.Idle:
                RedBatIdleState();
                break;

            case RedBatState.Attack:
                RedBatAttackState();
                break;
        }
    }

    private void RedBatIdleState()
    {
        if (Vector2.Distance(target.position, transform.position) < attackRange)
        {
            currentState = RedBatState.Attack;
            attackRoutine = StartCoroutine(AttackRoutine());
        }

    }

    private void RedBatAttackState()
    {
        

        if (Vector2.Distance(target.position, transform.position) > attackRange)
        {
            currentState = RedBatState.Idle;
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                attackRoutine = null;
            }
            else
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Vector2 attackDirection = (target.position - transform.position).normalized;
                projectile.GetComponent<Projectile>().Launch(attackDirection, moveSpeed);
            }
        }
        
    }

    private IEnumerator AttackRoutine()
    {
        while (currentState == RedBatState.Attack)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 attackDirection = (target.position - transform.position).normalized;
            projectile.GetComponent<Projectile>().Launch(attackDirection, projectileSpeed);

            yield return new WaitForSeconds(attackDelay);
        }
    }

}
