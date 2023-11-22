using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaSlider : MonoBehaviour
{
    public PlayerStats stats;
    public Slider manaBar;
    public TMP_Text manaPool;

    // Start is called before the first frame update
    void Start()
    {
        manaBar.maxValue = stats.maxMana;
        manaPool.text = stats.mana + " / " + stats.maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.value = stats.mana;
        manaPool.text = stats.mana + " / " + stats.maxMana;
    }
}
