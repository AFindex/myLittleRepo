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
    public GameObject yourCar;
    public List<baseItem> items;
    private int index;
    void Awake()
    {
        foreach (var item in items)
        {

        }
        InputManager.Instance.ResigerMouseScroll(MouseMoveEvent);
    }
    
    
    
    

    void MouseMoveEvent()
    {
        int maxNum = items.Count;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        int add = scroll > 0 ? 1 : -1;
        index += add;
        if (index < 0) index = maxNum;
        index %= maxNum;
        
        // switch fun with cube
        AimEvent.Instance.OnAimObjectChange(items[index].gameObject);
        AimEvent.Instance.aimShowObject = Instantiate(items[index].gameObject);
        Debug.Log($"scroll :{scroll}, index: {index}");
    }

}
