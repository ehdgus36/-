using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anime;
    public float attack_radius;
    public LayerMask layer;
    public Collider[] collider;
    public PlayerMove plmove;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
        if (!anime.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            plmove.enabled = true;
        }
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            plmove.enabled = false;
        }
    }
    void attack()
    {
        Debug.Log("aaaa");
        collider = Physics.OverlapSphere(transform.position, attack_radius,layer);
        if (collider[0] != null)
        {
            plmove.enabled = false;
            anime.SetTrigger("attack");
            transform.position = collider[0].transform.position+collider[0].transform.forward *- 0.5f;
            transform.LookAt(new Vector3( collider[0].transform.position.x, transform.position.y, collider[0].transform.position.z));
            
            collider[0].GetComponent<Enamy>().changeState(EnamyState.die);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attack_radius);
    }
}
