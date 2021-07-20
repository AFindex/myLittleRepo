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
    public GameObject aimShowObject;
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
        aimObject.layer = LayerMask.GetMask("Default");
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
        LayerMask mask = LayerMask.GetMask("Bulid");
        mask = (1 << 3);
        float step = 0.4f;
        Debug.Log($"mask:{mask}");
        while (true)
        {
            Vector3 hitpos = Vector3.zero;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit,100f, mask)) {
                Debug.Log($"hit!!!:{hit.point}");
                if (hit.transform.name == "workHub")
                {
                    item.gameObject.SetActive(true);
                    hitpos = hit.point;
                    Vector3 newPos = hitpos;
                    newPos.x = newPos.x - (newPos.x % step);
                    newPos.y = newPos.y - (newPos.y % step);
                    newPos.z = newPos.z - (newPos.z % step);
                    item.OnSetThisPos(newPos);

                }else if (hit.transform.gameObject.CompareTag("BulidItem"))
                {
                    item.gameObject.SetActive(true);
                    Vector3 hitDir = hit.point - hit.transform.position;
                    Vector3 newPos = hit.transform.position;
                    UIBaseAction.CheckVectorDir(hitDir,((type, isPostiveDir) =>
                    {
                        float addValue = step;
                        if (!isPostiveDir) addValue = -step;
                        if (type == UIBaseAction.ChooseType.XAxis)
                        {
                            newPos.x += addValue;
                        }else if (type == UIBaseAction.ChooseType.YAxis)
                        {
                            newPos.y += addValue;
                        }else if (type == UIBaseAction.ChooseType.ZAxis)
                        {
                            newPos.z += addValue;
                        }
                    }));
                    item.OnSetThisPos(newPos);
                }
            } else
            {
                item.gameObject.SetActive(false);
            }


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
    private bool isExtend = false;
    Vector3 realScale ;

    public void onMouseDownExtend()
    {
        if (isItemMoving && item.canSet)
        {
            startPos = aimObject.transform.position;
            isExtend = true;
            
            StartCoroutine(ExtendCube());
            isItemMoving = false;
        }
    }

    IEnumerator ExtendCube()
    {
        GameObject test = GameObject.Instantiate(aimObject);
        realScale =  aimObject.transform.localScale;
        while (true)
        {
            if (!isExtend)
            {
                Destroy(test);
                yield break;
            }
            
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 4f;
            Vector3 wPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 newPos = wPos;
            newPos.x = newPos.x - (newPos.x % 0.4f);
            newPos.y = newPos.y - (newPos.y % 0.4f);
            newPos.z = newPos.z - (newPos.z % 0.4f);
            
            test.transform.position = newPos;
            Debug.DrawLine(startPos,newPos, Color.red);
            PreGenObj(startPos,newPos);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private Vector3 startGenPos;
    private List<Vector3> genPos =new List<Vector3>();
    UIBaseAction.ChooseType whichPanel = UIBaseAction.ChooseType.XAxis | UIBaseAction.ChooseType.ZAxis;
    void GenObj(Vector3 startPos)
    {
        // frist
        GameplayManager.Instance.yourCar = new GameObject();
        GameplayManager.Instance.yourCar.AddComponent<YourCar>();
        GameplayManager.Instance.yourCar.name = "yourCar";
        GameplayManager.Instance.yourCar.transform.position = startPos;
        UIBaseAction.GenMultiObj(genPos, aimShowObject,(GameObject temp) =>
        {
            temp.GetComponent<BoxCollider>().isTrigger = false;
            temp.GetComponent<MeshRenderer>().material = realMat;
            temp.transform.SetParent(GameplayManager.Instance.yourCar.transform);
        });
    }

    void PreGenObj(Vector3 startPos, Vector3 endPos)
    {
        Vector3 aimDir = endPos - startPos;
        bool isReset = false;
        UIBaseAction.CheckVectorDir(aimDir,null,((type, b) =>
        {
            UIBaseAction.ChooseType all= UIBaseAction.ChooseType.XAxis | UIBaseAction.ChooseType.ZAxis | UIBaseAction.ChooseType.YAxis;
            UIBaseAction.ChooseType newPanel = all ^ type;
            if (newPanel == all) newPanel ^= UIBaseAction.ChooseType.YAxis;
            if (whichPanel != newPanel)
            {
                isReset = true;
                whichPanel = newPanel;
            }
        }));
        
        genPos = UIBaseAction.ChooseMultiObjectShow(aimObject, startPos, endPos, whichPanel,(() =>
        {
            if(isReset)
                aimObject.transform.localScale = realScale;
        }));
        startGenPos = startPos;
    }

    public void onMouseUpExtend()
    {
        isExtend = false;
        aimObject.transform.localScale = realScale;
        GenObj(startGenPos);
        Destroy(aimObject);
    }

}
