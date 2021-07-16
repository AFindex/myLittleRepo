using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InputManager>();
            }
            return _instance;
        }
    }
    private Action onMouseScroll;

    public void ResigerMouseScroll(Action mouseScroll)
    {
        onMouseScroll += mouseScroll;
    }

    void Update()
    {
        if(AimEvent.Instance.item)
            AimEvent.Instance.item.selectTodd();
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            onMouseScroll?.Invoke();
        }
        
    }
}