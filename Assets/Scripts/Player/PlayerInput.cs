using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody2D rb;
    private float speed = 1.0f;
    public Animator animator;

    [Header("Attaking")]
    public PlayerStats player;
    public float castingCD = 3f;
    private float curAttackSpeed;
    public PlayerStats enemy;
    public CombatHelper helper;
    public TMP_Text playerDamage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player.health = player.maxHealth;
        player.mana = player.maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();

        Move();

        bool canMelee = Targeting.canAttack && AttackToggle.canAttack;
        Attack(canMelee);

        //DisplayDamage(enemyDamage, enemy);
    }

    private void CheckHealth()
    {
        helper.DisplayDamage(playerDamage, player);
        
        if(player.health <= 0)
        {
            StartCoroutine(helper.Death(animator));
            this.enabled = false;
        }
    }

    private void Attack(bool canAttack)
    {
        if(canAttack)
        {
            curAttackSpeed += Time.deltaTime;
            if (curAttackSpeed > player.attackSpeed)
            {
                animator.Play("Player_Melee_Ready");
                //Do Damage
                //if (animator is playing the melee animation, deal damage)
                {
                    helper.Damage(player, enemy);
                    //DisplayDamage(enemyDamage, enemy);
                }
         
                curAttackSpeed = 0;
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

        //Cancel casting if player moves
        if(moveVec.magnitude > 0)
        {
            Spells.magic = Spells.SpellTypes.None;
            Spells.castBar.value = 0;
        }
    }
}
