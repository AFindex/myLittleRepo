using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class AddItemState : IFsmState<BaseItem>
{
    
    private string stateName = "AddItemState";
    private GameObject test;
    private Vector3 realScale;
    private Vector3 startPos;
    private Vector3 startGenPos;
    private GameObject extendObj;
    private List<Vector3> genPos =new List<Vector3>();
    UIBaseAction.ChooseType whichPanel = UIBaseAction.ChooseType.XAxis | UIBaseAction.ChooseType.ZAxis;
    
    public AddItemState(BaseItem item)
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
            extendObj.GetComponent<MeshRenderer>().material = aimObject.preBuildMat;
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
            test.GetComponent<MeshRenderer>().material = aimObject.preBuildMat;
            test.SetActive(true);
        }
        else
            test.SetActive(true);
        
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
            onMouseUpExtend();
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
        PreGenObj(startPos,newPos);
    }
    void GenObj(Vector3 startPos)
    {
        if (GameplayManager.Instance.yourCarObj == null)
        {
            GameplayManager.Instance.yourCarObj = new GameObject();
            GameplayManager.Instance.yourCarObj.AddComponent<YourCar>();

            GameplayManager.Instance.yourCarObj.name = "yourCar";
            GameplayManager.Instance.yourCarObj.transform.position = startPos;
            GameplayManager.Instance.StartPos = startPos;
            GameplayManager.Instance.rotation = GameplayManager.Instance.yourCarObj.transform.rotation;
            GameplayManager.Instance.yourCar = GameplayManager.Instance.yourCarObj.GetComponent<YourCar>();           
        }

        UIBaseAction.GenMultiObj(genPos, aimObject.gameObject,(GameObject temp) =>
        {
            temp.GetComponent<BoxCollider>().isTrigger = false;
            temp.GetComponent<MeshRenderer>().material = temp.GetComponent<BaseItem>().originMat;
            temp.transform.SetParent(GameplayManager.Instance.yourCarObj.transform);
            temp.layer = LayerMask.NameToLayer("Bulid");
            temp.name = "Gen_item";
            temp.SetActive(true);
        });
        
        GameplayManager.Instance.yourCar.UpdateOwnData();
    }
    
    void PreGenObj(Vector3 startPos, Vector3 endPos)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 4f;
        Vector3 wPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 aimDir = wPos - Camera.main.transform.position;
        Debug.DrawLine(Camera.main.transform.position,wPos,Color.blue);
        bool isReset = false;
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
        
        genPos = UIBaseAction.ChooseMultiObjectShow(extendObj,realScale, startPos, endPos, whichPanel,(() =>
        {
        }));
        startGenPos = startPos;
    }
    
    public void onMouseUpExtend()
    {
        extendObj.transform.localScale = realScale;
        GenObj(startGenPos);
    }
}
