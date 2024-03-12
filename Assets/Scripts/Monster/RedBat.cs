using UnityEngine;

public enum RedBatState { Idle, Attack }

public class RedBat : Monster
{
    private RedBatState currentState;

    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange;

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
        }

    }

    private void RedBatAttackState()
    {
        /*Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);*/

        if (Vector2.Distance(target.position, transform.position) > attackRange)
        {
            currentState = RedBatState.Idle;
        }
    }

}
