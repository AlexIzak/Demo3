using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/Create New Entity")]
public class PlayerStats : ScriptableObject
{
    public int health;
    public int mana;
    public float attackSpeed;
    public int damage;
    public float defenseMultiplier;
    public int manaRegen;
}
