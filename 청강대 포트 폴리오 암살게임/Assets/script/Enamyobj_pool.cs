using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamyobj_pool : MonoBehaviour
{
    public GameObject[] enemys_objpool;
    public GameObject enemy;
    public static Enamyobj_pool Instance;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < enemys_objpool.Length; i++)
        {
            enemys_objpool[i] = Instantiate(enemy, transform.position, Quaternion.identity);
            enemy.GetComponent<Enamy>().player = player;
            enemys_objpool[i].SetActive(false);
        }
    }   

    // Update is called once per frame
   
    public GameObject get_obj_pooling()
    {
        for (int i = 0; i < enemys_objpool.Length; i++)
        {
            if (enemys_objpool[i].activeSelf == false)
            {
                return enemys_objpool[i];
            }               
        }
        return null;
    }
}
