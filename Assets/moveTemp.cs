using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class moveTemp : MonoBehaviour
{
    // Start is called before the first frame update
    public float force;
    public float Gforce;
    public float GMaxSpeed;
    public float maxSpeed;
    public Rigidbody rigidbody;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveControlBySimpleMove();
        MoveWithCamera();
        
        Gravity();
    }

    void Gravity()
    {
        rigidbody.AddForce(Vector3.down * Gforce,ForceMode.Acceleration);
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
        
        if ((rigidbody.velocity ).magnitude <= maxSpeed)
        {
            rigidbody.AddForce(moveDir*force,ForceMode.Acceleration);
        }
        
        //transform.Translate(moveDir*m_speed,Space.World);
        
    }
    IEnumerator timer(Action inTimeTodo, Action AfterTodo)
    {
        int MaxTime = 5;
        int i = 0;
        while (true)
        {
            if (i >= MaxTime)
            {
                AfterTodo?.Invoke();
                yield break;
            }
            else
            {
               inTimeTodo?.Invoke();
            }
            i++;
            yield return null;
        }
    }
}
