using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = System.Object;

public class FsmManager : MonoBehaviour
{
    private static FsmManager _instance;
    public static FsmManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<FsmManager>();
            }
            return _instance;
        }
    }

    private Dictionary<string,IFsm<IFsmState<BaseItem>>> allFsmsDic = new Dictionary<string, IFsm<IFsmState<BaseItem>>>();
    
    public void AddFsm( string fsmName,IFsm<IFsmState<BaseItem>> fsm)
    {
        if(allFsmsDic.ContainsKey(fsmName)) return;
        
        foreach (var fsmState in fsm.allStates)
        {
            fsmState.OnInit(fsm);
        }
        allFsmsDic.Add(fsmName,fsm);
    }

    private void Update()
    {
        foreach (var fsm in allFsmsDic.Values)
        {
            if(fsm.isStart) fsm.currentState.OnUpdate();
        }
        
    }

    public void ChangeFsmState<T, TA>(IFsm<T> fsm, params Object[] enterParams) where T : IFsmState<BaseItem>
    {
        fsm.canUpdate = false;
        
        fsm.currentState.OnLeave();
        foreach (var state in fsm.allStates)
        {
            if (state.GetType() == typeof(TA))
            {
                fsm.currentState = state;
            }
        }
        fsm.currentState.OnEnter(enterParams);
        
        fsm.canUpdate = true;
    }
}
