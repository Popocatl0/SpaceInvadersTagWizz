    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handle the ship's inertia and maximum speed
/// </summary>
public class ShipMovement : ShipAction{    
    /// <summary>
    /// Called when a input is received
    /// </summary>
    public override void BeginAction(){
        BeginMove();
    }

    /// <summary>
    /// Stop all ship's movement 
    /// </summary>
    public override void StopAction(){
        if(controller.Rigbody.velocity.magnitude > 0){
            controller.Rigbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Check the input value and modify the ship's direction
    /// 0 => Not movew
    /// 1 => Right
    ///-1 => Left
    /// </summary>
    public void BeginMove(){
        controller.Rigbody.velocity = controller.Input.movement * controller.maxSpeed;
    }
}
