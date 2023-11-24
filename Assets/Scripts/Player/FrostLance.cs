using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostLance : MonoBehaviour
{
    private float spellDamage = 10f;
    //private float slowEffect = 15f;
    //private int stacks  = 5;
    //private float stackDuration = 5f;
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

                //Apply slow debuff to the enemy

                if (hitChance > 80)
                {
                    enemy.health -= (int)damage * 2; //Crit
                    CombatHelper.totalDamage = ((int)damage * 2).ToString();
                }
                else
                {
                    enemy.health -= (int)damage;
                    CombatHelper.totalDamage = ((int)damage).ToString();
                }
            }
            else if (hitChance < 20)
            {
                //Display Miss
                //print("Miss");
                CombatHelper.totalDamage = "Miss!";
            }
            Destroy(gameObject);
        }
    }
}
