using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSpit : MonoBehaviour
{
    public PlayerStats player;
    public PlayerStats enemy;
    public CombatHelper helper;
    public StatusEffects statusEffects;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            helper.Damage(enemy, player);
            //statusEffects.DisplayEffect(statusEffects.gameObject.name);
            Destroy(gameObject);
        }
    }
}
