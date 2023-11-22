using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissile : MonoBehaviour
{
    private float spellDamage = 15f;
    private float buffDuration = 10f;
    private float delay = 1f;
    public PlayerStats enemy;
    public CombatHelper helper;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            //Hit chance
            float hitChance = UnityEngine.Random.Range(1, 101);

            if (hitChance > 20)
            {
                //Caculate damage if we hit
                float damage = UnityEngine.Random.Range(spellDamage * 0.75f, spellDamage * 1.25f) * enemy.defenseMultiplier;

                if (hitChance > 80)
                {
                    enemy.health -= damage * 2; //Crit
                    helper.totalDamage = ((int)damage * 2).ToString();

                    //Apply buff to self

                }
                else
                {
                    enemy.health -= damage;
                    helper.totalDamage = ((int)damage).ToString();
                }
            }
            else if (hitChance < 20)
            {
                //Display Miss
                //print("Miss");
                helper.totalDamage = "Miss!";
            }
            Destroy(gameObject);
        }
    }
}
