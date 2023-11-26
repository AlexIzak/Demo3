using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fireball : MonoBehaviour
{
    private float spellDamage = 35f;
    //private float burnDamage = 4f;
    public PlayerStats enemy;
    public CombatHelper helper;
    public StatusEffects statusEffects;
    private GameObject burn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            print("Hit");
            //Hit chance
            float hitChance = UnityEngine.Random.Range(1, 101);

            if (hitChance > 20)
            {
                //Caculate damage if we hit
                float damage = UnityEngine.Random.Range(spellDamage * 0.75f, spellDamage * 1.25f) * enemy.currentDefenseMultiplier;

                //Apply burn debuff to the enemy
                //burn = GameObject.Find("Burn");
                //statusEffects.DisplayEffect(burn);

                //burn.GetComponentInChildren<Image>().enabled = true;
                //burn.GetComponentInChildren<TMP_Text>().enabled = true;

                if (hitChance > 80)
                {
                    enemy.health -= (int)damage * 2; //Crit
                    CombatHelper.totalDamage = ((int)damage * 2).ToString();

                    //Double debuff damage too
                    StatusEffects.burnCrit = true;
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
