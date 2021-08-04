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
    //
    List<GameObject> ropeNodeListL = new List<GameObject>();
    List<GameObject> ropeNodeListR = new List<GameObject>();
    private bool isFlying = false;
    private bool canShoot = false;

    private Vector3 LTarget;
    private Vector3 RTarget;
    private Vector3 hitPos;
    void Start()
    {
        ropeNode.transform.position = slots[0].transform.position;
        for (int i = 0; i < 80; i++)
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
    void Update()
    {
        AimAtWell();
    }

    void AimAtWell()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10000f,(1<<8)))
        {
            hitPos = hit.point;
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }

    IEnumerator FlyingR()
    {
        bool isShooting = false;
        while (true)
        {
            if (canShoot&&Input.GetMouseButtonDown(1))
            {
                isShooting = true;
                RTarget = hitPos;
            }else if (Input.GetMouseButtonUp(1))
            {
                ResetRope(ropeNodeListR,slots[1].transform.position);
                isShooting = false;
            }
            if (isShooting)
            {
                Vector3 dir = (RTarget - slots[1].transform.position).normalized;
                if (Input.GetKey(KeyCode.E))
                {
                    rig.AddForce(dir*rSpeed);
                }
                GenRope(ropeNodeListR, slots[1].transform.position, RTarget);
            }
            
            yield return null;
        }
        yield return null;
    }
    IEnumerator FlyingL()
    {
        bool isShooting = false;
        while (true)
        {
            if (canShoot&&Input.GetMouseButtonDown(0))
            {
                LTarget = hitPos;
                isShooting = true;
            }else if (Input.GetMouseButtonUp(0))
            {
                ResetRope(ropeNodeListL,slots[0].transform.position);
                isShooting = false;
            }
            if (isShooting)
            {
                Vector3 dir = (LTarget - slots[0].transform.position).normalized;
                if (Input.GetKey(KeyCode.Q))
                {
                    rig.AddForce(dir * lSpeed);
                }
                GenRope(ropeNodeListL,slots[0].transform.position, LTarget);
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
