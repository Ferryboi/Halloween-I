using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IAttackable, IHealable
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    public void OnAttacked(int damage = -1)
    {
        //If a negative value is given, instant death
        if (damage < 0) Death();

        currentHealth -= damage;

        if (currentHealth <= 0) Death();
    }

    public void OnHealed(int health = -1)
    {
        //If a negative value is given, restore health to max. If current health is already over max ignore
        if (health < 0 && currentHealth < maxHealth) { currentHealth = maxHealth; return; }

        currentHealth += health;
    }

    private void Death()
    {
        Entity entity = GetComponentInParent<Entity>();
        if (entity) entity.KillEntity();
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}
