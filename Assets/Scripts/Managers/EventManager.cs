using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager<T>
{
    static EventManager<T> _instance;
    public static EventManager<T> Instance{
        get{
            if(_instance == null){
                _instance = new EventManager<T> ();
            }
            return _instance;
        }
    }

    private EventManager(){
        OnListen = new Dictionary<string, OnListenDelegate>();
    }
    public delegate void OnListenDelegate(T val);
    static Dictionary<string, OnListenDelegate> OnListen;

    public void Add(string id, OnListenDelegate suscriber){
        if(OnListen.ContainsKey(id))
            OnListen[id] += suscriber;
        else{
            OnListen.Add(id, suscriber);
        }
    }

    public void Remove(string id, OnListenDelegate suscriber){
        if(OnListen.ContainsKey(id))
            OnListen[id] -= suscriber;
    }

    public void Trigger(string id, T val){
         if(OnListen.ContainsKey(id))
            OnListen[id]?.Invoke(val);
    }
}
