using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float spellDamage = 35f;
    private float burnDamage = 4f;
    public PlayerStats enemy;
    public CombatHelper helper;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            print("Hit!");
            //gameObject.SetActive(false);

            //Hit chance
            float hitChance = UnityEngine.Random.Range(1, 101);

            if (hitChance > 20)
            {
                //Caculate damage if we hit
                float damage = UnityEngine.Random.Range(spellDamage * 0.75f, spellDamage * 1.25f) * enemy.defenseMultiplier;

                //Apply burn debuff to the enemy

                if (hitChance > 80)
                {
                    enemy.health -= damage * 2; //Crit
                    helper.totalDamage = ((int)damage * 2).ToString();

                    //Double debuff damage too

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
