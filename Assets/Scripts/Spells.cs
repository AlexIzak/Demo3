using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
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
                Fireball();
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
        throw new NotImplementedException();
    }
}
