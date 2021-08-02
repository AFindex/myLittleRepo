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
        
    }
    //Translate移动控制函数
    void MoveControlByTranslateGetAxis()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下

        transform.Translate(transform.forward * vertical * m_speed * Time.deltaTime);//W S 上 下
        transform.Translate(transform.right * horizontal * m_speed * Time.deltaTime);//A D 左右
    }
    //SimpleMove移动控制函数 角色控制器
    void MoveControlBySimpleMove()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下

        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        dir.y = 0;
        
        m_character.SimpleMove(dir* vertical * m_speed); 
    }
}
