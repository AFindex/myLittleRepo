using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Timeline;

public class Float : MonoBehaviour
{
    // Start is called before the first frame update
    public float floatPower;
    private Rigidbody rig;
    RaycastHit hit;
    Ray ray ;
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ray = new Ray(transform.position,Vector3.down);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (hit.transform.gameObject.CompareTag("water"))
            {
                Debug.Log("is hit?");
                float y = hit.point.y;
                if (transform.position.y < y)
                {
                    float dis = y - transform.position.y;
                    dis = Mathf.Clamp(dis, 0, 5);
                    dis /= 5;
                    float force = Mathf.Lerp(0,1,dis);
                    rig.AddForce(Vector3.up*force*floatPower);
                }               
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction);
        Gizmos.DrawSphere(hit.point, 3);
    }
}
