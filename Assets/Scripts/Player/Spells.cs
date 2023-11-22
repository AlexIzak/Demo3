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
    private float spellCD;

    private void Start()
    {
        castBar.gameObject.SetActive(false);
        magic = SpellTypes.None;
    }

    private void Update()
    {
        switch (magic)
        {
            case SpellTypes.None:

                break;

            case SpellTypes.Fireball:

                while(castBar.value > 0) //Stop input while casting
                {
                    fireballButton.enabled = false;
                    fireballButton.image.color = Color.red;
                }

                if (castBar.value <= 0)
                {
                    fireballButton.enabled = true;
                    fireballButton.image.color = Color.white;

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

                while(castBar.value > 0)
                {
                    arcaneMissileButton.enabled = false;
                    arcaneMissileButton.image.color = Color.red;

                    Vector2 dest = Targeting.target.transform.position - transform.position;

                    GameObject s = Instantiate(arcaneMissile, transform.position, Quaternion.identity);
                    s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;
                }
                arcaneMissileButton.enabled = true;
                arcaneMissileButton.image.color = Color.white;

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

            gameObject.GetComponent<Animator>().Play("Player_Casting");
        }
    }

    private IEnumerator SpellCooldown(Button button)
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

            StartCoroutine(SpellCooldown(frostLanceButton));
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

            //End of cast - spawn spell
            //if (castBar.value == 0)
            //{
            //    castBar.gameObject.SetActive(false);
            //    Vector2 dest = Targeting.target.transform.position - transform.position;

            //    GameObject s = Instantiate(fireball, transform.position, Quaternion.identity);
            //    s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;
            //    print("Shot!");
            //}
            //playerStats.mana -= 120;
        }
    }
}
