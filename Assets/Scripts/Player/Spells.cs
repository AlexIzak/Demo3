using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Effects;

public class Spells : MonoBehaviour
{
    [Header("Spell Variables and References")]
    private float spellSpeed = 2f;
    public GameObject fireball;
    public GameObject arcaneMissile;
    public GameObject frostLance;
    public static Slider castBar;
    public TMP_Text spellName;
    private float mageArmorCD = 120f;

    [Header("Spell Buttons")]
    public Button fireballButton;
    public Button frostLanceButton;
    public Button arcaneMissileButton;
    public Button mageArmourButton;

    [Header("Spell Costs")]
    public int fireballCost = 120;
    public int frostLanceCost = 45;
    public int arcaneMissilesCost = 150;
    public int mageArmorCost = 200;

    [Header("Script References")]
    public PlayerStats playerStats;
    public StatusEffects statusEffects;

    public enum SpellTypes
    {
        None,
        Fireball,
        FrostLance,
        ArcaneMissile,
        MageShield
    }

    public static SpellTypes magic;


    private float spellTimer = 0;
    private float spellInterval = 1f;
    float regenTimer = 0;
    float regenInterval = 1f;

    [Header("Effect References")]
    public GameObject mageArmor;

    private void Start()
    {
        castBar = GameObject.Find("Casting").GetComponentInParent<Slider>();
        castBar.gameObject.SetActive(false);
        magic = SpellTypes.None;
        castBar.value = 0;
    }

    private void Update()
    {
        switch (magic)
        {
            case SpellTypes.None:

                break;

            case SpellTypes.Fireball:

                StartCoroutine(SpellCooldown(fireballButton, castBar.maxValue));
                if (castBar.value <= 0)
                {
                    ShootSpell(fireball);
                }

                break;

            case SpellTypes.FrostLance:

                StartCoroutine(SpellCooldown(frostLanceButton, 0.5f));
                if (castBar.value <= 0)
                {
                    ShootSpell(frostLance);
                }

                break;

            case SpellTypes.ArcaneMissile:

                StartCoroutine(SpellCooldown(arcaneMissileButton, castBar.maxValue));
                if(castBar.value > 0)
                {                    
                    spellTimer += Time.deltaTime;
                        
                    if(spellTimer >= spellInterval) //Use timer for instantiation
                    {
                        ShootSpell(arcaneMissile);
                        spellTimer = 0;
                    }

                }
                
                break;

            case SpellTypes.MageShield:

                StartCoroutine(SpellCooldown(mageArmourButton, mageArmorCD));
                statusEffects.DisplayEffect(mageArmor);

                break;

            default: break;
        }

        if(mageArmourButton.GetComponent<Image>().color == Color.red)
        {
            mageArmorCD -= Time.deltaTime;

            mageArmourButton.GetComponentInChildren<TMP_Text>().text = ((int)mageArmorCD).ToString();
        }
        
        //Reset casting and spells
        if(castBar.value <= 0)
        {
            castBar.gameObject.SetActive(false);
            magic = SpellTypes.None;
        }
        else
        {
            castBar.value -= Time.deltaTime;

            gameObject.GetComponentInParent<Animator>().Play("Player_Casting");
        }

        regenTimer += Time.deltaTime;
            
        //Mana regen
        if(playerStats.mana < playerStats.maxMana && castBar.value <= 0)
        {
            if (regenTimer >= regenInterval)
            {
                playerStats.mana += playerStats.currentManaRegen;

                if (playerStats.mana > playerStats.maxMana) playerStats.mana = playerStats.maxMana;

                regenTimer = 0;
            }
        }
    }

    private void ShootSpell(GameObject spell)
    {
        Vector2 dest = Targeting.target.transform.position - transform.position;
        GameObject s = Instantiate(spell, transform.position, Quaternion.identity);
        s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;
    }

    private IEnumerator SpellCooldown(Button button, float spellCD)
    {
        button.GetComponent<Image>().color = Color.red;
        button.GetComponent<Button>().enabled = false;
        button.GetComponentInChildren<TMP_Text>().enabled = true;
        button.GetComponentInChildren<TMP_Text>().text = ((int)spellCD).ToString();
        yield return new WaitForSeconds(spellCD);
        button.GetComponentInChildren<TMP_Text>().enabled = false;
        button.GetComponent<Image>().color = Color.white;
        button.GetComponent<Button>().enabled = true;
    }


    public void MageShield()
    {
        if (playerStats.mana > mageArmorCost) //Check for mana
        {
            magic = SpellTypes.MageShield;

            playerStats.mana -= mageArmorCost;
        }
    }

    public void MagicMissile()
    {
        if (playerStats.mana > arcaneMissilesCost) //Check for mana
        {
            //Spell starts casting
            castBar.gameObject.SetActive(true);
            castBar.maxValue = 5f;
            castBar.value = castBar.maxValue;
            spellName.text = frostLance.name;

            magic = SpellTypes.ArcaneMissile;

            playerStats.mana -= arcaneMissilesCost;
        }
    }

    public void FrostLance()
    {
        if (playerStats.mana > frostLanceCost) //Check for mana
        {

            //Spell starts casting
            //castBar.gameObject.SetActive(true);
            //castBar.maxValue = 0;
            //castBar.value = castBar.maxValue;
            //spellName.text = frostLance.name;

            magic = SpellTypes.FrostLance;

            playerStats.mana -= frostLanceCost;
        }
    }


    public void Fireball()
    {
        if (playerStats.mana > fireballCost) //Check for mana
        {
            //Spell starts casting
            castBar.gameObject.SetActive(true);
            castBar.maxValue = 3f;
            castBar.value = castBar.maxValue;
            spellName.text = fireball.name;

            magic = SpellTypes.Fireball;

            playerStats.mana -= fireballCost;
        }
    }

    public void BrilliantCasting()
    {
        fireballCost = frostLanceCost = arcaneMissilesCost = mageArmorCost = 0;
    }
}
