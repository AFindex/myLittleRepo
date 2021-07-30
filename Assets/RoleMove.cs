using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoleMove : MonoBehaviour
{
    // Start is called before the first frame update
    public List<RigMove> Legs;
    public Rigidbody rig;
    public float force = 1f;
    public float heigh = 10f;
    public GameObject[] vertex;
    private bool fristTypeLeg = false;
    void Start()
    {
        //StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        //if (fristTypeLeg)
        //{
        //    Legs[0].CanJump = true;
        //    Legs[2].CanJump = true;
        //    
        //    Legs[1].CanJump = false;
        //    Legs[3].CanJump = false;
        //}
        //else
        //{
        //    Legs[0].CanJump = false;
        //    Legs[2].CanJump = false;
        //    
        //    Legs[1].CanJump = true;
        //    Legs[3].CanJump = true;
        //}
        DynamicForce();
        
    }

    void DynamicForce()
    {
        //foreach (var slot in slots)
        //{
        //    Vector3 startPos = slot.position;
        //    RaycastHit hit;
        //    Vector3 dir = Vector3.down;
        //    if (Physics.Raycast( startPos,dir, out hit, 1000f, ~(1<<6)))
        //    {
        //        Debug.DrawLine(startPos,hit.point, Color.blue);
        //        float dis = Vector3.Distance(startPos, hit.point);
        //        math.clamp(1 - dis, 0, 1);
        //        Debug.Log(dis);
        //        float power =force * 0.25f ;
        //        rig.AddForceAtPosition(Vector3.up* Mathf.Lerp(0, force, dis),startPos);
        //        
        //        
        //    }
        //}
        for (int i = 0; i < vertex.Length; i++)
        {
            var wV = vertex[i].transform.position;
            RaycastHit hit;
            Ray ray = new Ray(wV, Vector3.down);
            if (Physics.Raycast(ray, out hit,1000f, ~(1<<6)))
            {
                
                Debug.DrawLine(wV,hit.point, Color.blue);
                float distance = Vector3.Distance(hit.point, wV);
                distance /= heigh;
                Debug.Log($"distance:{distance}");
                distance = Mathf.Clamp(0.5f -distance, 0, 0.5f);
                float temp = Mathf.Lerp(-force, force, distance);
                Debug.Log($"temp:{temp}");
                rig.AddForceAtPosition(Vector3.up * temp, wV);
            }
        } 
    }

    IEnumerator Timer()
    {
        int allTime = 10;
        int currentTime = 0;
        while (true)
        {
            if (currentTime < allTime)
            {
                currentTime++;
            }
            else
            {
                fristTypeLeg = !fristTypeLeg;
                currentTime = 0;
            }

            yield return null;
        }
    }
}
