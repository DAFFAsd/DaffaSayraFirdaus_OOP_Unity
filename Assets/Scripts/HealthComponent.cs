using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    public int Health => health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Subtract(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

