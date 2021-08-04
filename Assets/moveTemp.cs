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
        //Vector3 dir = ( Camera.main.transform.position - transform.position).normalized;
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;
        transform.forward = dir;
    }
    //SimpleMove移动控制函数 角色控制器
    void MoveControlBySimpleMove()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下

        Vector3 dir = transform.forward;
        dir.y = 0;
        Vector3 hDir = transform.right;
        hDir.y = 0;
        Vector3 moveDir = (dir * vertical + hDir * horizontal).normalized;
        
        transform.Translate(moveDir*m_speed,Space.World);
    }
}
