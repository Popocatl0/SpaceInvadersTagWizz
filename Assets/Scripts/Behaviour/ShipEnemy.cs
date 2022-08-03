using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contain all the components of the enemy
/// </summary>
public class ShipEnemy : ShipUnit{
    [SerializeField] int maxHealth;
    [SerializeField] int points;
    public int currentHealth {get; private set;}
    public Vector2 intiPos {get; private set;}

    public delegate void OnDestroyDelegate();
    public OnDestroyDelegate OnDestroy;

    public int Points{
        get{
            return points;
        }
    }
    protected override void Awake(){
        base.Awake();
        intiPos = transform.position;
    }
    /// <summary>
    /// Restore all health
    /// </summary>
    public override void ResetObject(){
        base.ResetObject();
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Reduce the health by the damage amount,
    /// if all health is reduced disable the object
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>Is death or not</returns>
    public bool Damage(int damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            OnDestroy?.Invoke();
            this.DetroyObject();
            return true;
        }
        return false;
    }

}
