using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AttackToggle : MonoBehaviour
{
    public Targeting target;
    public Transform player;
    private Transform enemy;
    private int range = 2;
    private float timer = 2f;
    public static bool canAttack = false;
    public Animator animator;
    private Vector2 distance;

    // Start is called before the first frame update
    void Start()
    {
        enemy = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            gameObject.GetComponent<Image>().color = Color.white;
            timer = 2f;
        }

        distance = enemy.position - player.position;

        animator.SetFloat("TargetX", distance.x);
        animator.SetFloat("TargetY", distance.y);
    }

    public void Attack()
    {
        //distance = enemy.position - player.position;

        if (distance.magnitude < range)
        {
            //Can attack so make button green and Instantiate a fireball
            gameObject.GetComponent<Image>().color = Color.green;
            canAttack = !canAttack;
        }
        else
        {
            //Cant attack so make button red
            gameObject.GetComponent<Image>().color = Color.red;
            canAttack = false;
        }
        //return canAttack;
    }
}
