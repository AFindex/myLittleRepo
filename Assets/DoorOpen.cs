using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility.Inspector;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    private float openRate;
    public GameObject Slot;
    public bool isOpening = false;
    private Vector3 orginPos;
    public float OpenSpeed = 1f;
    public float CloseSpeed = 1f;
    void Start()
    {
        orginPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        if (!isOpening)
        {
            if (currentPos.y < orginPos.y)
            {
                float dis = orginPos.y - currentPos.y;
                currentPos.y += dis * CloseSpeed;
                transform.position = currentPos;
            }
        }
        else
        {
            if (currentPos.y > Slot.transform.position.y)
            {
                float dis = Slot.transform.position.y - currentPos.y;
                currentPos.y += dis * OpenSpeed;
                transform.position = currentPos;
            }
        }
    }

    public void OpenState(bool open)
    {
        isOpening = open;
    }
    
    
}
