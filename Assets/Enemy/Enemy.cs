using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0 || true)        //currently Enemys should always die on 1 hit
        {
            Die();
        }
    }

    private void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);        // TODO
        Destroy(gameObject);
    }
}
