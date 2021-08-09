using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshFilter _meshFilter;
    [SerializeField]
    private List<Vector3> lPos = new List<Vector3>();
    public Vector3 outerPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnDrawGizmos()
    //{
    //    _meshFilter = gameObject.GetComponent<MeshFilter>();
    //    Mesh mesh = _meshFilter.mesh;
    //    if (lPos.Count == 0)
    //    {
    //        for (int i = 0; i < mesh.vertexCount; i++)
    //        {
    //            Vector3 pos = mesh.vertices[i];
    //            lPos.Add(pos);
    //        }    
    //    }


    //    foreach (var pos in lPos)
    //    {
    //        Vector3 wPos = transform.TransformPoint(pos);
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(wPos, 0.1f);
    //    }
    //    
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(outerPos, 0.5f);
    //    
    //    
    //    Vector3 oLPos = transform.InverseTransformPoint(outerPos);
    //    
    //    
    //}
}
