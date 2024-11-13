using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    private InvincibilityComponent flashComponent;
    private void Start()
    {
        flashComponent = GetComponent<InvincibilityComponent>();
    }
    public void Damage(int damage)
    {
        if (healthComponent != null && (flashComponent == null || !flashComponent.isInvincible))
        {
            healthComponent.Subtract(damage);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (healthComponent != null && (flashComponent == null || !flashComponent.isInvincible))
        {
            healthComponent.Subtract(bullet.damage);
        }
    }
}

