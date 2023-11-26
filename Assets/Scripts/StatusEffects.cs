using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffects : MonoBehaviour
{
    //Visuals for effects
    private TMP_Text timer;
    public static bool active;
    public float effectDuration;
    private Image icon;
    //private TMP_Text text;

    public PlayerInput player;
    public AIStateMachine enemy;

    //Burn variables
    private float burnTimer = 0f;
    private float burnInterval = 3f;
    public static bool burnCrit = false;

    //Slow variables
    private int stacks = 0;

    private void Start()
    {
        icon = GetComponentInChildren<Image>();
        timer = GetComponentInChildren<TMP_Text>();


    }

    public void DisplayEffect(GameObject effect)
    {
        //active = true;
        effect.SetActive(true);
        //icon.enabled = true;
        //timer.enabled = true;
    }

    private void Update()
    {
        if(icon.enabled)
        {
            switch(gameObject.name)
            {

                case "Burn":
                
                    burnTimer += Time.deltaTime;

                    if(burnTimer >= burnInterval)
                    {
                        if(burnCrit)
                        {
                            enemy.GetEnemyStats().health -= 8;
                        }
                        else enemy.GetEnemyStats().health -= 4;

                        burnTimer = 0;
                    }
                    break;

                //case "Slow":
                //    stacks++;
                //    for(int i = 0; i <= stacks; i++) enemy.GetEnemyStats().currentMoveSpeed *= 0.15f;

                //    if(stacks == 5) 
                //    break;

                case "Brilliance":
                    
                    break;

                case "MageArmor":
                    //effectDuration = 30f;
                    player.GetPlayerStats().currentDefenseMultiplier = 0.65f;
                    player.GetPlayerStats().currentManaRegen = 25;
                    break;

                case "Toxic":

                    break;
            }

            timer.text = ((int)effectDuration).ToString();
            effectDuration -= Time.deltaTime;
            if (effectDuration <= 0 )
            {
                gameObject.SetActive(false);
                ResetEffects();
            }
            //else ApplyStatus(); //Apply effect of this current status
        }
    }

    private void ResetEffects()
    {
        //Reset player buffs
        player.GetPlayerStats().currentManaRegen = player.GetPlayerStats().baseManaRegen;
        player.GetPlayerStats().currentDefenseMultiplier = player.GetPlayerStats().baseDefenseMultiplier;

        //Reset toxin debuff
    }

    private void ApplyStatus()
    {
       
    }
}
