using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEvent : MonoBehaviour
{
    
    private static AimEvent _instance;
    public static AimEvent Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AimEvent>();
            }
            return _instance;
        }
    }
    
    public GameObject aimObject;
    public Material preBuildMat;
    public baseItem item;
    public bool isItemMoving = false;
    
    public Color noReady;
    public Color Ready;
    Material realMat;
    MeshRenderer aimMeshRenderer;


     // 应当配表
     // 对象池
     public void OnAimObjectChange(GameObject newObj)
    {
        // todo
        if(aimObject != null && isItemMoving)
            Destroy(aimObject);
        aimObject = GameObject.Instantiate(newObj);
        //
        item = aimObject.GetComponent<baseItem>();
        SetItemAction();
        item.canSet = true;
        aimObject.GetComponent<BoxCollider>().isTrigger = true;
        isItemMoving = true;
        aimMeshRenderer = aimObject.GetComponent<MeshRenderer>();
        realMat = aimMeshRenderer.material;
        aimMeshRenderer.material = preBuildMat;
        StartCoroutine(MouseMoving());
    }

     void SetItemAction()
     {
         item.onMouseDowning += GameItemAction.Instance.OnItemMouseDownExtend;
         item.onMouseUping += GameItemAction.Instance.OnItemMouseUpExtend;
         item.onSetPos += GameItemAction.Instance.OnItemSetPosBase;
     }

    IEnumerator MouseMoving()
    {
        while (true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 4f;
            Vector3 wPos = Camera.main.ScreenToWorldPoint(mousePos);
            item.OnSetThisPos(wPos);
            //
            if (item.canSet && isItemMoving)
            {
                aimMeshRenderer.material.color = Ready; 
            }
            else if(isItemMoving)
            {
                aimMeshRenderer.material.color = noReady; 
            }
            if(!isItemMoving)
                yield break;
            yield return null;
        }
        yield return null;
    }

    public void onMouseMovingBase()
    {
        if (isItemMoving && item.canSet)
        {
           aimObject.GetComponent<BoxCollider>().isTrigger = false;
           aimMeshRenderer.material = realMat;
           isItemMoving = false;
        }
    }
    
    // extend
    private Vector3 startPos; 
    private Vector3 endPos;
    Vector3 preScale ;
    private bool isExtend = false;

    private List<Vector3> posSet = new List<Vector3>();
    public void onMouseDownExtend()
    {
        if (isItemMoving && item.canSet)
        {
            preScale = aimObject.transform.localScale;
            startPos = aimObject.transform.position;
            endPos = startPos;
            isExtend = true;
            
            StartCoroutine(ExtendCube());
            isItemMoving = false;
        }
    }

    IEnumerator ExtendCube()
    {
        while (true)
        {
            if(!isExtend) yield break;
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 4f;
            Vector3 wPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 currentPos = wPos;
            Vector3 newPos = (wPos + startPos) / 2;
            aimObject.transform.position = newPos;
            
            GenObj(startPos,wPos);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void GenObj(Vector3 startPos, Vector3 endPos)
    {
        Vector3 len = endPos - startPos;
        Vector3 scale = aimObject.transform.localScale;
        Vector3 eachAdd = len.normalized * scale.x;
        Debug.Log($"add: {eachAdd}, len:{len}");
        bool xdir = startPos.x > endPos.x;
        float xlen = len.x > 0 ? len.x : -len.x;
        int xNums = (int)(xlen / scale.x);
        if (xNums == 0) xNums = 1;
        
        bool ydir = startPos.y > endPos.y;
        bool zdir = startPos.z > endPos.z;
        
        for(int x = 0 ; x < xNums; x++)
        {
            Vector3 newpos = startPos;
            newpos.x += x*eachAdd.x;
            GameObject.Instantiate(aimObject, newpos, Quaternion.identity);
        }
    }

    public void onMouseUpExtend()
    {
        isExtend = false;
        endPos = aimObject.transform.position;
        aimObject.GetComponent<BoxCollider>().isTrigger = false;
        aimMeshRenderer.material = realMat;
    }

}
