using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public abstract class IFsmState<T>
{
    public IFsm<IFsmState<T>> FsmOwner;
    public T aimObject;
    public abstract void OnInit(IFsm<IFsmState<T>> owner);
    public abstract void OnDestory();
    public abstract void OnEnter(params Object[] parms);
    public abstract void OnLeave();
    public abstract void OnUpdate();
}
