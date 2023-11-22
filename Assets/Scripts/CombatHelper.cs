using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //Hit chance
        float hitChance = UnityEngine.Random.Range(1, 101);

        if(hitChance > 20)
        {
            //Caculate damage if we hit
            float damage = UnityEngine.Random.Range(dealer.damage * 0.75f, dealer.damage * 1.25f) * receiver.defenseMultiplier;

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

    public IEnumerator Death(Animator corpse)
    {
        corpse.Play("Player_Dead");
        yield return new WaitForSeconds(3);
        if(corpse.CompareTag("Player")) SceneManager.LoadScene(0);
        //else corpse.
    }

    public void Test()
    {
        print("");
    }
}
