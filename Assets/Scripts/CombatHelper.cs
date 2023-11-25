using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatHelper : MonoBehaviour
{
    public CombatHelper instance;
    public static string totalDamage;

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

            if(dealer.type == "Enemy" && hitChance >= 50)
            {
                damage += 10;
            }

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

            if(dealer.type == "Enemy" && AIStateMachine.enraged)
            {
                damage *= 2;
                totalDamage = ((int)damage).ToString();
            }
        }
        else if(hitChance < 20)
        {
            totalDamage = "Miss!";
        }
    }

    public IEnumerator Death(Animator corpse)
    {
        corpse.Play("Death");
        yield return new WaitForSeconds(3);
        if(corpse.CompareTag("Player")) SceneManager.LoadScene(0);
        else corpse.StopPlayback();
    }

    public void DisplayDamage(TMP_Text floatingNumbers, PlayerStats entity)
    {
        if (entity.health < entity.maxHealth)
        {
            floatingNumbers.enabled = true;
            floatingNumbers.text = CombatHelper.totalDamage;
        }
    }

    public void Test()
    {
        print("");
    }
}
