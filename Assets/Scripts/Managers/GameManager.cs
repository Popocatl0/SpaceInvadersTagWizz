using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the Core Game Loop
/// </summary>
public class GameManager : MonoBehaviour{
    [SerializeField] ShipController player;
    [SerializeField] PlayerArea area;
    [SerializeField] EnemyController enemy;
    static GameManager _instance;
    public static GameManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager> ();
                if (_instance == null){
                    GameObject obj = new GameObject ();
                    _instance = obj.AddComponent<GameManager> ();
                }
            }
            return _instance;
        }
    }
    /// <summary>
    /// Start a new match, with new score
    /// </summary>
    /// <param name="val"></param>
    public void StartMatch(LevelData data){
        gameObject.SetActive(true);
        player.ResetObject();
        area.Reset();
        enemy.Set(data);
    }

    /// <summary>
    /// Restart the match and set the score to zero
    /// </summary>
    public void ResetMatch(){
        gameObject.SetActive(true);
        player.ResetObject();
        area.Reset();
        enemy.Reset();
    }

    void EndMatch(string status){
       StartCoroutine(EndDelay(1.5f));
    }

    IEnumerator EndDelay(float delay){
        player.SetEnabled(false);
        enemy.isEnabled = false;
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

     void OnEnable(){
        EventManager<string>.Instance.Add("Endgame", EndMatch);
    }

    void OnDisable(){
        EventManager<string>.Instance.Remove("Endgame", EndMatch);
    }
}
