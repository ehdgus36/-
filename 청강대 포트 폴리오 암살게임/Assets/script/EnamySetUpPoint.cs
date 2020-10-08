using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnamySetUpPoint : MonoBehaviour
{
    public Transform[] Patrol_point;
    public int number=0;
    GameObject enemy;
    public Enemy_Patrol_Point[] enemy_Patrols;
    // Start is called before the first frame update

   
    void Start()
    {
        enemy = Enamyobj_pool.Instance.get_obj_pooling();
        enemy.transform.position = transform.position;
        for (int i = 0; i < Patrol_point.Length; i++)
        {
            enemy.GetComponent<Enamy>().patrol_Points.Add(enemy_Patrols[i]);                           
        }
        enemy.SetActive(true);
    }

    // Update is called once per frame
   
    private void OnDrawGizmos()
    {
        for (int i = 0; i < Patrol_point.Length - 1;i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Patrol_point[i].position, Patrol_point[i + 1].position);
            
        }
    }
}
