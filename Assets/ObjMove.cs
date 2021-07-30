using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{
    public GameObject Hits;
    public GameObject Target;
    private Vector3 beforeMovePos;
    private Vector3 beforeHitsMovePos;
    private Vector3 beforeTargetMovePos;
    void Start()
    {
        beforeMovePos = transform.position;
        beforeHitsMovePos = Hits.transform.position;
        beforeTargetMovePos = Target.transform.position;
        StartCoroutine(move());
    }

    void Update()
    {
        
    }

    IEnumerator move()
    {
        while (true)
        {
            
            yield return null;
        }
    }
}
