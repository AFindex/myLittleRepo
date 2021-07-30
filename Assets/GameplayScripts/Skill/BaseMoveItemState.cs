using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveItemState : IFsmState<BaseItem>
{
    private string stateName = "MoveItemState";
    MeshRenderer aimMeshRenderer;
    private GameObject uiShowObj;
    private int blockIndex;

    public BaseMoveItemState(BaseItem aim)
    {
        aimObject = aim;
    }
    public override void OnInit(IFsm<IFsmState<BaseItem>> fsmowner)
    {
        FsmOwner = fsmowner;  
        moveInit();
        Debug.Log($"OnInit : {stateName}");
    }
 
    public override void OnDestory()
    {
        Debug.Log($"OnDesTory : {stateName}");
    }

    public override void OnEnter(params object[] parms)
    {

    }
 
    public override void OnLeave()
    {
        Debug.Log($"OnLeave : {stateName}");
    }
 
    public override void OnUpdate()
    {
        MouseMoving();
        StateChange();
        Debug.Log($"OnUpdate : {stateName}");
    }

    public virtual void  StateChange()
    {
        if (aimObject.opType == OpType.Add)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeaveTodo();
                FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>,AddItemState>(FsmOwner, 
                    uiShowObj.transform.position,
                    uiShowObj.transform.rotation, 
                    blockIndex);
            }
        }else if (aimObject.opType == OpType.Dele)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeaveTodo();
                FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>,DeleItemState>(FsmOwner, uiShowObj.transform.position);
            }
            
        }
    }
    
    void moveInit()
    {
        if (uiShowObj == null)
        {
            uiShowObj = GameObject.Instantiate(aimObject.gameObject);
            uiShowObj.name = "uiShow";
        }
        
        uiShowObj.SetActive(true);
        uiShowObj.layer = LayerMask.NameToLayer("Default");
        aimObject.canSet = true;
        uiShowObj.GetComponent<BoxCollider>().isTrigger = true;
        //isItemMoving = true;
        aimMeshRenderer = uiShowObj.GetComponent<MeshRenderer>();
        aimMeshRenderer.material = aimObject.preBuildMat;
    }
    
    void LeaveTodo()
    {
        uiShowObj.SetActive(false);
    }
    
    void MouseMoving()
    {
        float step = 0.4f;
        
        Vector3 hitpos = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        LayerMask mask = (1 << 3);
        if (Physics.Raycast(ray, out hit,100f, mask)) {
            if (hit.transform.name == "workHub")
            {
                uiShowObj.SetActive(true);
                hitpos = hit.point;
                Vector3 newPos = hitpos;
                newPos.x = newPos.x - (newPos.x % step);
                newPos.y = newPos.y - (newPos.y % step);
                newPos.z = newPos.z - (newPos.z % step);
                // pos
                
                blockIndex = -1;
                uiShowObj.transform.position = newPos;
            }else if (hit.transform.gameObject.CompareTag("BulidItem"))
            {
                uiShowObj.SetActive(true);
                Vector3 hitObjPos = hit.transform.position;
                ItemOnplay itemOnplay = hit.transform.gameObject.GetComponent<ItemOnplay>();
                BaseItem baseItem = hit.transform.gameObject.GetComponent<BaseItem>();
                blockIndex = baseItem.BlockListIndex;
                int slotIndex =  itemOnplay.InWhichFace(hit.point);
                Vector3 posDir = (itemOnplay.slotS[slotIndex].position - hitObjPos).normalized;
                Vector3 newPos =hitObjPos  + posDir * step;
                //UIBaseAction.CheckVectorDir(hitDir,((type, isPostiveDir) =>
                //{
                //    float addValue = step;
                //    if (!isPostiveDir) addValue = -step;
                //    if (type == UIBaseAction.ChooseType.XAxis)
                //    {
                //        newPos.x += addValue;
                //    }else if (type == UIBaseAction.ChooseType.YAxis)
                //    {
                //        newPos.y += addValue;
                //    }else if (type == UIBaseAction.ChooseType.ZAxis)
                //    {
                //        newPos.z += addValue;
                //    }
                //}));
                // pos
                uiShowObj.transform.rotation = hit.transform.rotation;
                uiShowObj.transform.position = newPos;
            }
        } else
        {
            uiShowObj.SetActive(false);
        }
    
        if (aimObject.canSet )
        {
            aimMeshRenderer.material.color =aimObject.Ready; 
        }
        else 
        {
            aimMeshRenderer.material.color = aimObject.noReady; 
        }

    }

}
