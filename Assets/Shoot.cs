using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> slots;
    public GameObject ropeNode;
    List<GameObject> ropeNodeListL = new List<GameObject>();
    List<GameObject> ropeNodeListR = new List<GameObject>();
    private bool isFlying = false;

    private Vector3 LTarget;
    private Vector3 RTarget;
    void Start()
    {
        ropeNode.transform.position = slots[0].transform.position;
        for (int i = 0; i < 40; i++)
        {
            GameObject tempNodeL = Instantiate(ropeNode, slots[0].transform.position, Quaternion.identity);
            GameObject tempNodeR = Instantiate(ropeNode, slots[1].transform.position, Quaternion.identity);
            tempNodeL.transform.SetParent(ropeNode.transform);
            tempNodeR.transform.SetParent(ropeNode.transform);
            ropeNodeListL.Add(tempNodeL);
            ropeNodeListL.Add(tempNodeR);
        }
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
        if (!isFlying && Physics.Raycast(ray, out hit, 10000f,(1<<8)))
        {
            Vector3 hitPos = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                LTarget = hitPos;
                isFlying = true;
                StartCoroutine(Flying(false));
            }else if (Input.GetMouseButtonDown(1))
            {
                RTarget = hitPos;
                isFlying = true;
                StartCoroutine(Flying(true));
            }
        }
    }

    IEnumerator Flying(bool isRight)
    {
        while (true)
        {
            if (isRight)
            {
                GenRope(ropeNodeListR,slots[1].transform.position, RTarget);
                if (Input.GetMouseButtonUp(1))
                {
                    isFlying = false;
                    ResetRope(ropeNodeListR,slots[1].transform.position);
                    yield break;
                }
            }else
            {
                GenRope(ropeNodeListL,slots[0].transform.position, LTarget);
                if (Input.GetMouseButtonUp(0))
                {
                    isFlying = false;
                    ResetRope(ropeNodeListL,slots[0].transform.position);
                    yield break;
                }
            }
            yield return null;
        }
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
