using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the speed,direction and collision of the bullet 
/// </summary>
public class BulletObject : MonoBehaviour{
    public float speed;
    Rigidbody2D rigbody;

    public delegate void OnCollisionDelegate(BulletObject item, ShipEnemy target);
    public OnCollisionDelegate onCollision;

     void Awake(){
        rigbody = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// Initialize the direction and speed
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="pos"></param>
    public void Set(Vector3 dir, Vector3 pos){
        this.gameObject.SetActive(true);
        this.transform.position = pos;
        this.transform.up = dir.normalized;
        rigbody.velocity = dir.normalized * speed;
    }
    /// <summary>
    /// When collides with a player, call a delegate for handle the damage and score
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Enemy"){
            onCollision(this, other.GetComponent<ShipEnemy>());
        }
    }

    void OnBecameInvisible(){
        onCollision(this, null);
    }
}
