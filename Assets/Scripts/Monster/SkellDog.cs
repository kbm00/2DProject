using UnityEngine;

public enum SkellDState { Idle, Trace }

public class SkellDog : Monster
{
    private SkellDState currentState;

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;
    [SerializeField] Animator animator;

    private Transform target;
    private Vector2 startPos;

    private void Start()
    {
        currentState = SkellDState.Idle;
        target = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private void Update()
    {

        switch (currentState)
        {
            case SkellDState.Idle:
                SkellDIdleState();
                break;

            case SkellDState.Trace:
                SkellDTraceState();
                break;
        }
    }

    private void SkellDIdleState()
    {
        animator.SetBool("Run", false);
        if (Vector2.Distance(target.position, transform.position) < findRange)
        {
            currentState = SkellDState.Trace;
        }
    }

    public void SkellDTraceState()
    {
        float moveDirection = (target.position.x > transform.position.x) ? 1f : -1f;
        Vector2 movement = new Vector2(moveDirection * moveSpeed * Time.deltaTime, 0f);
        transform.Translate(movement);
        animator.SetBool("Run", true);
        


        if (Vector2.Distance(target.position, transform.position) > findRange)
        {
            currentState = SkellDState.Idle;

        }
    }

}
