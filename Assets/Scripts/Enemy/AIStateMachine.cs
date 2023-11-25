using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    public GameObject mage;
    public GameObject poisonSpit;
    public GameObject toxicSpit;
    public Transform spitSpawn;
    private float aggroRange = 5f;
    private bool aggroed = false;
    private Rigidbody2D rb;
    private float speed = 0.5f;
    private float attackRange = 3f;
    private int attackCount = 0;
    private float curAttackSpeed;
    public static bool enraged = false;

    private Animator animator;

    public CombatHelper helper;
    public PlayerStats player;
    public PlayerStats enemy;
    public TMP_Text enemyDamage;
    private float spellSpeed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy.health = enemy.maxHealth;
        enemyDamage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        Move();
    }

    private void Move()
    {
        Vector2 d = mage.transform.position - transform.position;

        aggroed = d.magnitude < aggroRange;

        if (aggroed)
        {
            rb.velocity = d * speed;
            Attack(d);
        }

        //animator.SetFloat("Horizontal", moveVec.x);
        //animator.SetFloat("Vertical", moveVec.y);
        //animator.SetFloat("Speed", moveVec.sqrMagnitude);//Optimized due to magnitude already being square rooted
    }

    private void Attack(Vector2 d)
    {
        if (d.magnitude < attackRange)
        {
            rb.velocity = new Vector2(0, 0);
            //Attack
            curAttackSpeed += Time.deltaTime;
            if (curAttackSpeed > enemy.attackSpeed)
            {
                animator.Play("EnemyAttack");
                //Do Damage
                //if (animator is playing the melee animation, deal damage)
                {
                    if (attackCount == 3)
                    {
                        //Start toxic split - cast

                        //Instantiate
                        SpitAttack(toxicSpit);

                        //Reset attack count
                        attackCount = 0;
                    }
                    else
                    {
                        SpitAttack(poisonSpit);
                        attackCount++;
                    }

                    print("Attack!");
                    //DisplayDamage(enemyDamage, enemy);
                }

                curAttackSpeed = 0;
            }
        }
    }

    private void SpitAttack(GameObject spit)
    {
        Vector2 dest = mage.transform.position - transform.position;
        GameObject s = Instantiate(spit, spitSpawn.position, Quaternion.identity);
        s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;
    }

    private void CheckHealth()
    {
        helper.DisplayDamage(enemyDamage, enemy);

        if(enemy.health < enemy.maxHealth/5)
        {
            //Enraged State
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            enraged = true;

        }
        if (enemy.health <= 0)
        {
            StartCoroutine(helper.Death(animator));
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            enraged = false;
            this.enabled = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
