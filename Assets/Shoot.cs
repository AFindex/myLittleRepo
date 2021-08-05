using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> slots;
    public GameObject ropeNode;
    public Rigidbody rig;

    public float lSpeed = 1f;
    public float rSpeed = 1f;
    
    public float lForce = 1f;
    public float rForce = 1f;
    //
    List<GameObject> ropeNodeListL = new List<GameObject>();
    List<GameObject> ropeNodeListR = new List<GameObject>();
    private bool canShoot = false;
    
    private bool rShooting = false;
    private bool lShooting = false;

    private Vector3 LTarget;
    private Vector3 RTarget;
    
    private GameObject LTargetGObj;
    private GameObject RTargetGObj;
    
    private Vector3 hitPos;
    private GameObject hitObj;
    void Start()
    {
        ropeNode.transform.position = slots[0].transform.position;
        for (int i = 0; i < 160; i++)
        {
            GameObject tempNodeL = Instantiate(ropeNode, slots[0].transform.position, Quaternion.identity);
            GameObject tempNodeR = Instantiate(ropeNode, slots[1].transform.position, Quaternion.identity);
            tempNodeL.transform.SetParent(transform);
            tempNodeR.transform.SetParent(transform);
            ropeNodeListL.Add(tempNodeL);
            ropeNodeListR.Add(tempNodeR);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(FlyingL());
        StartCoroutine(FlyingR());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        AimAtWell();
        Debug.Log($"Rshooting: {rShooting}");

        if (rShooting)
        {
            Vector3 dirR = (RTarget - slots[1].transform.position).normalized;
            float dot = Vector3.Dot(rig.velocity, dirR);
            dot = dot > 0 ? dot : -dot;
            float dirV = dot / rig.velocity.magnitude;
            if (dirV< rSpeed)
            {
                rig.AddForce(dirR * rForce,ForceMode.Acceleration);
            }
        }

        if (lShooting)
        {
            Vector3 dirL = (LTarget - slots[0].transform.position).normalized;
            float dot = Vector3.Dot(rig.velocity, dirL);
            dot = dot > 0 ? dot : -dot;
            float dirV = dot / rig.velocity.magnitude;
            if (dirV < lSpeed)
            {
                rig.AddForce(dirL * lForce,ForceMode.Acceleration);
            }
        }
    }

    void AimAtWell()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10000f,(1<<8)))
        {
            hitPos = hit.point;
            hitObj = hit.transform.gameObject;
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    void OnShootIt(bool isRight)
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (isRight)
            {
                Vector3 len = (slots[1].transform.position - RTargetGObj.transform.position);
                Vector3 dir = len.normalized;
                float lenV = len.magnitude;
                Debug.Log($"lenv:{lenV}");
                float disFocter = Mathf.Clamp(lenV, 12, 15);
                float force = (disFocter - 12)*rForce;
                Rigidbody rigTemp = RTargetGObj.GetComponent<Rigidbody>();
                if (rigTemp)
                {
                    rigTemp.AddForceAtPosition(dir*force,RTarget);
                }
            }
            else
            {
                Vector3 len = (slots[0].transform.position - LTargetGObj.transform.position);
                Vector3 dir = len.normalized;
                float lenV = len.magnitude;
                float disFocter = Mathf.Clamp(lenV, 12, 15);
                float force = (disFocter - 12)*lForce;
                Rigidbody rigTemp = LTargetGObj.GetComponent<Rigidbody>();
                if (rigTemp)
                {
                    rigTemp.AddForceAtPosition(dir*force,LTarget);
                }
            }    
        }
    }

    IEnumerator FlyingR()
    {
        int MaxTime = 300;
        int i = 0;
        
        Vector3 objPos = Vector3.zero; 
        while (true)
        {
            if (canShoot&&Input.GetMouseButtonDown(1))
            {
                i = 0;
                RTarget = hitPos;
                RTargetGObj = hitObj;
                objPos = RTargetGObj.transform.InverseTransformPoint(RTarget);
            }else if (Input.GetMouseButtonUp(1))
            {
                ResetRope(ropeNodeListR,slots[1].transform.position);
                RTarget = slots[1].transform.position;
                RTargetGObj = null;
                rShooting = false;
            }
            if (Input.GetMouseButton(1))
            {
                if (RTargetGObj)
                {
                    RTarget = RTargetGObj.transform.TransformPoint(objPos);
                    Vector3 dir = (RTarget - slots[1].transform.position).normalized;
                    if (Input.GetKey(KeyCode.E))
                    {
                        if (i >= MaxTime)
                        {
                            rShooting = false;
                        }
                        else
                        {
                            rShooting = true;
                        }
                        
                        i++;
                    }
                    OnShootIt(true);
                    GenRope(ropeNodeListR, slots[1].transform.position, RTarget);
                }

            }
            
            yield return null;
        }
        yield return null;
    }

    IEnumerator FlyingL()
    {
        int MaxTime = 300;
        int i = 0;
        Vector3 objPos = Vector3.zero; 
        while (true)
        {
            if (canShoot&&Input.GetMouseButtonDown(0))
            {
                i = 0;
                LTarget = hitPos;
                LTargetGObj = hitObj;
                objPos = LTargetGObj.transform.InverseTransformPoint(LTarget);
            }else if (Input.GetMouseButtonUp(0))
            {
                ResetRope(ropeNodeListL,slots[0].transform.position);
                LTarget = slots[0].transform.position;
                LTargetGObj = null;
                lShooting = false;
            }
            if (Input.GetMouseButton(0))
            {
                if (LTargetGObj)
                {
                    LTarget = LTargetGObj.transform.TransformPoint(objPos);
                    Vector3 dir = (LTarget - slots[0].transform.position).normalized;
                    if (Input.GetKey(KeyCode.Q))
                    {
                        if (i >= MaxTime)
                        {
                            lShooting = false;
                        }
                        else
                        {
                            lShooting = true;
                        }
                        i++;
                    }
                    OnShootIt(false);
                    GenRope(ropeNodeListL,slots[0].transform.position, LTarget);                   
                }
            }
            yield return null;
        }
        yield return null;
    }
    

    void GenRope(List<GameObject> list,Vector3 start, Vector3 end)
    {
        int max = list.Count;
        for (int i = 0; i < max; i++)
        {
            Vector3 newPos = (start * (max - i) + i * end)/max;
            list[i].transform.position = newPos;
        }
    }

    private void ResetRope(List<GameObject>list, Vector3 pos)
    {
        int max = list.Count;
        for (int i = 0; i < max; i++)
        {
            list[i].transform.position = pos;
        }
    }
}
