using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderInter : MonoBehaviour
{
    // Start is called before the first frame update
    public string pName = "_objPos";
    public Material[] toInter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positon = transform.position;
        foreach (var material in toInter)
        {
            material.SetVector(pName,positon);
        }
    }
}
