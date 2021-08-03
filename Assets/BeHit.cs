using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHit : MonoBehaviour
{
    // Start is called before the first frame update
    int breakCount = 4;
    private List<GameObject> genList = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeHitFormPoint(Vector3 hitPosition)
    {
        
        
    }

    void GenObj()
    {
        GameObject temp = Instantiate(gameObject, transform.position, Quaternion.identity);

    }
}
