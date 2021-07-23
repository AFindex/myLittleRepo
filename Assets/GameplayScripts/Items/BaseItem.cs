using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpType
{
    Add = 0,
    Dele = 1,
}

public class BaseItem : MonoBehaviour
{
    // static
    public Material beSelectedMat;
    public Material preBuildMat;
    public Material originMat;
    public Material selectedShow;
    public Color noReady;
    public Color Ready;
        
    // instance
    public bool canSet = true;
    public int ItemIndex = -1;
    public OpType opType = OpType.Add;
    public string itemName = "test";
    
    // state
    protected SkillFSM itemFsm;
    public void Awake()
    {
        originMat = GetComponent<MeshRenderer>().material;
    }

    public virtual void OnUISelected(bool isSelected)
    {
        
    }
    

    public virtual void ChangeOpMode()
    {
        
    }
    public virtual void OnRuning()
    {
        
    }
    
    public virtual void OnLeaveRuning()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        AimEvent.Instance.item.canSet = false;
    }
    
    private void OnTriggerExit(Collider other)
    {
        AimEvent.Instance.item.canSet = true;
    }
}
