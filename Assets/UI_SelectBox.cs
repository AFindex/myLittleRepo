using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectBox : MonoBehaviour
{
    // Start is called before the first frame update
    public bool BeSelected = false;
    public Image itemImge ;
    public Image bg;

    private void Awake()
    {
        //bg = this.transform.Find("selectBoxBg").GetComponent<Image>();
    }

    public void SetItem()
    {
        // todo
        itemImge.gameObject.SetActive(true);
    }

    public void SetSelected(bool selected)
    {
        BeSelected = selected;
        if (selected)
        {
            bg.gameObject.SetActive(true);
        }
        else
        {
            bg.gameObject.SetActive(false);
        }
    }
    
}
