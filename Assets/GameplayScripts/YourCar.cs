using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum YourCarState
{
    Buliding = 0,
    Runing = 1
}

public class ObjPos
{
    public Vector3 position;
    public Quaternion rotation;

}
public class YourCar : MonoBehaviour
{
    // public
    public YourCarState CurrentState;
    public Dictionary<int,BaseItem> OwnItem;
    public List<GameObject> BlockList = new List<GameObject>();
    public List<ObjPos> blockListTransforms = new List<ObjPos>();
    // private
    private int currentDicCount = 0;
    private Vector3 center;
    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        UpdateOwnData();
        StartCoroutine(StateMatchRuning());
    }
    /// <summary>
    /// 为新item设置父物体
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    public void AddItemToBlock(ref BaseItem item ,int index)
    {
        if (index == -1)
        {
            int max = BlockList.Count - 1;
            GameObject newBlock = new GameObject();
            newBlock.name = "block";
            newBlock.transform.SetParent(GameplayManager.Instance.yourCarObj.transform);
            BlockList.Add(newBlock);
            ObjPos tempTransform = new ObjPos();
            tempTransform.position = newBlock.transform.position;
            tempTransform.rotation = newBlock.transform.rotation;
            
            blockListTransforms.Add(tempTransform);
            
            // set item index
            item.BlockListIndex = max + 1;
            item.gameObject.transform.SetParent(BlockList[item.BlockListIndex].transform);
        }else if (index == -2)
        {
            item.BlockListIndex = BlockList.Count - 1;
            item.gameObject.transform.SetParent(BlockList[item.BlockListIndex].transform);
        }
        else if (index < BlockList.Count && index >=0)
        {
            item.BlockListIndex = index;
            item.gameObject.transform.SetParent(BlockList[item.BlockListIndex].transform);
        }
    }
    /// <summary>
    /// 运行时状态更新
    /// </summary>
    /// <returns></returns>
    IEnumerator StateMatchRuning()
    {
        while (true)
        {
            if (CurrentState == YourCarState.Buliding)
            {
                
            }
            else if(CurrentState == YourCarState.Runing)
            {
                
            }
            yield return null;
        }
        yield return null;
    }
    /// <summary>
    /// 删除物体
    /// </summary>
    /// <param name="toDele"></param>
    public void DestoryMyItem(List<int> toDele)
    {
        for (int i = 0; i < toDele.Count; i++)
        {
            if (OwnItem.ContainsKey(i))
            {
                OwnItem.Remove(i);
            }
        }
    }
    /// <summary>
    /// 选择对应范围内的物体
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="type"></param>
    /// <param name="OnSelectTodo"></param>
    /// <returns></returns>
    public List<int> SelectMyItem(Vector3 startPos, Vector3 endPos, UIBaseAction.ChooseType type, Action<BaseItem> OnSelectTodo = null)
    { 
        List<Vector3> range = UIBaseAction.ChooseMultiObjectBase(startPos, endPos, type);
        return QueryMutilOwnItem(range, OnSelectTodo);
    }
    /// <summary>
    /// 更新子物体数据
    /// </summary>
    public void UpdateOwnData()
    {
        // 每个item 一个index
         int childCout = gameObject.transform.childCount;
         if(childCout == 0) return;
         
         if(OwnItem == null) OwnItem = new Dictionary<int, BaseItem>();
         
         for (int i = 0; i < childCout; i++)
         {
             GameObject child = gameObject.transform.GetChild(i).gameObject;
             if (child.name == "block")
             {
                int subchildCout = child.transform.childCount;
                if(subchildCout == 0) continue;
                for (int z = 0; z < subchildCout; z++)
                {
                    GameObject subChild = child.transform.GetChild(z).gameObject;
                    BaseItem temp = subChild.GetComponent<BaseItem>();
                     if (temp != null && temp.ItemIndex == -1)
                     { 
                         currentDicCount += 1;
                         temp.ItemIndex = currentDicCount;
                         Debug.Log("child");
                         OwnItem.Add(currentDicCount, temp);
                     }       
                }
             }
         }       
    }
    /// <summary>
    /// 设置运行时，包括设置各分块物理
    /// </summary>
    /// <param name="toRunning"></param>
    public void YourCarSetRunning(bool toRunning)
    {
        int childCout = gameObject.transform.childCount;
        if(childCout == 0) return;
        
        for (int i = 0; i < childCout; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.name == "block")
            {
                if (toRunning)
                {
                    Rigidbody rig = child.GetComponent<Rigidbody>();
                    if (rig == null)
                    {
                        rig = child.AddComponent<Rigidbody>();
                    } 
                    rig.useGravity = true;
                    rig.freezeRotation = false;
                }
                else
                {
                     Rigidbody rig = child.GetComponent<Rigidbody>();
                     if(rig != null )
                         Destroy(rig);

                     child.transform.position = blockListTransforms[i].position;
                     child.transform.rotation = blockListTransforms[i].rotation;
                }       
            }
        }
        
    }
    public void SetCenter(Vector3 pos)
    {
        center = pos;
    }
    /// <summary>
    /// 查询物体-single
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="OnQueryTodo"></param>
    /// <returns></returns>
    public int QuerySingleOwnItem(Vector3 pos, Action<BaseItem> OnQueryTodo = null)
    {
        foreach (var item in OwnItem)
        {
            if (item.Value.gameObject.transform.position == pos)
            {
                OnQueryTodo?.Invoke(item.Value);
                return item.Key;
            }
            
        }
        return -1;
    }
    /// <summary>
    /// 查询物体-mutil
    /// </summary>
    /// <param name="posList"></param>
    /// <param name="OnQueryTodo"></param>
    /// <returns></returns>
    public List<int> QueryMutilOwnItem(List<Vector3> posList, Action<BaseItem> OnQueryTodo = null)
    {
        List<int> res = new List<int>();
        foreach (var pos in posList)
        {
            int queryObjIndex =  QuerySingleOwnItem(pos, OnQueryTodo);
            if(queryObjIndex != -1)
                res.Add(queryObjIndex);
        }
        return res;
    }
}
