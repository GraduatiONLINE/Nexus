﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionRouter : MonoBehaviour
{
    private static GameObject localAvatar;
    private static GameObject currentChair;
    private static GameObject currentCharacter;
    public static Map<int, string> interactionMap;


    // Start is called before the first frame update
    void Start()
    {
        interactionMap = new Map<int, string>();
        interactionMap.Add(1, "ShakeHand");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetCurrentChair(GameObject o)
    {
        Debug.Log("current chair set");
        currentChair = o;
    }

    public static void SetLocalAvatar(GameObject o)
    {
        Debug.Log("[Action Router] Current Avatar set: " + o);
        localAvatar = o;
    }

    public static void SetCurrentCharacter(GameObject o)
    {
        Debug.Log("[Action Router] Current Character set");
        currentCharacter = o;
    }

    public static GameObject GetLocalAvatar()
    {
        return localAvatar;
    }

    public static GameObject GetCurrentChair()
    {
        return currentChair;
    }

    public static GameObject GetCurrentCharacter()
    {
        return currentCharacter;
    }


    // ActionRouter.SetLocalAvatar(....);
}


public class Map<T1, T2>
{
    private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
    private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

    public Map()
    {
        this.Forward = new Indexer<T1, T2>(_forward);
        this.Reverse = new Indexer<T2, T1>(_reverse);
    }

    public class Indexer<T3, T4>
    {
        private Dictionary<T3, T4> _dictionary;
        public Indexer(Dictionary<T3, T4> dictionary)
        {
            _dictionary = dictionary;
        }
        public T4 this[T3 index]
        {
            get { return _dictionary[index]; }
            set { _dictionary[index] = value; }
        }
    }

    public void Add(T1 t1, T2 t2)
    {
        _forward.Add(t1, t2);
        _reverse.Add(t2, t1);
    }

    public Indexer<T1, T2> Forward { get; private set; }
    public Indexer<T2, T1> Reverse { get; private set; }
}
