using UnityEngine;

public enum BatState { Idle, Trace }

public class Bat : Monster
{
    private BatState currentState;

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private Transform target;
    private Vector2 startPos;

    private void Start()
    {
        currentState = BatState.Idle;
        target = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private void Update()
    {

        switch (currentState)
        {
            case BatState.Idle:
                BatIdleState();
                break;

            case BatState.Trace:
                BatTraceState();
                break;
        }
    }

    private void BatIdleState()
    {
        if (Vector2.Distance(target.position, transform.position) < findRange)
        {
            currentState = BatState.Trace;
        }

    }

    private void BatTraceState()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        if (Vector2.Distance(target.position, transform.position) > findRange)
        {
            currentState = BatState.Idle;
        }
    }

}






