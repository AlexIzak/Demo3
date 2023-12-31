using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/Create New Entity")]
public class PlayerStats : ScriptableObject
{
    [Header("Stats")]
    public string type;
    public float maxHealth;
    public float health;
    public float maxMana;
    public float mana;
    public float attackSpeed;
    public float baseMoveSpeed;
    public float currentMoveSpeed;
    public float damage;
    public float baseDefenseMultiplier;
    public float currentDefenseMultiplier;
    public int baseManaRegen;
    public int currentManaRegen;
}
