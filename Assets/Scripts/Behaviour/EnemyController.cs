using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Display and control the enemies 
/// </summary>
public class EnemyController : MonoBehaviour{
    public bool isEnabled {get; set;}
    [SerializeField] LevelData data;
    int destroyed;
    ShipEnemy[] enemies;
    Vector3 initPos;
    int dir;
    void Awake(){
        initPos = transform.position;
    }
    public void Set(LevelData _data=null){
        if(initPos == null) 
            initPos = transform.position;
        else 
            transform.position = initPos;
        destroyed = 0;
        dir = 1;
        if(enemies != null && enemies.Length > 0){
            for (int i = 0; i < enemies.Length; i++){
                enemies[i].OnDestroy -= OnDefeat;
                Destroy(enemies[i]);
            }
        }
        if(_data != null)
            data = _data;
        isEnabled = true;
        enemies = data.Display(transform, OnDefeat);
    }

    public void Reset(){
        destroyed = 0;
        dir = 1;
        isEnabled = true;
        transform.position = initPos;
        foreach (ShipEnemy enemy in enemies){
            enemy.ResetObject();
        }
    }

    void OnDefeat(){
        destroyed++;
        if(destroyed >= enemies.Length){
            EventManager<string>.Instance.Trigger("Endgame", "Win");
        }
    }

    void Move(){
        if(!isEnabled) return;
        transform.Translate( Vector3.right * dir * Time.deltaTime * data.Speed);
        if(Mathf.Abs(transform.position.x) >= data.Limit){
            dir *= -1;
            transform.Translate( Vector3.down * data.VerticalSpacing);
        }
    }

    void Update(){
        Move();
    }
}
