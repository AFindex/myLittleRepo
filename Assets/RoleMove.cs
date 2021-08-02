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
    private bool fristStep = true;
    
    private Vector3 beforeMovePos;
    private int index = 0;
    void Start()
    {
        beforeMovePos = transform.position;
        //StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 addDir = transform.position - beforeMovePos;
        addDir.y = 0;
        float Pdis = addDir.magnitude;
        if(Pdis > 2)
        {
            int index2 = (index + 2) % 4;
            Legs[index].CanJump = false;
            Legs[index2].CanJump = false;
            index++;
            index %= 4;
            index2 = (index + 2) % 4;
            Debug.Log(index);
            Legs[index].CanJump = true;
            Legs[index2].CanJump = true;
            
            beforeMovePos = transform.position;
        }

        DynamicForce();
        
    }

    void DynamicForce()
    {
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
                //Debug.Log($"distance:{distance}");
                distance = Mathf.Clamp(0.5f -distance, 0, 0.5f);
                float temp = Mathf.Lerp(0, force, distance);
                //Debug.Log($"temp:{temp}");
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
