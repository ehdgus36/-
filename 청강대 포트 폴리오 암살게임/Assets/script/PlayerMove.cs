using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed=5;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                speed = 3;
                Move();
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                speed = 1f;   
                Move();
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }
        }
    }
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 lookDirection = new Vector3(x, 0, z);//특정오브젝트를 기준으로 이동방향을 넣는다

        
        transform.Translate(0, 0, speed * Time.deltaTime);

        var Rote = Quaternion.LookRotation(lookDirection);//lookDirection을 회전 값으로만든다

        transform.rotation = Quaternion.Slerp(transform.rotation, Rote, 20 * Time.deltaTime);//* Quaternion.Euler(0, trans.transform.eulerAngles.y, 0);//방향키에따라 바라보는방향이
        
    }
}
