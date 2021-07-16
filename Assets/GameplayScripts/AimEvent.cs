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
    private List<GameObject> gameSet = new List<GameObject>();
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
            Vector3 sub = currentPos - startPos;

            //Vector3 newScale;
            //newScale.x = preScale.x * sub.x ;
            //newScale.y = preScale.y * sub.y ;
            //newScale.z = preScale.z * sub.z ;
            //Debug.Log($"sub: {sub}, new:{newScale}");
            //aimObject.transform.localScale = newScale;
            
            //int xCount = (int) (float)(sub.x / 1.4f);
            //xCount = xCount > 0 ? xCount : -xCount;
            //int yCount = (int) (float)(sub.y / 0.4f);
            //yCount = yCount > 0 ? yCount : -yCount;
            //int zCount = (int) (float)(sub.z / 0.4f);
            //zCount = zCount > 0 ? zCount : -zCount;

            //for (int x = 0; x < xCount; x++)
            //{
            //    for (int y = 0; y < yCount; y++)
            //    {
            //        for (int z = 0; z < zCount; z++)
            //        {
            //            Vector3 subNormal = sub.normalized;
            //            Vector3 newSubPos = startPos;
            //            if (posSet.Contains(newSubPos))
            //            {
            //                Debug.Log("already have it!");
            //            }
            //            
            //            posSet.Add(newPos);   
            //           newSubPos.x += subNormal.x * x;
            //           newSubPos.y += subNormal.y * y;
            //           newSubPos.z += subNormal.z * z;
            //        }
            //    }
            //}

            yield return new WaitForSeconds(0.01f);
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
