using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnplay : MonoBehaviour
{
    [SerializeField] 
    public List<Transform> slotS;

    public int InWhichFace(Vector3 hitPoint)
    {
        float min = -1.0f;
        int whichSlot = 0;
        for (int i = 0; i < slotS.Count; i++)
        {
            float tempDis = Vector3.Distance(slotS[i].transform.position, hitPoint);
            if (i == 0) min = tempDis;
            
            if (min > tempDis)
            {
                min = tempDis;
                whichSlot = i;
            }
        }

        return whichSlot;
    }
}
