using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AIStateMachine : MonoBehaviour
{

    public GameObject player;
    private float aggroRange = 3f;
    private bool aggroed = false;
    private Rigidbody2D rb;
    private float speed = 0.5f;
    private float attackRange = 2f;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 d = player.transform.position - transform.position;

        aggroed = d.magnitude < aggroRange;

        if (aggroed)
        {
            rb.velocity = d * speed;
        }

        //animator.SetFloat("Horizontal", moveVec.x);
        //animator.SetFloat("Vertical", moveVec.y);
        //animator.SetFloat("Speed", moveVec.sqrMagnitude);//Optimized due to magnitude already being square rooted
    }
}
