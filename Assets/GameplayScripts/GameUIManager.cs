using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
        
    private static GameUIManager _instance;
    public static GameUIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameUIManager>();
            }
            return _instance;
        }
    }
    public Text State;
    public List<UI_SelectBox> selectBoxes;

    public void SetBoxSelected(int index)
    {
        for (int i = 0; i < selectBoxes.Count; i++)
        {
            if(i == index)
                selectBoxes[index].SetSelected(true);
            else
                selectBoxes[i].SetSelected(false);   
        }
    }

    public void InitItem(List<BaseItem> toInit)
    {
        selectBoxes[0].SetItem();
        
    }

    public void SetState(string info)
    {
        State.text = info;
    }

}
