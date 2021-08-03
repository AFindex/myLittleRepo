using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTemp : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_speed;
    public CharacterController m_character;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveControlBySimpleMove();
        MoveWithCamera();
        
    }
    //Translate移动控制函数
    void MoveWithCamera()
    {
        float move = Input.GetAxis("Mouse X");
        Vector3 dir = ( Camera.main.transform.position - transform.position).normalized;
        dir.y = 0;
        Vector3 currentDir = transform.forward;
        currentDir.y = 0;
        float angle = Vector3.Angle(-dir, currentDir);
        //Debug.Log($"move:{move},angle:{angle}");
        //
        float value = 0;
        if(angle!=0) value = move / angle;
        transform.forward = -dir;
        //transform.RotateAround(transform.position,Vector3.up,value );
    }
    //SimpleMove移动控制函数 角色控制器
    void MoveControlBySimpleMove()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下

        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        dir.y = 0;
        Vector3 hDir = -Vector3.Cross(dir, Vector3.up).normalized;
        Vector3 moveDir = (dir * vertical + horizontal * hDir).normalized;
        
        m_character.SimpleMove(moveDir* m_speed); 
    }
}
