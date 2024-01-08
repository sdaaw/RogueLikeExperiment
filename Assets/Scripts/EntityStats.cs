using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityStats
{

    public GameObject entityObject;

    public float AttackRange {  get; set; }
    public float AttackDamage { get; set; }
    public float MaxHealth { get; set; }
    public float CurrentHealth //idk why I opened this, maybe for future ^3^
    { 
        get 
        { 
            return _currentHealth; 
        } 
        set 
        {
            _currentHealth = value;
        } 
    }
    public bool IsDead 
    { 
        get 
        {
            _isDead = _currentHealth <= 0; 
            return _currentHealth <= 0; 
        } 
        set 
        {
            _isDead = value; 
        } 
    }

    private float _currentHealth;
    private bool _isDead;


    public void BasicAttack(EntityStats target, float amount)
    {
        if(_isDead) return;

        target.TakeDamage(entityObject, amount);
    }
    public void TakeDamage(GameObject source, float amount) 
    {
        _currentHealth -= amount;
    }
}
