using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     private static GameManager _instance;
     public static GameManager Instance
     {
         get
         {
             if(_instance == null)
             {
                 _instance = GameObject.FindObjectOfType<GameManager>();
             }
             return _instance;
         }
     }
        
    // Start is called before the first frame update
    public GameObject cube;
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
        Debug.Log($"scroll :{scroll}, index: {index}");
    }

}
