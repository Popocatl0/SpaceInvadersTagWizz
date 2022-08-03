using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contain all the components of the ship
/// Manage the Actions Cycle and contain the player's ship data
/// </summary>
public class ShipController : ShipUnit
{
    public Transform startPosition;
    [SerializeField] string _id;
    [SerializeField] InputManager _input;
    public float maxSpeed;
    public float shootRate;
    public Rigidbody2D Rigbody {get; private set;}
    public Score Score {get; private set;}
    public Vector2 currentDir {get; set;}

   public InputManager Input{
        get{ return _input;}
    }
    public String ID{
        get{ return _id;}
    }

    ShipAction[] actions;

    protected override void Awake(){
        base.Awake();
        Rigbody = GetComponent<Rigidbody2D>();
        Score = GetComponent<Score>();
        actions = GetComponents<ShipAction>();
        currentDir = this.transform.up;

        foreach(var act in actions){
            act.Init(this);
        }
        _input.SetController(this);
        Score.SetController(this);

        if(startPosition != null) transform.position = startPosition.position;

        SetEnabled();
    }

    /// <summary>
    /// Enable or disable itself and its actions
    /// </summary>
    /// <param name="val"></param>
    public override void SetEnabled(bool val=true){
        base.SetEnabled(val);
        foreach(var act in actions){
            act.SetActive(val);
            if(!val)
                act.StopAction();
        }
    }
    /// <summary>
    /// When a input is received, call the respective action's method
    /// </summary>
    public void UpdateInput(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.BeginAction();
        }
    }
    /// <summary>
    /// Every frame call the respective action's method
    /// </summary>
    void Update(){
        if(!isEnabled) return;
        foreach(var act in actions){
            if(act.actionEnabled) act.ProcessAction();
        }
    }

    public override void ResetObject(){
        base.ResetObject();
        Score.ResetPoints();
    }
}
