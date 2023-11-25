using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpit : MonoBehaviour
{
    public PlayerStats player;
    public PlayerStats enemy;
    public CombatHelper helper;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            helper.Damage(enemy, player);
            Destroy(gameObject);
        }
    }
}
