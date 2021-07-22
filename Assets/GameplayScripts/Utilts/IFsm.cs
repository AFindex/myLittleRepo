using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IFsm<T>
{
    abstract public IFsm<T> Create(string fsmName, params T[] states);
    abstract public void FsmStart<T>();
    abstract public void FsmEnd();
    public T currentState;
    public List<T> allStates;
    
    public string fsmName;
    public bool isStart = false;
    public bool canUpdate = false;
}
