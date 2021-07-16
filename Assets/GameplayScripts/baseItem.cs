using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class baseItem : MonoBehaviour
{
    public bool canSet = true;
    public Action onMouseDowning;
    public Action onMouseUping;
    public Action<Vector3> onSetPos;
    public void selectTodd()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onMouseDowning?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            onMouseUping?.Invoke();
        }
    }
    public void OnSetThisPos(Vector3 pos)
    {
        onSetPos?.Invoke(pos);
    }

    private void OnTriggerStay(Collider other)
    {
        AimEvent.Instance.item.canSet = false;
    }

    private void OnTriggerExit(Collider other)
    {
        AimEvent.Instance.item.canSet = true;
    }

}
