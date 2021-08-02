using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigMove : MonoBehaviour
{
    public GameObject Hits;
    public GameObject Target;
    public GameObject EndJoint;

    public float originPos;
    private bool isJump = false;
    private Vector3 jumpTarge;
    private float MaxLen;
    private float currY;


    public bool CanJump = false;
    public float step = 2f;

    private bool fristStep = true;
    
    private Vector3 beforeMovePos;
    private Vector3 beforeHitsMovePos;
    private Vector3 beforeTargetMovePos;
    private void Start()
    {
        beforeMovePos = transform.position;
        beforeHitsMovePos = Hits.transform.position;
        beforeTargetMovePos = Target.transform.position;
    }

    void move()
    {
        Vector3 addDir = transform.position - beforeMovePos;
        addDir.y = 0;
        float Pdis = addDir.magnitude;
        if(CanJump)
        {
            if(Pdis > 2) {
                if (fristStep)
                {
                    step = 2;
                    fristStep = false;
                }
                else
                {
                    step = 1;
                }
                Hits.transform.position += addDir *step;
                beforeMovePos = transform.position;
            }    
        }
        
       
        RaycastHit hit;
        Vector3 dir = Vector3.down;
        if (!isJump && Physics.Raycast( Hits.transform.position,dir, out hit, 1000f, ~(1<<6)))
        {
            Debug.DrawLine(hit.point, Hits.transform.position,Color.red);
            Debug.DrawLine(hit.point, EndJoint.transform.position,Color.red);
        
            Vector3 newPos = Target.transform.position;
            newPos.y =hit.point.y + originPos;
            Target.transform.position = newPos;
        }
        
        if (!isJump  && Pdis > 2)
        {
            isJump = true;
            jumpTarge = Hits.transform.position;
            jumpTarge.y = originPos;

            Vector3 currentPXZ = Target.transform.position;
            currentPXZ.y = 0;
            Vector3 currentTargetPXZ = jumpTarge;
            currentTargetPXZ.y = 0;
            MaxLen = Vector3.Distance(currentPXZ, currentTargetPXZ);
            currY = Target.transform.position.y;
        }
        
        if (isJump)
        {
            Vector3 newP = Target.transform.position;
        
            Vector3 newPXZ = newP;
            Vector3 jumpTargeXZ = jumpTarge;
            jumpTargeXZ.y = 0;
            newPXZ.y = 0;
            float disX = Vector3.Distance(newPXZ, jumpTargeXZ);
            
            if (disX < 1f)
            {
                isJump = false;
                Target.transform.position = jumpTarge;
            }
            else
            {
                float height = disX * (MaxLen - disX) + currY;
                float step = 0.1f;
                Vector3 dirXZ = (jumpTargeXZ - newPXZ).normalized;
                newP += dirXZ*step;
                newP.y = height;
                Target.transform.position = newP;
            }
        }
    }

    void Update()
    {
        Hits.transform.position = beforeHitsMovePos;
        Target.transform.position = beforeTargetMovePos;
        
        move();
        //
        
        beforeHitsMovePos = Hits.transform.position;
        beforeTargetMovePos = Target.transform.position;
    }
}
