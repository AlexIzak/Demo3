using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public float castingCD = 3f;
    private float curCastingCD;
    public float spellSpeed;
    public GameObject spell;

    public PlayerStats playerStats;

    public enum SpellTypes
    {
        Fireball,
        FrostLance,
        MagicMissile,
        MageShield
    }

    SpellTypes magic;

    private void Update()
    {
        switch(magic)
        {
            case SpellTypes.Fireball:
                //Fireball();
                break;

            case SpellTypes.FrostLance:
                FrostLance();
                break;

            case SpellTypes.MagicMissile:
                MagicMissile();
                break;        

            case SpellTypes.MageShield:
                MageShield();
                break;

            default: break;
        }
    }

    public void MageShield()
    {
        throw new NotImplementedException();
    }

    public void MagicMissile()
    {
        throw new NotImplementedException();
    }

    public void FrostLance()
    {
        throw new NotImplementedException();
    }

    public void Fireball()
    {
        if (playerStats.mana > 120)
        {
            curCastingCD += Time.deltaTime;
            if (curCastingCD > castingCD)
            {
                Vector2 dest = Targeting.target.transform.position - transform.position;

                GameObject s = Instantiate(spell, transform.position, Quaternion.identity);
                s.GetComponent<Rigidbody2D>().velocity = new Vector2(dest.x, dest.y).normalized * spellSpeed;

                curCastingCD = 0;
            }
            playerStats.mana -= 120;
        }
    }
}
