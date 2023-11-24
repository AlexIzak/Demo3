using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public PlayerStats stats;
    public Slider healthBar;
    public TMP_Text amount;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = stats.maxHealth;
        amount.text = stats.health + " / " + stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = stats.health;
        amount.text = (int)stats.health + " / " + stats.maxHealth;
    }
}
