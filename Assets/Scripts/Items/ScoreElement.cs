using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Set score text
/// </summary>
public class ScoreElement : MonoBehaviour
{
    public Text textScore, endScore;
    public string PointsPattern = "00";

    public void UpdateScore(int value){
        textScore.text = value.ToString(PointsPattern);
        endScore.text = value.ToString(PointsPattern);
    }

    void OnEnable(){
        EventManager<int>.Instance.Add("PlayerScore", UpdateScore);
    }

    void OnDisable(){
        EventManager<int>.Instance.Remove("PlayerScore", UpdateScore);
    }
}
