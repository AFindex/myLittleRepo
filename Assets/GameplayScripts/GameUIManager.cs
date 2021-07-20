using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
        
    private static GameUIManager _instance;
    public static GameUIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameUIManager>();
            }
            return _instance;
        }
    }
    public Text State;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
