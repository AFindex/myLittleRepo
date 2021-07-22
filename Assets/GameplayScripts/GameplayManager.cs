using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

enum ObjState
{
    Building,
    Runing
}

enum BulidState
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
    public YourCar yourCar;
    public List<baseItem> items;
    private int index = -1;
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
            AimEvent.Instance.OnUISelectedChange(items[index], true);
            GameUIManager.Instance.SetState("测试方块-建造");
        }
        else
        {
            GameUIManager.Instance.SetState("");
        }
        
        GameUIManager.Instance.SetBoxSelected(index);
    }

    public void ChangeOpMode()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log($"Change Op Mode to : OpType.Dele");
            if (index < items.Count)
            {
                items[index].opType = OpType.Dele;
                GameUIManager.Instance.SetState("测试方块-涂色");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log($"Change Op Mode to : OpType.Add");
            if (index < items.Count)
            {
                items[index].opType = OpType.Add;
                GameUIManager.Instance.SetState("测试方块-建造");
            }
        }
    }

}
