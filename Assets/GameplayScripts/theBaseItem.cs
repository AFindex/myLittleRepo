using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class theBaseItem : BaseItem
{
    BaseMoveItemState _baseMoveState;
    AddItemState addState ;
    DeleItemState deleState ;

    public override void OnUISelected(bool isSelected)
    {
        base.OnUISelected(isSelected);
        if (isSelected)
        {
            BaseItem item = gameObject.GetComponent<BaseItem>();
            
            if(_baseMoveState == null)
                _baseMoveState = new BaseMoveItemState(item);
            if(addState == null)
                addState = new AddItemState(item);
            if(deleState == null)
                deleState = new DeleItemState(item);
            if (itemFsm == null)
            {
                itemFsm = new SkillFSM();
                itemFsm.Create(itemFsm+"FSM", _baseMoveState, addState, deleState);
                FsmManager.Instance.AddFsm(itemFsm.fsmName, itemFsm);
            }
            itemFsm.FsmStart<BaseMoveItemState>();
        }
        else
        {
            if (itemFsm != null)
            {
                itemFsm.FsmEnd();
            }
        }
    }
    public override void ChangeOpMode()
    {
        base.ChangeOpMode();
        if (Input.GetKeyDown(KeyCode.U))
        {
            opType = OpType.Dele;
            GameUIManager.Instance.SetState("测试方块-涂色");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            opType = OpType.Add;
            GameUIManager.Instance.SetState("测试方块-建造");
        }
    }

    public override void OnRuning()
    {
        base.OnRuning();
    }

    public override void OnLeaveRuning()
    {
        base.OnLeaveRuning();
    }
}
