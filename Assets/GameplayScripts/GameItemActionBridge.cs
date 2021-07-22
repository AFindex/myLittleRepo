using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemActionBridge : MonoBehaviour
{
     
    private static GameItemActionBridge _instance;
    public static GameItemActionBridge Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameItemActionBridge>();
            }
            return _instance;
        }
    }
    
    public void OnItemSetPosBase(Vector3 pos)
    {
        AimEvent.Instance.aimObject.transform.position = pos;
    }
    public void OnItemSetPosAttach(Vector3 pos)
    {
        AimEvent.Instance.aimObject.transform.position = pos;
    }
    public void OnItemMouseDownBase()
    {
        //AimEvent.Instance.onMouseMovingBase();
    }
    public void OnItemMouseUpBase()
    {
    }

    public void OnItemMouseDownExtend(OpType type)
    {
        if (type == OpType.Add)
        {
            
            //AimEvent.Instance.onMouseDownExtend();
        }
    }

    public void OnItemMouseUpExtend(OpType type)
    {
        if (type == OpType.Add)
        {
            //AimEvent.Instance.onMouseUpExtend();
        }
    }
}
