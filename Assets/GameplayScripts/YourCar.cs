using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum YourCarState
{
    Buliding = 0,
    Runing = 1
}
public class YourCar : MonoBehaviour
{
    public YourCarState CurrentState;
    private Vector3 center;
    private Dictionary<int,baseItem> OwnItem;
    // 相对位置

    private void Start()
    {
        UpdateOwnData();
        StartCoroutine(StateMatchRuning());
    }

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

    public List<int> SelectMyItem(Vector3 startPos, Vector3 endPos, UIBaseAction.ChooseType type, Action<baseItem> OnSelectTodo = null)
    { 
        List<Vector3> range = UIBaseAction.ChooseMultiObjectBase(startPos, endPos, type);
        return QueryMutilOwnItem(range, OnSelectTodo);
    }
    

    public void UpdateOwnData()
    {
        // 每个item 一个index
         int childCout = gameObject.transform.childCount;
         if(childCout == 0) return;
         
         if(OwnItem == null) OwnItem = new Dictionary<int, baseItem>();
         int currentDicCount = OwnItem.Count;
         for (int i = 0; i < childCout; i++)
         {
             GameObject child = gameObject.transform.GetChild(i).gameObject;
             baseItem temp = child.GetComponent<baseItem>();

             if (temp.ItemIndex == -1)
             {
                temp.ItemIndex = currentDicCount + i;
                OwnItem.Add(i, temp);
             }
             
             //temp.ItemIndex = i;
             //if (!OwnItem.ContainsKey(temp.ItemIndex))
             //{
             //    OwnItem.Add(i, temp);
             //}
             //else
             //{
             //    temp.ItemIndex = currentDicCount + i;
             //    OwnItem.Add(i, temp);
             //}
         }       
    }

    public void SetCenter(Vector3 pos)
    {
        center = pos;
    }

    public int QuerySingleOwnItem(Vector3 pos, Action<baseItem> OnQueryTodo = null)
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

    public List<int> QueryMutilOwnItem(List<Vector3> posList, Action<baseItem> OnQueryTodo = null)
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
