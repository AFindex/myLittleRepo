using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Utility.Inspector;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnStayTodo;
    public UnityEvent OnExitTodo;
    public UnityEvent OnExtendTodo;
    public UnityEvent OnNotExtendTodo;
    public GameObject Slot;
    public float extendLen = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Slot.transform.position, transform.position) > extendLen)
        {
            OnExtendTodo?.Invoke();
        }
        else
        {
            OnNotExtendTodo?.Invoke();
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "triger")
        {
            OnStayTodo?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "triger")
        {
            OnExitTodo?.Invoke();
        }
    }
}
