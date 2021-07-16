using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMove : MonoBehaviour
{
    // Start is called before the first frame update
    List<ContactPoint> contactPoints = new List<ContactPoint>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        other.GetContacts(contactPoints);
        foreach (var point in contactPoints)
        {
            Vector3 dirOrigin2Contact = point.point - transform.position;
            Vector3 dirOfContact = Vector3.Cross(transform.up, dirOrigin2Contact).normalized;
            Debug.DrawLine(point.point,  point.point - dirOfContact, Color.cyan);
            float forcePower = 0.1f;
            other.rigidbody.AddForceAtPosition(dirOfContact*forcePower,point.point);
        }
    }

    private void OnDrawGizmos()
    {
        foreach (var point in contactPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point.point, 0.01f);
        }
    }
}
