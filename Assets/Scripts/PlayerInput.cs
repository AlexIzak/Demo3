using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;
    private float speed = 1.0f;
    public Animator animator;

    [Header("Attaking")]
    public GameObject spell;

    public float castingCD;
    private float curCastingCD;

    public float spellSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Attack(Targeting.canAttack);
    }

    private void Attack(bool canAttack)
    {
        curCastingCD += Time.deltaTime;
        if (curCastingCD > castingCD)
        {
            if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal_2")) > 0 || Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical_2")) > 0)
            {
                GameObject s = Instantiate(spell, transform.position, Quaternion.identity);
                s.GetComponent<Rigidbody2D>().velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal_2"), CrossPlatformInputManager.GetAxis("Vertical_2")).normalized * spellSpeed;

                curCastingCD = 0;
            }
        }
    }

    private void Move()
    {
        Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));

        if (moveVec.magnitude > 1)
        {
            moveVec = moveVec.normalized;
        }

        rb.velocity = moveVec * speed;

        animator.SetFloat("Horizontal", moveVec.x);
        animator.SetFloat("Vertical", moveVec.y);
        animator.SetFloat("Speed", moveVec.sqrMagnitude);//Optimized due to magnitude already being square rooted

        //Camera follow
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
