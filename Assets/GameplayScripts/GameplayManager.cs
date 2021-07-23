using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Build.Content;
using UnityEngine;

public enum ObjState
{
    Building,
    Runing
}

public enum BulidState
{
    AddItem,
    DeleItem
}
public class GameplayManager : MonoBehaviour
{
    private static GameplayManager _instance;
    public static GameplayManager Instance
     {
         get
         {
             if(_instance == null)
             {
                 _instance = GameObject.FindObjectOfType<GameplayManager>();
             }
             return _instance;
         }
     }
        
    // Start is called before the first frame update
    public GameObject yourCarObj;
    public Vector3 StartPos;
    public Quaternion rotation;
    public YourCar yourCar;
    public List<BaseItem> items;
    private int index = -1;
    
    //
    public BaseItem currentSelectedItem;
    void Awake()
    {
        InputManager.Instance.ResigerMouseScroll(MouseMoveEvent);
        GameUIManager.Instance.InitItem(items);
        MouseMoveEvent();
    }

    void MouseMoveEvent()
    {
        if (index < items.Count && index >= 0)
        {
            AimEvent.Instance.OnUISelectedChange(items[index], false);
            GameUIManager.Instance.SetState("测试方块-建造");
        }
        int maxNum = 8;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        int add = scroll > 0 ? 1 : -1;
        index += add;
        if (index < 0) index = maxNum - 1;
        else if (index > maxNum) index = 0;
        index %= maxNum;

        if (index < items.Count && index >= 0)
        {
            currentSelectedItem = items[index];
            AimEvent.Instance.OnUISelectedChange(items[index], true);
            GameUIManager.Instance.SetState("测试方块-建造");
        }
        else
        {
            currentSelectedItem = null;
            GameUIManager.Instance.SetState("");
        }
        
        GameUIManager.Instance.SetBoxSelected(index);
    }

    void SetBulidType(ObjState state)
    {
        if (state == ObjState.Building)
        {
            if (yourCarObj != null)
            {
                Rigidbody rig = yourCarObj.GetComponent<Rigidbody>();
                if(rig != null )
                    Destroy(rig);
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].OnLeaveRuning();
                }
                                
                yourCarObj.transform.position = StartPos;
                yourCarObj.transform.rotation = rotation;
            }
            
        }else if (state == ObjState.Runing)
        {
            if (yourCarObj != null)
            {
                Rigidbody rig = yourCarObj.GetComponent<Rigidbody>();
                if (rig == null)
                {
                    rig = yourCarObj.AddComponent<Rigidbody>();
                }
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].OnRuning();
                }               
                rig.useGravity = true;
                rig.freezeRotation = false;
            }
        }
    }

    // 全局
    public void ChangeBulidType()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetBulidType(ObjState.Runing);
        }else if (Input.GetKeyDown(KeyCode.B))
        {
            SetBulidType(ObjState.Building);
        }
    }

    public void SelectedItemOperation()
    {
        if (currentSelectedItem)
        {
            currentSelectedItem.ChangeOpMode();
        }
    }

}
