using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spells : MonoBehaviour
{
    private float spellSpeed = 2f;
    public GameObject fireball;
    public GameObject arcaneMissile;
    public GameObject frostLance;
    public Slider castBar;
    public TMP_Text spellName;
    public Button fireballButton;
    public Button frostLanceButton;
    public Button arcaneMissileButton;
    public Button mageArmourButton;

    public PlayerStats playerStats;

    public enum SpellTypes
    {
        None,
        Fireball,
        FrostLance,
        ArcaneMissile,
        MageShield
    }

    SpellTypes magic;

    private float spellTimer = 0;
    private float spellInterval = 1f;

    private void Start()
    {
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

                if (castBar.value <= 0)
                {
                    Vector2 dest = Targeting.target.transform.position - transform.position;

                    GameObject s = Instantiate(fireball, transform.position, Quaternion.identity);
                    s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;
                }

                break;

            case SpellTypes.FrostLance:

                if (castBar.value <= 0)
                {
                    castBar.gameObject.SetActive(false);
                    Vector2 dest = Targeting.target.transform.position - transform.position;

                    GameObject s = Instantiate(frostLance, transform.position, Quaternion.identity);
                    s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;
                }

                break;

            case SpellTypes.ArcaneMissile:

                if(castBar.value > 0)
                {
                    Vector2 dest = Targeting.target.transform.position - transform.position;
                    
                    spellTimer += Time.deltaTime;
                        
                    print(spellTimer);

                    if(spellTimer >= spellInterval) //Use timer for instantiation
                    {
                        GameObject s = Instantiate(arcaneMissile, transform.position, Quaternion.identity);
                        s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;


                        spellTimer = 0;
                    }

                }
                
                break;

            case SpellTypes.MageShield:
                MageShield();
                break;

            default: break;
        }
        

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
    }

    private IEnumerator SpellCooldown(Button button, float spellCD)
    {
        button.GetComponent<Image>().color = Color.red;
        button.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(spellCD);
        button.GetComponent<Image>().color = Color.white;
        button.GetComponent<Button>().enabled = true;
    }


    public void MageShield()
    {
        throw new NotImplementedException();
    }

    public void MagicMissile()
    {
        if (playerStats.mana > 150) //Check for mana
        {
            //Spell starts casting
            castBar.gameObject.SetActive(true);
            castBar.maxValue = 5f;
            castBar.value = castBar.maxValue;
            spellName.text = frostLance.name;

            magic = SpellTypes.ArcaneMissile;

            playerStats.mana -= 150;

            StartCoroutine(SpellCooldown(arcaneMissileButton, castBar.maxValue));
        }
    }

    public void FrostLance()
    {
        if (playerStats.mana > 45) //Check for mana
        {

            //Spell starts casting
            castBar.gameObject.SetActive(true);
            castBar.maxValue = 0;
            castBar.value = castBar.maxValue;
            spellName.text = frostLance.name;

            magic = SpellTypes.FrostLance;

            playerStats.mana -= 45;

            StartCoroutine(SpellCooldown(frostLanceButton, 0.5f));
        }
    }


    public void Fireball()
    {
        if (playerStats.mana > 120) //Check for mana
        {
            //Spell starts casting
            castBar.gameObject.SetActive(true);
            castBar.maxValue = 3f;
            castBar.value = castBar.maxValue;
            spellName.text = fireball.name;

            magic = SpellTypes.Fireball;

            playerStats.mana -= 120;

            StartCoroutine(SpellCooldown(fireballButton, castBar.maxValue));
        }
    }
}
