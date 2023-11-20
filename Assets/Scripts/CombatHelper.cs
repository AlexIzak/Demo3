using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CombatHelper : MonoBehaviour
{
    public CombatHelper instance;
    public string totalDamage;

    private void Start()
    {
        instance = this;
    }

    public void Damage(PlayerStats dealer, PlayerStats receiver)
    {
        //Caculate damage
        float damage = UnityEngine.Random.Range(dealer.damage * 0.75f, dealer.damage * 1.25f) * receiver.defenseMultiplier;

        //Hit chance
        float hitChance = UnityEngine.Random.Range(1, 101);

        if(hitChance > 20)
        {
            if(hitChance > 80)
            {
                receiver.health -= damage * 2; //Crit
                totalDamage = ((int)damage * 2).ToString();
            }
            else
            {
                receiver.health -= damage;
                totalDamage = ((int)damage).ToString();
            }
        }
        else if(hitChance < 20)
        {
            //Display Miss
            //print("Miss");
            totalDamage = "Miss!";
        }
    }

    public void Test()
    {
        print("");
    }
}
