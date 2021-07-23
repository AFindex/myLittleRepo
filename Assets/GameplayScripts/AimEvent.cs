using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEvent : MonoBehaviour
{
    
    private static AimEvent _instance;
    public static AimEvent Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AimEvent>();
            }
            return _instance;
        }
    }
    public GameObject aimObject;
    public BaseItem item;
    public void OnUISelectedChange(BaseItem selectedItem,bool beSelected)
    {
        selectedItem.OnUISelected(beSelected);
    }
}
