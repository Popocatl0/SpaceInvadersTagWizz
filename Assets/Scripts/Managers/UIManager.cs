using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handle the screens and UI elements
/// </summary>
public class UIManager : MonoBehaviour{
    static UIManager _instance;
    public static UIManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<UIManager> ();
                if (_instance == null){
                    GameObject obj = new GameObject ();
                    _instance = obj.AddComponent<UIManager> ();
                }
            }
            return _instance;
        }
    }
    public CanvasGroup initMenu, gameOver;
    public Text endText;
    CanvasGroup currentCanvas;

    /// <summary>
    /// Initialize de UI elements
    /// </summary>
    void Start(){
        currentCanvas = initMenu;
    }
    /// <summary>
    /// Fade a canvas, enable or disable the element based in the alpha value
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="time"></param>
    /// <param name="alphaEnd"></param>
    /// <returns></returns>
    IEnumerator Fade(CanvasGroup canvas, float time, float alphaEnd){
        if(alphaEnd > 0) canvas.gameObject.SetActive(true);
        float alphaStart = canvas.alpha;
        float timer = 0;
        while(timer < time){
            canvas.alpha = Mathf.Lerp(alphaStart, alphaEnd, timer/time);
            timer += Time.deltaTime;
            yield return null;
        }
        //canvas.alpha = alphaEnd;
        if(alphaEnd == 0) canvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Change the screen
    /// </summary>
    /// <param name="menu"></param>
    public void ChangeCanvas(CanvasGroup menu){
        StartCoroutine(Fade(menu, 0.5f, 1));
        StartCoroutine(Fade(currentCanvas, 0.5f, 0));

        currentCanvas = menu;
    }

    void EndScreen(string status){
        endText.text = "You " + status;
        StartCoroutine(EndDelay(1.5f));
    }

    IEnumerator EndDelay(float delay){
        yield return new WaitForSeconds(delay);
        ChangeCanvas(gameOver);
    }

    void OnEnable(){
        EventManager<string>.Instance.Add("Endgame", EndScreen);
    }

    void OnDisable(){
        EventManager<string>.Instance.Remove("Endgame", EndScreen);
    }
}
