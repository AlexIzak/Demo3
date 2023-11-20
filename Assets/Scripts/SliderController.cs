using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public PlayerStats stats;
    public Slider healthBar;
    public TMP_Text amount;

    //public Slider manaBar;
    //public TMP_Text manaPool;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = stats.maxHealth;
        amount.text = stats.health + " / " + stats.maxHealth;

        //manaBar.maxValue = stats.maxMana;
        //manaPool.text = stats.mana + " / " + stats.maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = stats.health;
        amount.text = (int)stats.health + " / " + stats.maxHealth;

        //manaBar.value = stats.mana;
        //manaPool.text = stats.mana + " / " + stats.maxMana;
    }
}
