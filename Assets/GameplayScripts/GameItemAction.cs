using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItemAction : MonoBehaviour
{
     
    private static GameItemAction _instance;
    public static GameItemAction Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameItemAction>();
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
        AimEvent.Instance.onMouseMovingBase();
    }
    public void OnItemMouseUpBase()
    {
    }

    public void OnItemMouseDownExtend()
    {
        AimEvent.Instance.onMouseDownExtend();
    }

    public void OnItemMouseUpExtend()
    {
        AimEvent.Instance.onMouseUpExtend();
    }
}
