using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public enum OpType
{
    Add = 0,
    Dele = 1,
}

public class baseItem : MonoBehaviour
{
    public bool canSet = true;

    public Material beSelectedMat;
    public Material preBuildMat;
    public Material originMat;
    public Material selectedShow;
    
    // refact
    public int ItemIndex = -1;
    public Color noReady;
    public Color Ready;
    public OpType opType = OpType.Add;
    private String itemName = "test";
    private SkillFSM itemFsm;
    
    // state
    MoveItemState moveState;
    AddItemState addState ;
    DeleItemState deleState ;   
    public void Awake()
    {
        originMat = GetComponent<MeshRenderer>().material;
    }

    public void OnUISelected(bool isSelected)
    {
        if (isSelected)
        {
            baseItem item = gameObject.GetComponent<baseItem>();
            if(moveState == null)
                moveState = new MoveItemState(item);
            if(addState == null)
                addState = new AddItemState(item);
            if(deleState == null)
                deleState = new DeleItemState(item);
            if (itemFsm == null)
            {
                itemFsm = new SkillFSM();
                itemFsm.Create(itemFsm+"FSM", moveState, addState, deleState);
                FsmManager.Instance.AddFsm(itemFsm.fsmName, itemFsm);
            }
                    
            itemFsm.FsmStart<MoveItemState>();
        }
        else
        {
            if (itemFsm != null)
            {
                itemFsm.FsmEnd();
            }
        }
       
    }
    public void DestroySelf()
    {
        // 之后使用对象池回收
        Destroy(this.gameObject);
    }

    public void SetGameObjSeleced(bool isSeleced)
    {
        if(isSeleced)
            GetComponent<MeshRenderer>().material = beSelectedMat;
        else
            GetComponent<MeshRenderer>().material = originMat;
            
    }

    private void OnTriggerStay(Collider other)
    {
        AimEvent.Instance.item.canSet = false;
    }

    private void OnTriggerExit(Collider other)
    {
        AimEvent.Instance.item.canSet = true;
    }

}
