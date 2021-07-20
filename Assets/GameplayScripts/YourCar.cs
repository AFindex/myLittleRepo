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
    private List<baseItem> OwnItem;
    // 相对位置
    private List<Vector3> OwnItemPos;

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

    public void SelectMyItem(Vector3 startPos, Vector3 endPos, UIBaseAction.ChooseType type, Action<baseItem> OnSelectTodo = null)
    { 
        List<Vector3> range = UIBaseAction.ChooseMultiObjectBase(startPos, endPos, type);
        QueryMutilOwnItem(range, OnSelectTodo);
    }
    

    public void UpdateOwnData()
    {
         int childCout = gameObject.transform.childCount;
         if(OwnItem == null) OwnItem = new List<baseItem>(childCout);
         if(OwnItemPos == null) OwnItemPos = new List<Vector3>(childCout);
         for (int i = 0; i < childCout; i++)
         {
             GameObject child = gameObject.transform.GetChild(i).gameObject;
             Vector3 ownPos = child.transform.position;
             Vector3 pos = ownPos - center;
             pos /= 0.4f;
             OwnItemPos[i] = pos;
             OwnItem[i] = child.GetComponent<baseItem>();
         }       
    }

    public void SetCenter(Vector3 pos)
    {
        center = pos;
    }

    public GameObject QuerySingleOwnItem(Vector3 pos, Action<baseItem> OnQueryTodo = null)
    {
        for (int i = 0; i < OwnItemPos.Count; i++)
        {
            if (OwnItemPos[i] == pos)
            {
                OnQueryTodo?.Invoke(OwnItem[i]);
                return OwnItem[i].gameObject;
            }
        }
        return null;
    }

    public List<GameObject> QueryMutilOwnItem(List<Vector3> posList, Action<baseItem> OnQueryTodo = null)
    {
        List<GameObject> res = new List<GameObject>();
        foreach (var pos in posList)
        {
            GameObject queryObj =  QuerySingleOwnItem(pos, OnQueryTodo);
            res.Add(queryObj);
        }
        return res;
    }
}
