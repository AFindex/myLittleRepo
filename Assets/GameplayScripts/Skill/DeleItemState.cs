using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleItemState : IFsmState<BaseItem>
{    
    
    private string stateName = "AddItemState";
    private GameObject test;
    private Vector3 realScale;
    private Vector3 startPos;
    private Vector3 startGenPos;
    private GameObject extendObj;
    private List<int> toDele;
    UIBaseAction.ChooseType whichPanel = UIBaseAction.ChooseType.XAxis | UIBaseAction.ChooseType.ZAxis;
    
    public DeleItemState(BaseItem item)
    {
        this.aimObject = item;
    }
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
        if (extendObj == null)
        {
            extendObj = GameObject.Instantiate(aimObject.gameObject);
            extendObj.name = "extenObj";
            extendObj.GetComponent<MeshRenderer>().material = aimObject.selectedShow;
            extendObj.SetActive(true);
        }
        else
        {
            extendObj.SetActive(true);
            extendObj.transform.localScale = aimObject.gameObject.transform.localScale;
        }

        if (test == null)
        {
            test = GameObject.Instantiate(aimObject.gameObject);
            test.name = "test";
            test.GetComponent<MeshRenderer>().material = aimObject.selectedShow;
            test.SetActive(true);
        }
        else
            test.SetActive(true);
        
        toDele = new List<int>();
        realScale =  aimObject.gameObject.transform.localScale;
        if (parms.Length > 0)
        {
            if (parms[0] is Vector3)
                startPos = (Vector3)parms[0];
        }
        Debug.Log($"OnEnter : {stateName}, startPos{startPos}");
    }
 
    public override void OnLeave()
    {
        extendObj.SetActive(false);
        test.SetActive(false);
        Debug.Log($"OnLeave : {stateName}");
    }
 
    public override void OnUpdate()
    {
        StateChange();
        ExtendCube();
        Debug.Log($"OnUpdate : {stateName}");
    }

    void StateChange()
    {
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUpDele();
            FsmManager.Instance.ChangeFsmState<IFsmState<BaseItem>,BaseMoveItemState>(FsmOwner);
        }
    }
    void ExtendCube()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 4f;
        Vector3 wPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 newPos = wPos;
        newPos.x = newPos.x - (newPos.x % 0.4f);
        newPos.y = newPos.y - (newPos.y % 0.4f);
        newPos.z = newPos.z - (newPos.z % 0.4f);
        
        test.transform.position = newPos;
        PreDeleObjSelected(startPos,newPos);
    }
    void DeleObj()
    {
        GameplayManager.Instance.yourCar.DestoryMyItem(toDele);
    }
    
    void PreDeleObjSelected(Vector3 startPos, Vector3 endPos)
    {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 4f;
        Vector3 wPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 aimDir = wPos - Camera.main.transform.position;
        UIBaseAction.CheckVectorDir(aimDir,null,((type, b) =>
        {
            Debug.Log($"type: {type}");
            UIBaseAction.ChooseType all= UIBaseAction.ChooseType.XAxis | UIBaseAction.ChooseType.ZAxis | UIBaseAction.ChooseType.YAxis;
            UIBaseAction.ChooseType newPanel = all ^ type;
            if (newPanel == all) newPanel ^= UIBaseAction.ChooseType.YAxis;
            //whichPanel = newPanel;

            if (type == UIBaseAction.ChooseType.XAxis)
            {
                whichPanel = UIBaseAction.ChooseType.YAxis | UIBaseAction.ChooseType.ZAxis;
            }else if (type == UIBaseAction.ChooseType.ZAxis)
            {
                whichPanel = UIBaseAction.ChooseType.YAxis | UIBaseAction.ChooseType.XAxis;
            }else 
            {
                whichPanel = UIBaseAction.ChooseType.XAxis | UIBaseAction.ChooseType.ZAxis;
            }
                
        }));
        
        UIBaseAction.ChooseMultiObjectShow(extendObj,realScale, startPos, endPos, whichPanel,null);
        toDele = GameplayManager.Instance.yourCar.SelectMyItem(startPos,endPos,
            whichPanel,(item =>
            {
                item.gameObject.GetComponent<MeshRenderer>().material = item.beSelectedMat;
                Debug.Log($"OnSelected: {item.name}");
            }));       
        startGenPos = startPos;
    }
    
    public void onMouseUpDele()
    {
        extendObj.transform.localScale = realScale;
        //DeleObj();
    }
}
