using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private SkillFSM _fsm;
    void Start()
    {
        //_fsm =new SkillFSM(); 
        //AtestState a = new AtestState();
        //BtestState b = new BtestState();
        //CtestState c = new CtestState();
        //DtestState d = new DtestState();
        //EtestState e = new EtestState();
        //_fsm.Create("testFsm", a, b, c, d, e);
        //FsmManager.Instance.AddFsm(_fsm.fsmName, _fsm);
        //_fsm.FsmStart<AtestState>();
    }
}

/// <summary>
/// AState
/// </summary>
class AtestState : IFsmState<BaseItem>
{
    private string stateName = "A";
    public override void OnInit(IFsm<IFsmState<BaseItem>> owner)
    {
        FsmOwner = owner;  
        Debug.Log($"OnInit : {stateName}");
    }

    public override void OnDestory()
    {
        Debug.Log($"OnDesTory : {stateName}");
    }

    public override void OnEnter(params object[] parms)
    {
        Debug.Log($"OnEnter : {stateName}");
    }

    public override void OnLeave()
    {
        Debug.Log($"OnLeave : {stateName}");
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
            FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>, BtestState>(FsmOwner);
        Debug.Log($"OnUpdate : {stateName}");
    }
}
/// <summary>
/// BState
/// </summary>
class BtestState : IFsmState<BaseItem>
{
    private string stateName = "B";
    
    public override void OnInit(IFsm<IFsmState<BaseItem>> owner)
    {
        FsmOwner = owner;  
        Debug.Log($"OnInit : {stateName}");
    }
  
    public override void OnDestory()
    {
        Debug.Log($"OnDesTory : {stateName}");
    }

    public override void OnEnter(params object[] parms)
    {
        Debug.Log($"OnEnter : {stateName}");
    }

    public override void OnLeave()
    {
        Debug.Log($"OnLeave : {stateName}");
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.B))
            FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>, CtestState>(FsmOwner);
        Debug.Log($"OnUpdate : {stateName}");
    }
}
/// <summary>
/// CState
/// </summary>
class CtestState : IFsmState<BaseItem>
{
    private string stateName = "C";
        
    public override void OnInit(IFsm<IFsmState<BaseItem>> owner)
    {
        FsmOwner = owner;  
        Debug.Log($"OnInit : {stateName}");
    }
  
    public override void OnDestory()
    {
        Debug.Log($"OnDesTory : {stateName}");
    }

    public override void OnEnter(params object[] parms)
    {
        Debug.Log($"OnEnter : {stateName}");
    }

    public override void OnLeave()
    {
        Debug.Log($"OnLeave : {stateName}");
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.C))
            FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>, DtestState>(FsmOwner);
        Debug.Log($"OnUpdate : {stateName}");
    }
}
/// <summary>
/// DState
/// </summary>
class DtestState : IFsmState<BaseItem>
{
    private string stateName = "D";
        
    public override void OnInit(IFsm<IFsmState<BaseItem>> owner)
    {
        FsmOwner = owner;  
        Debug.Log($"OnInit : {stateName}");
    }
  
    public override void OnDestory()
    {
        Debug.Log($"OnDesTory : {stateName}");
    }

    public override void OnEnter(params object[] parms)
    {
        Debug.Log($"OnEnter : {stateName}");
    }

    public override void OnLeave()
    {
        Debug.Log($"OnLeave : {stateName}");
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.D))
            FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>, EtestState>(FsmOwner);
        Debug.Log($"OnUpdate : {stateName}");
    }
}
/// <summary>
/// EState
/// </summary>
class EtestState : IFsmState<BaseItem>
{
    private string stateName = "E";
        
    public override void OnInit(IFsm<IFsmState<BaseItem>> owner)
    {
        FsmOwner = owner;  
        Debug.Log($"OnInit : {stateName}");
    }
 
    public override void OnDestory()
    {
        Debug.Log($"OnDesTory : {stateName}");
    }

    public override void OnEnter(params object[] parms)
    {
        Debug.Log($"OnEnter : {stateName}");
    }

    public override void OnLeave()
    {
        Debug.Log($"OnLeave : {stateName}");
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
            FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>, AtestState>(FsmOwner);
        Debug.Log($"OnUpdate : {stateName}");
    }
}