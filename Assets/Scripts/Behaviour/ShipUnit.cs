using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contain all the components of the ship
/// </summary>
public class ShipUnit : MonoBehaviour{ 
    public Collider2D Collider {get; protected set;}
    public bool isEnabled {get; protected set;}

    protected Sprite initSprite;

    protected virtual void Awake(){
        Collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Enable or disable itself and its actions
    /// </summary>
    /// <param name="val"></param>
    public virtual void SetEnabled(bool val=true){
        isEnabled = val;
        Collider.enabled = val;
    }
    
    /// <summary>
    /// Reset all components of the ship and enable it
    /// </summary>
    /// <param name="resetPos"></param>
    public virtual void ResetObject(){
        gameObject.SetActive(true);
        SetEnabled();
    }

    /// <summary>
    /// disable the ship
    /// </summary>
    public virtual void DetroyObject(){
        SetEnabled(false);
        gameObject.SetActive(false);
    }
}
