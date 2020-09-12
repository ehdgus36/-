using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamySetUpPoint : MonoBehaviour
{
    public Transform[] Patrol_point;
    public int number=0;
    GameObject enemy; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }
    void Start()
    {
        enemy = Enamyobj_pool.Instance.get_obj_pooling();
        enemy.transform.position = transform.position;
        for (int i = 0; i < Patrol_point.Length; i++)
        {
            enemy.GetComponent<Enamy>().patrol_Point.Add(Patrol_point[i]);                           
        }
        enemy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < Patrol_point.Length - 1;i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Patrol_point[i].position, Patrol_point[i + 1].position);
            
        }
    }
}
